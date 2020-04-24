using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

namespace Iso {
	public class GlobalVariables : Singleton<GlobalVariables> {

		public float pathHight = 0.2f;
		public float feedbackLineLifetime = 1.0f;
		public GameObject feedbackLinePrefab;
	}
}
