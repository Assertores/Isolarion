using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class GUIReset : MonoBehaviour {
		public void Execute() {
			PlayerPrefs.SetInt("currentLevel", 0);
		}
	}
}