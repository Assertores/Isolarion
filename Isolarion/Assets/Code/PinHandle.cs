using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

namespace Iso {
	public class PinHandle : Multiton<PinHandle> {

		public Transform r_Head;

		void Start() {
			if(!r_Head) {
				Debug.LogError("FATAL: No Head Reference set.");
				Destroy(this);
				return;
			}
		}

		void Update() {

		}

		public static bool CheckPins() {
			foreach(var it in s_references) {
				if(!it.CheckPin()) {
					return false;
				}
			}

			return true;
		}

		bool CheckPin() {
			foreach(var it in s_references) {
				if(it == this) {
					continue;
				}

				if(!Physics.Linecast(r_Head.position, it.r_Head.position, GlobalVariables.s_instance.pinCheckMask)) {
					StartCoroutine(HandleFeedbackLine(r_Head.position, it.r_Head.position));
					return false;
				}
			}

			return true;
		}

		IEnumerator HandleFeedbackLine(Vector3 start, Vector3 end) {
			GameObject line = Instantiate(GlobalVariables.s_instance.feedbackLinePrefab);

			float size = (start - end).magnitude;
			line.transform.position = start;
			line.transform.rotation = Quaternion.LookRotation(end - start, transform.up);
			line.transform.localScale = new Vector3(size, size, size);

			yield return new WaitForSeconds(GlobalVariables.s_instance.feedbackLineLifetime);
			Destroy(line);
		}
	}
}
