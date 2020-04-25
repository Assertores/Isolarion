using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iso {
	public class CandleFX : MonoBehaviour {

		[SerializeField] Light light;
		[SerializeField] Transform flame;

		[SerializeField] AnimationCurve duration;

		[SerializeField] AnimationCurve intansity;
		[SerializeField] Gradient colorSpace;
		[SerializeField] AnimationCurve scale;

		bool isInIntensityChange = false;
		bool isInColorChange = false;
		bool isInScaleChange = false;

		void Start() {
			if(!light) {
				Debug.LogWarning("no light reference set.");
				light = GetComponentInChildren<Light>();
				if(!light) {
					Debug.LogError("FATAL: no light reference found.");
					Destroy(this);
					return;
				}
			}
		}

		void Update() {
			if(!isInIntensityChange) {
				StartCoroutine(ChangeLightIntensity(
					intansity.Evaluate(Random.Range(0.0f, 1.0f)),
					duration.Evaluate(Random.Range(0.0f, 1.0f))
				));
			}
			if(!isInColorChange) {
				StartCoroutine(ChangeLightColor(
					colorSpace.Evaluate(Random.Range(0.0f, 1.0f)),
					duration.Evaluate(Random.Range(0.0f, 1.0f))
				));
			}
			if(!isInScaleChange) {
				StartCoroutine(ChangeScale(
					flame,
					scale.Evaluate(Random.Range(0.0f, 1.0f)),
					duration.Evaluate(Random.Range(0.0f, 1.0f))
				));
			}
		}

		IEnumerator ChangeLightIntensity(float newIntensity, float duration, float starTimer = 0.0f) {
			isInIntensityChange = true;
			yield return new WaitForSeconds(starTimer);

			float startIntensity = light.intensity;
			float startTime = Time.time;

			while(startTime + duration > Time.time) {
				light.intensity = Mathf.Lerp(startIntensity, newIntensity, (Time.time - startTime) / duration);
				yield return null;
			}
			light.intensity = newIntensity;

			isInIntensityChange = false;
		}

		IEnumerator ChangeLightColor(Color newColor, float duration, float starTimer = 0.0f) {
			isInColorChange = true;
			yield return new WaitForSeconds(starTimer);

			float startH, startS, startV;
			Color.RGBToHSV(light.color, out startH, out startS, out startV);
			float endH, endS, endV;
			Color.RGBToHSV(newColor, out endH, out endS, out endV);
			float startTime = Time.time;

			while(startTime + duration > Time.time) {
				float delta = (Time.time - startTime) / duration;

				float currentH = Mathf.Lerp(startH, endH, delta);
				float currentS = Mathf.Lerp(startS, endS, delta);
				float currentV = Mathf.Lerp(startV, endV, delta);

				light.color = Color.HSVToRGB(currentH, currentS, currentV);
				yield return null;
			}
			light.color = newColor;

			isInColorChange = false;
		}

		IEnumerator ChangeScale(Transform element, float newScale, float duration, float starTimer = 0.0f) {
			isInScaleChange = true;
			yield return new WaitForSeconds(starTimer);

			float startScale = element.localScale.x;
			float startTime = Time.time;

			while(startTime + duration > Time.time) {
				float currentScale = Mathf.Lerp(startScale, newScale, (Time.time - startTime) / duration);
				element.localScale = new Vector3(currentScale, currentScale, currentScale);
				yield return null;
			}
			element.localScale = new Vector3(newScale, newScale, newScale);

			isInScaleChange = false;
		}
	}
}
