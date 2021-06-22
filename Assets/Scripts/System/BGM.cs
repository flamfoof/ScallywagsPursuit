using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

	public GameObject player;
	public AudioSource death;
	public AudioClip deathClip;
	public AudioSource bgm;

	// Use this for initialization
	void Start () {
		death.clip = deathClip;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<PlayerHealth> ().isDead == true) {
			bgm.Pause ();
			bgm.clip = deathClip;
			bgm.Play ();
		}
	}
}
