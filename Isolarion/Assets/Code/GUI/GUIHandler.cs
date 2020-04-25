using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Iso {
	public class GUIHandler : MonoBehaviour {

		[SerializeField] TextMeshProUGUI levelProgression;

		private void Update() {
			levelProgression.text = LevelHandler.s_instance.currentLevel + "/" + LevelHandler.s_instance.maxLevel;
		}

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