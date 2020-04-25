using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

namespace Iso {
	public class CameraRotator : Singleton<CameraRotator> {

		[SerializeField] Transform menuPositioning;
		[SerializeField] GameObject TopButton;
		[SerializeField] GameObject BottomButton;
		[SerializeField] float rotateTime;

		Quaternion gameRotation;
		Quaternion menuRotation;

		void Start() {
			menuRotation = menuPositioning.rotation;

			TopButton.SetActive(false);
			BottomButton.SetActive(false);
		}

		public void Activate(Quaternion rot) {
			gameRotation = rot;
			TopButton.SetActive(true);
		}

		public void OnTopButtonPress() {
			GlobalVariables.s_instance.isInTransition = true;
			BottomButton.SetActive(true);
			TopButton.SetActive(false);
			StartCoroutine(LerpRotationTimed(Camera.main.transform, menuRotation, rotateTime));
		}

		public void OnBottomButtonPress() {
			BottomButton.SetActive(false);
			TopButton.SetActive(true);
			StartCoroutine(LerpRotationTimed(Camera.main.transform, gameRotation, rotateTime));
			StartCoroutine(DelaySetTransition(rotateTime, false));
		}

		IEnumerator LerpRotationTimed(Transform target, Quaternion end, float time) {
			float startTime = Time.time;
			Quaternion start = target.rotation;

			while(startTime + time > Time.time) {
				target.rotation = Quaternion.Lerp(start, end, (Time.time - startTime) / time);
				yield return null;
			}
			target.rotation = end;
		}

		IEnumerator DelaySetTransition(float delay, bool newValue) {
			yield return new WaitForSeconds(delay);

			GlobalVariables.s_instance.isInTransition = newValue;
		}
	}
}
