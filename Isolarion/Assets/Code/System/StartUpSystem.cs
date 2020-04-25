using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class StartUpSystem : MonoBehaviour {

		[SerializeField] Transform startCamPos;
		[SerializeField] float AnimTime = 1.0f;
		[SerializeField] AnimationCurve AnimCurve;
		[SerializeField] float MaxWaitTime = 30.0f;

		Vector3 targetPosition;
		Quaternion targetRotation;
		float startTime;
		bool isTriggered;

		void Start() {
			targetPosition = Camera.main.transform.position;
			targetRotation = Camera.main.transform.rotation;
			startTime = Time.time;

			Camera.main.transform.position = startCamPos.position;
			Camera.main.transform.rotation = startCamPos.rotation;

			GlobalVariables.s_instance.isInTransition = true;
		}

		void Update() {
			if(isTriggered || Time.time - startTime < MaxWaitTime) {
				return;
			}

			TriggerAnimation();
		}

		public void TriggerAnimation() {
			isTriggered = true;
			StartCoroutine(IEAnim());
		}

		IEnumerator IEAnim() {
			startTime = Time.time;

			while(startTime + AnimTime > Time.time) {
				float currentValue = AnimCurve.Evaluate((Time.time - startTime) / AnimTime);
				Camera.main.transform.position = Vector3.Lerp(startCamPos.position, targetPosition, currentValue);
				Camera.main.transform.rotation = Quaternion.Lerp(startCamPos.rotation, targetRotation, currentValue);
				yield return null;
			}
			Camera.main.transform.position = targetPosition;
			Camera.main.transform.rotation = targetRotation;

			CameraRotator.s_instance.Activate(targetRotation);
			LevelHandler.s_instance.StartLevel();
			Destroy(this);
		}
	}
}
