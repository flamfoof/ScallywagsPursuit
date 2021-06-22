using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainCheck : MonoBehaviour {

	[SerializeField]
	private bool hasCapn = false;

	public GameObject Player;
	public GameObject Captain;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		hasCapn = Player.GetComponent<PlayerInteraction>().hasCapn;
		if (hasCapn == true) {
			Captain.SetActive (true);
		} else {
			Captain.SetActive (false);
		}
	}
}
