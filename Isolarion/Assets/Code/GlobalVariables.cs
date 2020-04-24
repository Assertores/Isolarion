using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

namespace Iso {
	public class GlobalVariables : Singleton<GlobalVariables> {

		public LayerMask pinCheckMask;
		public float feedbackLineLifetime = 1.0f;
		public GameObject feedbackLinePrefab;
	}
}
