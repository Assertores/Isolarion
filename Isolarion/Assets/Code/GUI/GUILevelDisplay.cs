using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Iso {
	public class GUILevelDisplay : MonoBehaviour {

		[SerializeField] TextMeshProUGUI levelProgression;

		private void Update() {
			levelProgression.text = (LevelHandler.s_instance.currentLevel + 1) + "/" + (LevelHandler.s_instance.maxLevel + 1);
		}
	}
}
