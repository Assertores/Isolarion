using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class GUIDoCheck : MonoBehaviour {
		public void Execute() {
			if(GlobalVariables.s_instance.isInTransition) {
				return;
			}
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