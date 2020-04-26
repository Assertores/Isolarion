using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class GUISkip : MonoBehaviour {
		public void Execute() {
			if(LevelHandler.s_instance.NextLevel()) {
				return;
			}

			Debug.Log("finished Last Level.");
		}
	}
}
