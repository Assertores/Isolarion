using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;
using UnityEngine.AI;

namespace Iso {
	public class PinHandle : Multiton<PinHandle> {

		public static bool CheckPins() {
			foreach(var it in s_references) {
				if(!it.CheckPin()) {
					// TODO: play level wrong sound
					return false;
				}
			}
			// TODO: play level complete sound
			return true;
		}

		bool CheckPin() {
			foreach(var it in s_references) {
				if(it == this) {
					continue;
				}

				NavMeshPath path = new NavMeshPath();
				if(NavMesh.CalculatePath(transform.position, it.transform.position, NavMesh.AllAreas, path)) {
					if(path.corners[path.corners.Length-1].x == it.transform.position.x &&
						path.corners[path.corners.Length - 1].z == it.transform.position.z) {
						path.corners[0].y = GlobalVariables.s_instance.pathHight;
						for(int i = 1; i < path.corners.Length; i++) {
							path.corners[i].y = GlobalVariables.s_instance.pathHight;
							StartCoroutine(HandleFeedbackLine(path.corners[i - 1], path.corners[i]));
						}
						return false;
					}
				}
			}

			return true;
		}

		IEnumerator HandleFeedbackLine(Vector3 start, Vector3 end) {
			GameObject line = Instantiate(GlobalVariables.s_instance.feedbackLinePrefab);

			line.transform.position = start;
			line.transform.rotation = Quaternion.LookRotation(end - start, Vector3.up);
			line.transform.localScale = new Vector3(1.0f, 1.0f, (start - end).magnitude);

			yield return new WaitForSeconds(GlobalVariables.s_instance.feedbackLineLifetime);

			Destroy(line);
		}
	}
}
