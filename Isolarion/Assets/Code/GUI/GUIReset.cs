using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class GUIReset : MonoBehaviour {
		public void Execute() {
			LevelHandler.s_instance.ResetLevel();
		}
	}
}