using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

namespace Iso {
	public class LevelHandler : Singleton<LevelHandler> {

		[SerializeField] LevelData[] levels;
		[SerializeField] Transform levelHolder;
		[SerializeField] GameObject p_pin;
		[SerializeField] Transform shapeSpawnPosition;

		int currentLevel = 0;

		void Start() {
#if UNITY_EDITOR
			if(!p_pin) {
				Debug.LogError("FATAL: no pin reference set.");
				Destroy(this);
				return;
			}
			if(levels.Length <= 0) {
				Debug.LogError("FATAL: no levels to play.");
				Destroy(this);
				return;
			}
			for(int i = 0; i < levels.Length; i++) {
				if(!levels[i]) {
					Debug.LogError("FATAL: level " + i + "was a null reference.");
					Destroy(this);
					return;
				}
			}
#endif

			if(!levelHolder) {
				Debug.LogWarning("no level Holder specifyed.");
				levelHolder = new GameObject("AUTO_LevelHolder").transform;
			}
			if(!shapeSpawnPosition) {
				Debug.LogWarning("no spawn location specifyed.");
				shapeSpawnPosition = levelHolder;
			}

			SerialiceLevel(levels[0]);
		}

		public bool NextLevel() {
			currentLevel++;
			if(currentLevel < levels.Length) {
				SerialiceLevel(levels[currentLevel]);
				return true;
			}
			return false;
		}

		void SerialiceLevel(LevelData level) {
			foreach(Transform it in levelHolder) {
				Destroy(it.gameObject);
			}

			foreach(var it in level.Pins) {
				Instantiate(p_pin, levelHolder).transform.position = new Vector3(it.x, 0.0f, it.y);
			}

			foreach(var it in level.Shapes) {
				Instantiate(it, levelHolder).transform.position = shapeSpawnPosition.position;
			}
		}
	}
}
