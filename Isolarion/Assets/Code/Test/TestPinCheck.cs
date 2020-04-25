using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class TestPinCheck : MonoBehaviour {
		private void Start() {
			StartCoroutine(DelayStart());
		}

		IEnumerator DelayStart() {
			yield return new WaitForSeconds(1.0f);
			Debug.Log("pin test was " + (PinHandle.CheckPins() ? "successfull" : "a failer"));
		}
	}
}
