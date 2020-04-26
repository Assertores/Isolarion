using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class GUIActivateTimed : MonoBehaviour {
		[SerializeField] float delay = 180.0f;

		float startTime;
		bool isActive;

		private void Start() {
			LevelHandler.OnLevelBegin += ResetTimer;
			ResetTimer();
		}

		void ResetTimer() {
			startTime = Time.time;
			foreach(Transform it in transform) {
				it.gameObject.SetActive(false);
			}
			isActive = false;
		}

		private void FixedUpdate() {
			if(isActive) {
				return;
			}
			if(startTime + delay > Time.time) {
				return;
			}
			foreach(Transform it in transform) {
				it.gameObject.SetActive(true);
			}
			isActive = true;
		}
	}
}
