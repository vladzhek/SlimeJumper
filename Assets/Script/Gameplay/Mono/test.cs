using UnityEditor;
using UnityEngine;

namespace Script.Gameplay.Mono
{
    public class EditorGUILayoutPopup : EditorWindow
    {
        public string[] options = new string[] { "Cube", "Sphere", "Plane" };
        public int index = 0;

        [MenuItem("Examples/Editor GUILayout Popup usage")]
        static void Init()
        {
            EditorWindow window = GetWindow(typeof(EditorGUILayoutPopup));
            window.Show();
        }

        void OnGUI()
        {
            index = EditorGUILayout.Popup(index, options);
            if (GUILayout.Button("Create"))
                InstantiatePrimitive();
        }

        private void InstantiatePrimitive()
        {
            Debug.Log("WORK");
        }
    }
}