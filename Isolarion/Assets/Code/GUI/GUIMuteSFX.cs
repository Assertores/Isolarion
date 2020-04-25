using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

namespace Iso {
	public class GUIMuteSFX : MonoBehaviour {

		[SerializeField] AudioMixer mixer;
		[SerializeField] TextMeshProUGUI text;
		[SerializeField] Color offColor;

		float startValue;
		bool isMuted = false;
		Color normalColor;

		private void Start() {
			mixer.GetFloat("SFXVolume", out startValue);
			normalColor = text.color;
		}

		public void Execute() {
			isMuted = !isMuted;
			mixer.SetFloat("SFXVolume", isMuted ? -80 : startValue);
			text.color = isMuted ? offColor : normalColor;
		}
	}
}
