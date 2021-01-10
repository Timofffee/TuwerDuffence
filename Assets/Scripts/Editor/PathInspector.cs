using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class PathInspector : Editor 
{
    private const float handleSize = 0.1f;
	private const float pickSize = 0.12f;
	
	private int selectedIndex = -1;

    private Path path;
    private Transform handleTransform;
    private Quaternion handleRotation;
	
	private Vector3 ShowPoint(int index) {
		Vector3 point = handleTransform.TransformPoint(path.points[index]);
        float size = HandleUtility.GetHandleSize(point);
		Handles.color = index==0 ? Color.yellow : Color.green;
		if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.CubeHandleCap)) 
        {
			selectedIndex = index;
		}
        
		if (selectedIndex == index) 
        {
			EditorGUI.BeginChangeCheck();
			point = Handles.DoPositionHandle(point, handleRotation);
			if (EditorGUI.EndChangeCheck()) {
				Undo.RecordObject(path, "Move Point");
				EditorUtility.SetDirty(path);
				path.points[index] = handleTransform.InverseTransformPoint(point);
			}

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.black;
            Handles.Label(point + new Vector3(0f, 0.3f * size, 0f), index.ToString(), style);
		}
        else
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            Handles.Label(point + new Vector3(0f, 0.3f * size, 0f), index.ToString(), style);
        }
		return point;
	}
    
    private void OnSceneGUI() 
    {
        path = target as Path;
		handleTransform = path.transform;
        handleRotation = handleTransform.rotation;
        if (path.points == null || path.points.Length == 0) 
        {
            return;
        }
        Vector3[] points = new Vector3[path.points.Length];
        for (int idx = 0; idx < points.Length; idx++)
        {
            points[idx] = ShowPoint(idx);
        }

		Handles.color = Color.white;
        for (int idx = 0; idx < points.Length-1; idx++)
        {
            Handles.DrawLine(points[idx], points[idx+1]);
        }
		
	}

}
