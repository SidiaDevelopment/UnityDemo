using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class GraphQl
{
    public void Request(string city)
    {
        var c =Contexts.sharedInstance.game.controller.Value;
        c.StartCoroutine(GetRequest(city));
    }
    
    IEnumerator GetRequest(string city)
    {
        var text = "query { getCityByName(name: \"" + city + "\") { weather { timestamp} } }";
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://graphql-weather-api.herokuapp.com/" + "?query=" + text))
        {
            yield return webRequest.SendWebRequest();
            
            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                ParseTimestamp(webRequest.downloadHandler.text);
            }
        }
    }

    private void ParseTimestamp(string text)
    {
        var regex = new Regex("\"timestamp\":(\\d+)");
        Contexts.sharedInstance.game.ReplaceTimestamp(long.Parse(regex.Matches(text)[0].Groups[1].ToString()));
    }
}