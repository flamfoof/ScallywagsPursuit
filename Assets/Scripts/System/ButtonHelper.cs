using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHelper : MonoBehaviour {

	Quaternion rotation;
	// Use this for initialization
	void Awake () {
		rotation = transform.rotation;
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.rotation = rotation;
	}
}
