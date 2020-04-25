﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

namespace Iso {
	public class InputSystem : Singleton<InputSystem> {

		[SerializeField] LayerMask shapeMask;
		[SerializeField] LayerMask curserMask;
		[SerializeField] LayerMask flickMask;
		[SerializeField] float flickForce = 1.0f;
		[SerializeField] float rotationSpeed = 1.0f;

		GameObject currentObject;
		Vector3 offset;

		void Start() {

		}

		// Update is called once per frame
		void Update() {
			if(Input.GetMouseButtonDown(0)) {
				offset = GetShape(out currentObject);
				// absolutly on the wrong place but my curren system forces me to do it that way
				if(!currentObject) {
					if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
						out RaycastHit flickHit,
						1000.0f,
						flickMask)) {
						Vector3 force = Random.insideUnitSphere.normalized;
						force.y = Mathf.Abs(force.y);
						flickHit.rigidbody.AddForce(force * flickForce, ForceMode.Impulse);
					}
				}
			} else if(Input.GetMouseButtonUp(0)) {
				currentObject = null;
			}

			if(!currentObject) {
				return;
			}

			// TODO: only if mous position changed
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

				target = FindShapeParant(hit.collider.gameObject);
				if(!target) {
					return Vector3.zero;
				}

				Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
				out hit,
				1000.0f,
				curserMask);

				return target.transform.position - hit.point;
			}
			return Vector3.zero;
		}

		GameObject FindShapeParant(GameObject child) {
			for(GameObject element = child; element.transform != child.transform.root; element = element.transform.parent.gameObject) {
				if(element.GetComponent<ShapeHandle>()) {
					return element;
				}
			}
			return child.transform.root.gameObject.GetComponent<ShapeHandle>() ? child.transform.root.gameObject : null;
		}
	}
}
