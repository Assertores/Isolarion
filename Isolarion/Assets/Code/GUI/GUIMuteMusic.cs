using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

namespace Iso {
	public class GUIMuteMusic : MonoBehaviour {

		[SerializeField] AudioMixer _mixer;
		[SerializeField] TextMeshProUGUI text;
		[SerializeField] Color offColor;

		float startValue;
		bool isMuted = false;
		Color normalColor;

		private void Start() {
			_mixer.GetFloat("MusicVolume", out startValue);
			normalColor = text.color;
		}

		public void Execute() {
			isMuted = !isMuted;
			_mixer.SetFloat("MusicVolume", isMuted ? -80 : startValue);
			text.color = isMuted ? offColor : normalColor;
		}
	}
}
