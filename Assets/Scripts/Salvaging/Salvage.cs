using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salvage : MonoBehaviour {


	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SalvageObject(GameObject objSalvage)
	{
		Destroy (objSalvage);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
//			Debug.Log ("Ship incoming");
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
