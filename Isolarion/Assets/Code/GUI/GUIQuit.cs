using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class GUIQuit : MonoBehaviour {
		public void Execute() {
			Application.Quit();
#if UNITY_EDITOR
			Debug.Break();
#endif
		}
	}
}