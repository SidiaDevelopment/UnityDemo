using System.Linq;
using System.Reflection;
using DesperateDevs.Unity.Editor;
using DesperateDevs.Utils;
using UnityEditor;
using UnityEngine;

namespace SidiaKit.Features.Searchable
{
    public class SearchableInspectorUtil
    {
        readonly Object _target;
        readonly SerializedObject _serializedObject;

        FieldInfo[] _fieldInfos;
        string _searchString = "";

        public SearchableInspectorUtil(Object target, SerializedObject serializedObject)
        {
            _target = target;
            _serializedObject = serializedObject;
        }

        public void DrawFields()
        {
            var type = _target.GetType();

            if (_fieldInfos == null)
                _fieldInfos = type
                    .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(f => f.GetCustomAttribute<HideInInspector>() == null)
                    .ToArray();

            _searchString = EditorLayout.SearchTextField(_searchString);

            var color = GUI.contentColor;
            foreach (var info in _fieldInfos)
            {
                GUI.contentColor = color;

                var property = _serializedObject.FindProperty(info.Name);
                var readablePropertyName = info.Name.ToSpacedCamelCase().Substring(1).UppercaseFirst();
                if (EditorLayout.MatchesSearchString(readablePropertyName.ToLower(), _searchString.ToLower()))
                    EditorGUILayout.PropertyField(property, new GUIContent(readablePropertyName), true);
            }

            GUI.contentColor = color;

            _serializedObject.ApplyModifiedProperties();
        }
    }
}