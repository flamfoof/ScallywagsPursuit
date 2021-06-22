using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

	public PlayerInteraction player;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		player = player.GetComponent<PlayerInteraction> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && player.hasCapn) {
			Debug.Log ("player has the Capn");
			anim.SetBool ("playerNear", true);
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") {
			//			Debug.Log ("Ship leaving");
			anim.SetBool ("playerNear", false);
		}
	}
}
