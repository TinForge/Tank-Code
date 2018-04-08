using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace TinForge.TankController
{
	[CustomEditor(typeof(Track_Deform))]
	public class Track_Deform_Editor : Editor
	{
		SerializedProperty offsetProp;
		SerializedProperty anchorNumProp;
		SerializedProperty anchorArrayProp;
		SerializedProperty widthArrayProp;
		SerializedProperty heightArrayProp;
		SerializedProperty offsetArrayProp;

		void OnEnable()
		{
			offsetProp = serializedObject.FindProperty("offset");
			anchorNumProp = serializedObject.FindProperty("anchorNum");
			anchorArrayProp = serializedObject.FindProperty("anchorArray");
			widthArrayProp = serializedObject.FindProperty("widthArray");
			heightArrayProp = serializedObject.FindProperty("heightArray");
			offsetArrayProp = serializedObject.FindProperty("offsetArray");
		}

		public override void OnInspectorGUI()
		{
			Set_Inspector();
			if (GUI.changed)
			{
				Set_Vertices();
			}
			if (Event.current.commandName == "UndoRedoPerformed")
			{
				Set_Vertices();
			}
		}

		void Set_Inspector()
		{
			if (EditorApplication.isPlaying == false)
			{
				GUI.backgroundColor = new Color(1.0f, 1.0f, 0.5f, 1.0f);
				serializedObject.Update();
				EditorGUILayout.Space();

				//EditorGUILayout.
				EditorGUILayout.Slider(offsetProp, -2f, 2f, "Vertical Offset ");
				EditorGUILayout.IntSlider(anchorNumProp, 1, 64, "Number of Anchor Wheels");
				EditorGUILayout.Space();

				anchorArrayProp.arraySize = anchorNumProp.intValue;
				widthArrayProp.arraySize = anchorNumProp.intValue;
				heightArrayProp.arraySize = anchorNumProp.intValue;
				offsetArrayProp.arraySize = anchorNumProp.intValue;
				for (int i = 0; i < anchorArrayProp.arraySize; i++)
				{
					anchorArrayProp.GetArrayElementAtIndex(i).objectReferenceValue = EditorGUILayout.ObjectField("Anchor Wheel", anchorArrayProp.GetArrayElementAtIndex(i).objectReferenceValue, typeof(Transform), true);
					EditorGUILayout.Slider(widthArrayProp.GetArrayElementAtIndex(i), 0.0f, 10.0f, "Weight Width");
					EditorGUILayout.Slider(heightArrayProp.GetArrayElementAtIndex(i), 0.0f, 10.0f, "Weight Height");
					EditorGUILayout.Slider(offsetArrayProp.GetArrayElementAtIndex(i), -10.0f, 10.0f, "Offset");
					EditorGUILayout.Space();
				}

				// Update Value
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				if (GUILayout.Button("Update Values"))
				{
					Set_Vertices();
				}
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				//
				serializedObject.ApplyModifiedProperties();
			}
		}

		void Set_Vertices()
		{
			GameObject thisGameObject = Selection.activeGameObject;
			if (thisGameObject.GetComponent<MeshFilter>().sharedMesh == null)
			{
				Debug.LogError("Mesh is not assigned in the Mesh Filter.");
				return;
			}
			PrefabUtility.DisconnectPrefabInstance(thisGameObject); // Break prefab connection.
			Mesh thisMesh = thisGameObject.GetComponent<MeshFilter>().sharedMesh;
			float[] initialPosArray = new float[anchorArrayProp.arraySize];
			IntArray[] movableVerticesList = new IntArray[anchorArrayProp.arraySize];
			// Get vertices in the range.
			for (int i = 0; i < anchorArrayProp.arraySize; i++)
			{
				if (anchorArrayProp.GetArrayElementAtIndex(i).objectReferenceValue != null)
				{
					Transform anchorTransform = anchorArrayProp.GetArrayElementAtIndex(i).objectReferenceValue as Transform;
					initialPosArray[i] = anchorTransform.localPosition.x;
					Vector3 anchorPos = thisGameObject.transform.InverseTransformPoint(anchorTransform.position);
					List<int> withinVerticesList = new List<int>();
					for (int j = 0; j < thisMesh.vertices.Length; j++)
					{
						float distZ = Mathf.Abs(anchorPos.z - thisMesh.vertices[j].z);
						float distY = Mathf.Abs((anchorPos.y + offsetArrayProp.GetArrayElementAtIndex(i).floatValue) - thisMesh.vertices[j].y);
						if (distZ <= widthArrayProp.GetArrayElementAtIndex(i).floatValue * 0.5f && distY <= heightArrayProp.GetArrayElementAtIndex(i).floatValue * 0.5f)
						{
							withinVerticesList.Add(j);
						}
					}
					IntArray withinVerticesArray = new IntArray(withinVerticesList.ToArray());
					movableVerticesList[i] = withinVerticesArray;
				}
			}
			// Set values.
			Track_Deform deformScript = thisGameObject.GetComponent<Track_Deform>();
			deformScript.initialPosArray = initialPosArray;
			deformScript.initialVertices = thisMesh.vertices;
			deformScript.movableVerticesList = movableVerticesList;
		}
	}
}