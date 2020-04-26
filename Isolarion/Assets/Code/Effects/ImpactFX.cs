using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	[RequireComponent(typeof(Collider))]
	public class ImpactFX : MonoBehaviour {
		private void OnCollisionEnter(Collision collision) {
			if(Time.timeSinceLevelLoad < 0.2f) {
				return;
			}
			AudioHandler.s_instance.PlayAudio(6); // diceImpackt
		}
	}
}
