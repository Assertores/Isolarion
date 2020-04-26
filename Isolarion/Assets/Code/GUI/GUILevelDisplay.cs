using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Iso {
	public class GUILevelDisplay : MonoBehaviour {

		[SerializeField] TextMeshProUGUI levelProgression;

		private void Update() {
			int currentLevel = (LevelHandler.s_instance.currentLevel + 1);
			int maxLevel = (LevelHandler.s_instance.maxLevel + 1);
			if(currentLevel > maxLevel) {
				currentLevel = maxLevel;
			}
			levelProgression.text = currentLevel + "/" + maxLevel;
		}
	}
}
