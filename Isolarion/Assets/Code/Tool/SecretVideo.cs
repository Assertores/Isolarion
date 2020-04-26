using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class SecretVideo : MonoBehaviour {

		[SerializeField] GameObject target;
		[SerializeField] GameObject[] antiTargets;
		[SerializeField] string commandLineArg;
		[SerializeField] bool ExecuteOnAwake;

		private void Awake() {
			if(!target) {
				Destroy(this);
				return;
			}
			target.SetActive(false);

			if(ExecuteOnAwake) {
				Execute();
			}
		}

		public void Execute() {
			if(commandLineArg == "") {
				Destroy(this);
				return;
			}
			foreach(string it in System.Environment.GetCommandLineArgs()) {
				if(it == commandLineArg) {
					target.SetActive(true);
					foreach(var jt in antiTargets) {
						jt?.SetActive(false);
					}
					break;
				}
			}

			Destroy(this);
		}
	}
}