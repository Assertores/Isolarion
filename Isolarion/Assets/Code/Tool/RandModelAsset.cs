using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsserTOOLres {
	[System.Serializable]
	public struct RanTexElement {
		public Texture _albedo;
		public Texture _hight;
	}

	[CreateAssetMenu(fileName = "RanTexAsset", menuName = "Game/RandModel")]
	public class RandModelAsset : ScriptableObject {
		public Mesh[] _meshs;
		public RanTexElement[] _ranTexs;
	}
}
