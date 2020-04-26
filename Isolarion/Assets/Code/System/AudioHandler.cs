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
			" 5: enterLevel\n" +
			" 6: diceImpackt\n" +
			" 7: buttonClick")]
		[SerializeField] AudioSource[] sourceAudio;

		[SerializeField] AudioSource[] sourceMusic;

		bool isReady = true;

		void Start() {
#if UNITY_EDITOR
			if(sourceAudio.Length < 8) {
				Debug.LogError("not the right amound of audio sources");
				isReady = false;
			}

			if(sourceMusic.Length <= 0) {
				Debug.LogWarning("no music to play");
			}
#endif

			// TODO: find a better spot for this
			PlayMusic(true);
		}

		public AudioSource PlayAudio(int index) {
			if(!isReady) {
				return null;
			}

			var element = Instantiate(sourceAudio[index]);
			StartCoroutine(IEPlayAudio(element));
			return element;
		}

		public void PlayMusic(bool start) {
			if(sourceMusic.Length <= 0) {
				return;
			}

			if(start) {
				StartCoroutine(IEPlayMusic());
			} else {
				StopCoroutine(IEPlayMusic());
				foreach(var it in sourceMusic) {
					it.Stop();
				}
			}
		}

		IEnumerator IEPlayAudio(AudioSource element) {
			element.Play();

			yield return new WaitForSeconds(element.clip.length);
			while(element.isPlaying) {
				yield return new WaitForSeconds(0.5f);
			}
			Destroy(element.gameObject);
		}

		IEnumerator IEPlayMusic() {
			while(true) {

				var currentAudioSource = sourceMusic[Random.Range(0, sourceMusic.Length)];
				currentAudioSource.Play();
				yield return new WaitForSeconds(currentAudioSource.clip.length);
			}
		}
	}
}
