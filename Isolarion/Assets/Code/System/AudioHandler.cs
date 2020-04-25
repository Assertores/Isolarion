using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

namespace Iso {
	public class AudioHandler : Singleton<AudioHandler> {

		[Tooltip(
			" 0: shapeOnClick\n" +
			" 1: shapeOnReleace\n" +
			" 2: levelComplete\n" +
			" 3: levelFailed\n" +
			" 4: leaveLevel\n" +
			" 5: enterLevel")]
		[SerializeField] AudioSource[] sourceAudio;

		bool isReady = true;

		void Start() {
#if UNITY_EDITOR
			if(sourceAudio.Length < 6) {
				Debug.LogError("not the right amound of audio sources");
				isReady = false;
			}
#endif
		}

		public void PlayAudio(int index) {
			if(!isReady) {
				return;
			}

			StartCoroutine(IEPlayAudio(sourceAudio[index]));
		}

		IEnumerator IEPlayAudio(AudioSource original) {
			var element = Instantiate(original);

			element.Play();

			yield return new WaitForSeconds(element.clip.length);
			Destroy(element);
		}
	}
}
