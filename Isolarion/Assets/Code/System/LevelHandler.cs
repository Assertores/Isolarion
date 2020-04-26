using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;
using System;

namespace Iso {
	public class LevelHandler : Singleton<LevelHandler> {

		public static Action OnLevelBegin;

		[SerializeField] LevelData[] levels;
		[SerializeField] Transform levelHolder;
		[SerializeField] GameObject p_pin;
		[SerializeField] Transform shapeSpawnPosition;
		[SerializeField] float outTransitionTime = 0.5f;
		[SerializeField] AnimationCurve outTransitionScale;
		[SerializeField] float inTransitionTime = 0.5f;
		[SerializeField] AnimationCurve inTransitionScale;
		[SerializeField] bool resetProggres = false;
		[SerializeField] float spawnRandomPositionDeviation = 0.2f;
		[SerializeField] Animation endscreenAnimation;

		public int currentLevel { get; private set; } = 0;
		public int maxLevel { get => levels.Length - 1; }

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
			if(resetProggres) {
				PlayerPrefs.SetInt("currentLevel", 0);
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

			currentLevel = PlayerPrefs.GetInt("currentLevel");
		}

		private void OnDestroy() {
			PlayerPrefs.SetInt("currentLevel", currentLevel);
		}

		public void StartLevel() {
			if(currentLevel < levels.Length) {
				StartCoroutine(IELevelChangeIn(levels[currentLevel]));
			}
		}

		public bool NextLevel() {
			currentLevel++;
			if(currentLevel >= levels.Length) {
				GlobalVariables.s_instance.isInTransition = true;
				endscreenAnimation.Play();
				return false;
			}
			StartCoroutine(IELevelChangeOut(levels[currentLevel]));
			return true;
		}

		public void ResetLevel() {
			PlayerPrefs.SetInt("currentLevel", 0);
			currentLevel = 0;
			StartCoroutine(IELevelChangeOut(levels[currentLevel]));
		}

		IEnumerator IELevelChangeOut(LevelData nextLevel) {
			GlobalVariables.s_instance.isInTransition = true;
			float startTime = Time.time;

			AudioHandler.s_instance.PlayAudio(4); // leaveLevel

			// TODO: seamce computationally intensive. may use animations insted
			while(startTime + outTransitionTime > Time.time) {
				float scale = outTransitionScale.Evaluate((Time.time - startTime) / outTransitionTime);
				foreach(Transform it in levelHolder) {
					it.localScale = new Vector3(scale, scale, scale);
				}
				yield return null;
			}

			DestroyLevel();
			StartCoroutine(IELevelChangeIn(nextLevel));
		}

		IEnumerator IELevelChangeIn(LevelData nextLevel) {
			GlobalVariables.s_instance.isInTransition = true;
			float startTime = Time.time;

			AudioHandler.s_instance.PlayAudio(5); // enterLevel

			List<GameObject> pins;
			List<GameObject> shapes;
			SerialiceLevel(nextLevel, out pins, out shapes);

			// TODO: seamce computationally intensive. may use animations insted
			while(startTime + inTransitionTime > Time.time) {
				float scale = inTransitionScale.Evaluate((Time.time - startTime) / inTransitionTime);
				foreach(var it in pins) {
					it.transform.localScale = new Vector3(scale, scale, scale);
				}
				foreach(var it in shapes) {
					it.transform.localScale = new Vector3(scale, scale, scale);
				}
				yield return null;
			}

			foreach(var it in pins) {
				it.transform.localScale = Vector3.one;
			}
			foreach(var it in shapes) {
				it.transform.localScale = Vector3.one;
			}

			OnLevelBegin?.Invoke();

			GlobalVariables.s_instance.isInTransition = false;
		}

		void DestroyLevel() {
			foreach(Transform it in levelHolder) {
				Destroy(it.gameObject);
			}
		}

		void SerialiceLevel(LevelData level, out List<GameObject> pins, out List<GameObject> shapes) {
			pins = new List<GameObject>();
			shapes = new List<GameObject>();
			foreach(var it in level.Pins) {
				var element = Instantiate(p_pin, levelHolder);
				element.transform.position = new Vector3(it.x, 0.0f, it.y);
				element.transform.localScale = Vector3.zero;
				element.transform.Rotate(transform.up, Random.Range(-180, 180));
				pins.Add(element);
			}

			foreach(var it in level.Shapes) {
				var element = Instantiate(it, levelHolder);

				var pos = shapeSpawnPosition.position;
				var rand = Random.insideUnitCircle * spawnRandomPositionDeviation;
				pos.x += rand.x;
				pos.z += rand.y;

				element.transform.position = pos;
				element.transform.localScale = Vector3.zero;
				element.transform.Rotate(transform.up, Random.Range(-180, 180));
				shapes.Add(element);
			}
		}
	}
}
