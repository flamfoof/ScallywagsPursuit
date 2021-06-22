﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMinimap : MonoBehaviour {


	public Transform player;

	// Update is called once per frame
	void LateUpdate () {
		Vector3 newPosition = player.position;
		newPosition.z = transform.position.z;
		transform.position = newPosition;

		transform.rotation = Quaternion.Euler (0, player.eulerAngles.y, 0);
	}
}
