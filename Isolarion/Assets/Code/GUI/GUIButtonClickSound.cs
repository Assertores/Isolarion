using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class GUIButtonClickSound : MonoBehaviour {
		public void Execute() {
			AudioHandler.s_instance.PlayAudio(7); // buttonClick
		}
	}
}
