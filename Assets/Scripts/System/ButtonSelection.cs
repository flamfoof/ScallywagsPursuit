using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelection : MonoBehaviour {

	public List<Button> buttons;
	int selection = 0;
	public int size = 0;
	bool pressed = false;
	PlayerMove direction;

	// Use this for initialization
	void Start () {
		size = buttons.Count;
		//direction = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMove>();
		//buttons [selection].Select ();
//		Debug.Log ("STARTING HOOO");
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetAxisRaw ("Vertical") != 0 || Input.GetAxisRaw ("DPadMoveVert") != 0) {
			
		}
		*/
		float horizMove = Input.GetAxisRaw ("Horizontal");
		float vertMove = Input.GetAxisRaw ("Vertical");
		float horizDMove = Input.GetAxis ("DPadMoveHoriz");
		float vertDMove = Input.GetAxis ("DPadMoveVert");

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
		}

		Vector2 dir = new Vector2 (horizDMove, vertDMove);
		if (dir == Vector2.zero) {
			dir = new Vector2(horizMove, vertMove);
		}
		if (dir.y != 0) {
			
			if (dir.y == 1.0f && !pressed) {
				selection--;
				if (selection <= -1) {
					selection = size - 1;
				}
				selection = Mathf.Abs (selection % size);
//				Debug.Log (selection % size);
				buttons [selection].Select ();
			} else if (dir.y == -1.0f && !pressed) {
				selection++;
				selection = Mathf.Abs (selection % size);
//				Debug.Log (selection % size);
				buttons [selection].Select ();
			}
			pressed = true;
		} else {
			pressed = false;
		}

	}

	void OnEnable()
	{
//		Debug.Log ("first button is selected");
		selection = 0;
		buttons [0].Select ();
		StartCoroutine (SelectFirstButton ());
	}
	IEnumerator SelectFirstButton()
	{
		yield return new WaitForFixedUpdate();
		buttons [0].OnSelect (null);
		selection = 0;
		buttons [0].Select ();
	}

}
