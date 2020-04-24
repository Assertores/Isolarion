using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	[CreateAssetMenu(fileName = "Level", menuName = "Game/Level")]
	public class LevelData : ScriptableObject {
		public GameObject[] Shapes;
		public Vector2[] Pins;
	}
}