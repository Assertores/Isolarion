using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;
using UnityEngine.AI;

namespace Iso {
	public class PinHandle : Multiton<PinHandle> {

		public static bool CheckPins() {

			for(int i = 0; i < s_references.Count; i++) {
				for(int j = i + 1; j < s_references.Count; j++) {
					NavMeshPath path = new NavMeshPath();
					if(NavMesh.CalculatePath(s_references[i].transform.position, s_references[j].transform.position, NavMesh.AllAreas, path)) {
						if(path.corners[path.corners.Length - 1].x == s_references[j].transform.position.x &&
							path.corners[path.corners.Length - 1].z == s_references[j].transform.position.z) {
							path.corners[0].y = GlobalVariables.s_instance.pathHight;
							for(int k = 1; k < path.corners.Length; k++) {
								path.corners[k].y = GlobalVariables.s_instance.pathHight;
								s_references[i].StartCoroutine(s_references[i].HandleFeedbackLine(path.corners[k - 1], path.corners[k]));
							}
							AudioHandler.s_instance.PlayAudio(3); // levelFailed
							return false;
						}
					}
				}
			}
			AudioHandler.s_instance.PlayAudio(2); // levelComplete
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
