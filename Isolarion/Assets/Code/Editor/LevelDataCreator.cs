using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Iso {
	public class LevelDataCreator : MonoBehaviour {
		[MenuItem("Assets/Parse Level")] //!< makes that this funktion apears in the drop down menu
		static void ParseLevel() {
			var level = Selection.activeObject as GameObject;
			if(!level) {
				Debug.LogError("FATAL: selected object was no GameObject.");
				return;
			}

			string path = AssetDatabase.GetAssetPath(Selection.activeObject);

			if(path == "") {
				path = "Assets/";
			} else if(Path.GetExtension(path) != "") {
				path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
			}
			path = path.Remove(path.Length - 1);

			string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + level.name + ".asset");

			var element = new LevelData();

			var pins = level.GetComponentsInChildren<PinHandle>();
			element.Pins = new Vector2[pins.Length];
			for(int i = 0; i < pins.Length; i++) {
				element.Pins[i].x = pins[i].transform.position.x;
				element.Pins[i].y = pins[i].transform.position.z;
			}

			var shapes = level.GetComponentsInChildren<ShapeHandle>();
			element.Shapes = new GameObject[shapes.Length];
			for(int i = 0; i < shapes.Length; i++) {
				element.Shapes[i] = PrefabUtility.GetCorrespondingObjectFromSource(shapes[i].gameObject);
			}

			AssetDatabase.CreateAsset(element, assetPathAndName);
			AssetDatabase.SaveAssets();

			Selection.activeObject = element;
			EditorUtility.FocusProjectWindow();
		}
	}
}
