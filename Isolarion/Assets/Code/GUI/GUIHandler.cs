using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class GUIHandler : MonoBehaviour {
		public void DoCheck() {
			if(!PinHandle.CheckPins()) {
				return;
			}
			if(LevelHandler.s_instance.NextLevel()) {
				return;
			}

			Debug.Log("finished Last Level.");
		}

		public void ResetProgress() {
			PlayerPrefs.SetInt("currentLevel", 0);
		}
	}
}