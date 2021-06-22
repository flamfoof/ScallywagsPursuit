using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonCD : MonoBehaviour {

	[SerializeField]
	private float cooldown = 0.5f;
	[SerializeField]
	private float cooldownTimer;

	public bool cooling = false;
	[SerializeField]
	private float fillAmount;

	[SerializeField]
	private Image icon;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		cooldown = player.GetComponent<PlayerShoot> ().delayShootTime;
		fillAmount = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if (cooldownTimer > 0) {
			cooldownTimer -= Time.deltaTime;
		}

		if (cooldownTimer < 0) {
			cooldownTimer = 0;
			cooling = false;
		}

		fillAmount = Mathf.Abs((cooldownTimer / cooldown) - 1);
		icon.fillAmount = fillAmount;
	}

	public void CballCDOn()
	{
		if (cooldownTimer == 0) {
			if (player.GetComponent<PlayerShoot> ().canShoot) {
				cooldownTimer = cooldown;
				fillAmount = 0;
				cooling = true;
			}
		}
	}
}
