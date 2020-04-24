using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class DoCheck : MonoBehaviour {
		public void Execute() {
			if(!PinHandle.CheckPins()) {
				return;
			}
			if(LevelHandler.s_instance.NextLevel()) {
				return;
			}

			Debug.Log("finished Last Level.");
		}
	}
}