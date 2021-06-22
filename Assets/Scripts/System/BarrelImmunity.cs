using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelImmunity : MonoBehaviour {

	private GameObject tut;
	// Use this for initialization
	void Start () {
		tut = GameObject.FindGameObjectWithTag ("Tutorial");
	}
	
	// Update is called once per frame
	void Update () {
		if (!tut.GetComponent<TextBoxScript> ().playerMoved && GetComponent<EnemyHealth>().health < GetComponent<EnemyHealth>().healthMax) {
			GetComponent<EnemyHealth> ().health = GetComponent<EnemyHealth> ().healthMax;
			GetComponent<EnemyHealth> ().UpdateSprite ();
		}
	}
}
