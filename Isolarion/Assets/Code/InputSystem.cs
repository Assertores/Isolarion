﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

namespace Iso {
	public class InputSystem : Singleton<InputSystem> {

		[SerializeField] LayerMask shapeMask;
		[SerializeField] LayerMask curserMask;
		[SerializeField] float rotationSpeed = 1.0f;

		GameObject currentObject;
		Vector3 offset;

		void Start() {

		}

		// Update is called once per frame
		void Update() {
			if(Input.GetMouseButtonDown(0)) {
				offset = GetShape(out currentObject);
			} else if(Input.GetMouseButtonUp(0)) {
				currentObject = null;
			}

			if(!currentObject) {
				return;
			}

			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
			out RaycastHit hit,
			1000.0f,
			curserMask)) {

				currentObject.transform.position = hit.point + offset;
			}

			currentObject.transform.Rotate(transform.up, Input.mouseScrollDelta.y * rotationSpeed);
		}

		Vector3 GetShape(out GameObject target) {
			target = null;
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
				out RaycastHit hit,
				1000.0f,
				shapeMask)) {

				// TODO: evel stuff dont yous root
				target = hit.collider.transform.root.gameObject;

				Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
				out hit,
				1000.0f,
				curserMask);
				return target.transform.position - hit.point;
			}
			return Vector3.zero;
		}
	}
}
