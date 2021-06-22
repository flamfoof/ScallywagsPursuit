using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	public float speed = 10.0f;
	public float angle = 0.0f;
	public float dragDefault = 1.0f;
	public float decel = 1.0f;
	public bool canMove = true;
	public float rotationLerpSpeed = 5.0f;
	public Vector2 vel;

	public GameObject islandMenu;
	public GameObject pauseMenu;
	public GameObject enemyMenu;
	public GameObject salvageMenu;

	private GameObject textbox;
	private bool isRunning = false;
	private bool diagonalPush = false;
	public Vector2 dir;
	private Rigidbody2D rb;

	//Audio
	public AudioClip moveSFX;
	public AudioSource moveSource;

	void Start () {
		angle = 0.0f;
		rb = GetComponent<Rigidbody2D> ();
		if (GameObject.FindGameObjectWithTag ("Dialogue") != null) {
			textbox = GameObject.FindGameObjectWithTag ("Dialogue");
		}
		textbox.SetActive (false);

		//Audio
		moveSource.clip = moveSFX;
	}

	void FixedUpdate () {
		float horizMove = Input.GetAxisRaw ("Horizontal");
		float vertMove = Input.GetAxisRaw ("Vertical");
		float horizDMove = Input.GetAxis ("DPadMoveHoriz");
		float vertDMove = Input.GetAxis ("DPadMoveVert");
		Vector2 moveDir = (Vector2) (Quaternion.Euler (0, 0, angle) * Vector2.up);
		/*
		if (horizMove >= 0.5) {
			horizMove = Mathf.CeilToInt (horizMove);
		} else if (horizMove <= -0.5) {
			horizMove = Mathf.Floor (horizMove);
		} else {
			horizMove = 0;
		}

		if (vertMove >= 0.5) {
			vertMove = Mathf.CeilToInt (vertMove);
		} else if (vertMove <= -0.5) {
			vertMove = Mathf.Floor (vertMove);
		} else {
			vertMove = 0;
		}

		if (horizDMove >= 0.5) {
			horizDMove = Mathf.CeilToInt (horizDMove);
		} else if (horizDMove <= -0.5) {
			horizDMove = Mathf.Floor (horizDMove);
		} else {
			horizDMove = 0;
		}

		if (vertDMove >= 0.5) {
			vertDMove = Mathf.CeilToInt (vertDMove);
		} else if (vertDMove <= -0.5) {
			vertDMove = Mathf.Floor (vertDMove);
		} else {
			vertDMove = 0;
		}*/

		dir = new Vector2 (horizDMove, vertDMove);
		if (dir == Vector2.zero) {
			dir = new Vector2(horizMove, vertMove);
		}
		/*
		Debug.Log ("Horizontal: " + horizMove);
		Debug.Log ("Vertical: " + vertMove);
		Debug.Log ("Horizontal D: " + horizDMove);
		Debug.Log ("Vertical D: " + vertDMove);
		Debug.Log (dir);
		*/

//		Debug.Log (dir);
//		vel = moveDir;
		//Vector3 diff = transform.InverseTransformPoint(dir);
		//angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

		/*
		if ((horizMove != 0 && vertMove != 0) || (horizDMove != 0 && vertDMove != 0)) {
			if (dir.x == 1 && dir.y == 1) {
				angle = 315.0f;
			}
			if (dir.x == -1 && dir.y == 1) {
				angle = 45.0f;
			}
			if (dir.x == -1 && dir.y == -1) {
				angle = 135.0f;
			}
			if (dir.x == 1 && dir.y == -1) {
				angle = 225.0f;
			}
			diagonalPush = true;
			if(!isRunning)
				StartCoroutine (StayTilted ());
		} else {
			if (!diagonalPush) {
				if (dir.x == 1) {
					angle = 270.0f;
				}
				if (dir.x == -1) {
					angle = 90.0f;
				}
				if (dir.y == 1) {
					angle = 0.0f;
				}
				if (dir.y == -1) {
					angle = 180.0f;
				}
			}
		}*/
		//Debug.Log(transform.localRotation.z);
//		Debug.Log (angle);
		if (dir != Vector2.zero && canMove) {
			angle = (Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg) - 90;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationLerpSpeed * Time.deltaTime);
			/* All this down here are all failure attempts before reaching these two lines of successful codes........
			//float rotation = Mathf.LerpAngle (transform.rotation.z, angle, Time.deltaTime);
			//Vector3 angleRot = new Vector3(0, 0, angle);
			//Quaternion rotation = Quaternion.LookRotation(angleRot, Vector2.up);
			//Debug.Log ("Rotation is: " + transform.forward);
//			Debug.Log ("rotation is: " + transform.rotation.z);
			//transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationLerpSpeed * Time.deltaTime);
			//rb.MoveRotation (rb.rotation + (Time.deltaTime * (rotation * rotationLerpSpeed)));
			//rb.AddTorque (rotation.z);
			*/

		}
		if (islandMenu.activeSelf || pauseMenu.activeSelf || enemyMenu.activeSelf || salvageMenu.activeSelf || GetComponent<PlayerHealth>().isDead || textbox.activeSelf) {
			
			canMove = false;
		} else {
			canMove = true;
		}
		if (canMove) {
			if (dir == Vector2.zero) {
				rb.AddForce(Vector2.zero);
				//Audio
				moveSource.Play();
			
			} else {
//				Debug.Log ("moving");
				rb.AddForce (transform.up * speed);
			}
		}
		//gameObject.transform.Translate(dir);

		if (vel == Vector2.zero) {
//			Debug.Log (vel);
			rb.AddForce (Vector3.zero);
			rb.drag = decel;
		} else {
			rb.drag = dragDefault;
		}
	}
	private IEnumerator StayTilted()
	{
		isRunning = true;
		yield return new WaitForSeconds (0.3f);
		diagonalPush = false;
		isRunning = false;
		//Debug.Log ("Done Tilting");
	}
	public float GetDirectionY()
	{
		return (float)dir.y;
	}
	public float GetDirectionX()
	{
		return (float)dir.x;
	}
}
