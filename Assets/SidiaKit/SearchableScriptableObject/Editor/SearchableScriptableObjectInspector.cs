using UnityEditor;

namespace SidiaKit.Features.Searchable
{
    [CustomEditor(typeof(SearchableScriptableObject), true)]
    public class SearchableScriptableObjectInspector : Editor
    {
        SearchableInspectorUtil _drawer;

        public override void OnInspectorGUI()
        {
            if (_drawer == null)
                _drawer = new SearchableInspectorUtil(target, serializedObject);

            _drawer.DrawFields();
        }
    }
}