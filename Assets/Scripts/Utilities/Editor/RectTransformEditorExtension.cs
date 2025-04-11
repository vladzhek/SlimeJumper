#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Angry.Utilities.Editor
{
	[CustomEditor(typeof(RectTransform))]
	public class RectTransformEditorExtension : UnityEditor.Editor
	{
		private UnityEditor.Editor _editor;
		
		private void Awake()
		{
			Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
			Type type = assembly.GetType("UnityEditor.RectTransformEditor");
			_editor = CreateEditor(target, type);
		}
		
		public override void OnInspectorGUI()
		{
			_editor.OnInspectorGUI();
			
			if (GUILayout.Button("Snap Anchors Around Self"))
			{
				SnapAnchorsToSelf();
			}
		}
		
		private void SnapAnchorsToSelf()
		{
			var transform = Selection.activeTransform;
			var rectTransform = transform.GetComponent<RectTransform>();
			Undo.RecordObject(rectTransform, "Snap anchors around self");
			
			if (rectTransform != null)
			{
				var parent = transform.parent.GetComponent<RectTransform>();
				if (parent != null)
				{
					var rtOffsetMin = rectTransform.offsetMin;
					var rtOffsetMax = rectTransform.offsetMax;
					var rtAnchorMin = rectTransform.anchorMin;
					var rtAnchorMax = rectTransform.anchorMax;
					
					var parentWidth = parent.rect.width;      
					var parentHeight = parent.rect.height;
					
					var anchorMin = new Vector2(rtAnchorMin.x + (rtOffsetMin.x / parentWidth),
						rtAnchorMin.y + (rtOffsetMin.y / parentHeight));
					var anchorMax = new Vector2(rtAnchorMax.x + (rtOffsetMax.x / parentWidth),
						rtAnchorMax.y + (rtOffsetMax.y / parentHeight));
					
					rectTransform.anchorMin = anchorMin;
					rectTransform.anchorMax = anchorMax;
					
					rectTransform.offsetMin = new Vector2(0, 0);
					rectTransform.offsetMax = new Vector2(0, 0);
					rectTransform.pivot = new Vector2(0.5f, 0.5f);
				}
			}
		}
		
		private void OnSceneGUI()
		{
			MethodInfo onSceneGUIRefresh = _editor.GetType().GetMethod("OnSceneGUI",
				BindingFlags.NonPublic | BindingFlags.Instance);
			onSceneGUIRefresh?.Invoke(_editor, null);
		}
		
		private void OnDestroy()
		{
			DestroyImmediate(_editor);
		}
	}
}
#endif