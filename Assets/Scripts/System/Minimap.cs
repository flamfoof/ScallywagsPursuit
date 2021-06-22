using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

	public float zoomDist = 10.0f;
	public float maxZoomDist = 40.0f;
	public float minZoomDist = 6.0f;
	public float zoomSpeed = 0.5f;
	public float lerpZoomSpeed = 5.0f;
	public  Camera cam;
	public Transform player;

	//Controller camera support
	float inputValue;
	float yAxis;

	void start()
	{
		cam = GetComponent<Camera> ();
		cam.fieldOfView = zoomDist;
	}
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 newPosition = player.position;
		newPosition.z = transform.position.z;
		transform.position = newPosition;

		transform.rotation = Quaternion.Euler (0, player.eulerAngles.y, 0);

		//Controller camera support
		yAxis = Input.GetAxis ("RightTriggerMove");
		// Debug.LogError (yAxis);

		if (Input.GetAxis ("Mouse ScrollWheel") != 0.0f || yAxis != 0) {
			//Debug.Log("Mouse Wheel scroll: " + Input.GetAxis ("Mouse ScrollWheel"));

			if (Input.GetAxis ("Mouse ScrollWheel") != 0.0f) {
				inputValue = Input.GetAxis ("Mouse ScrollWheel");
			} else if (yAxis != 0) {
				inputValue = yAxis;
			} else {
				inputValue = 0;
			}
//			Debug.Log ("Asserted camera zoom value: " + inputValue);
			if (zoomDist <= minZoomDist && inputValue > 0.0f) {
				zoomDist = minZoomDist;
//				Debug.Log ("Zoomed In");
			} else if (zoomDist >= maxZoomDist && inputValue < 0.0f) {
				zoomDist = maxZoomDist;
//				Debug.Log ("Zoomed Out");
			} else {
				zoomDist -= inputValue * zoomSpeed;
			}
		}

		Zoom ();

	}

	void Zoom()
	{
		float lerpVal = zoomDist;
		cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, lerpVal, lerpZoomSpeed * Time.deltaTime);
		//Vector3 lerpPos = new Vector3 (transform.position.x, transform.position.y, zoomDist);
		//transform.position = Vector3.Lerp (transform.position, lerpPos, lerpZoomSpeed * Time.deltaTime);
	}
}
