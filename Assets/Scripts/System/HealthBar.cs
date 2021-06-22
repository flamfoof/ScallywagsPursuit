using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	[SerializeField]
	private float fillAmount;

	[SerializeField]
	private Image bar;

	private float currentHealth;
	private float maxHealth;


	public GameObject player;
	// Use this for initialization
	void Start () {
		maxHealth = player.GetComponent<PlayerHealth> ().healthMax;
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth = player.GetComponent<PlayerHealth> ().health;
		BarUpdate ();
	}
	//Updates the bar dynamically
	private void BarUpdate() {

		fillAmount = Conversion (currentHealth, 0, maxHealth, 0, 1);
		bar.fillAmount = fillAmount;
	}
	//Scaling for any max hp
	private float Conversion(float value, float inMin, float inMax, float outMin, float outMax) {

		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
