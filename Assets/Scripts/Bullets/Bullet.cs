using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float bulletLife = 5.0f;
	public int damageToEnemy = 8;
	public int damageToPlayer = 10;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, bulletLife);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Enemy" && transform.tag == "PlayerBullet") {
			other.transform.GetComponent<EnemyHealth> ().DealDamage (damageToEnemy);
			Destroy (gameObject);
		} else if (other.transform.tag == "Player" && transform.tag == "EnemyBullet") {
			other.transform.GetComponent<PlayerHealth> ().DealDamage (damageToPlayer);
			Destroy (gameObject);
		} else if (other.transform.tag == "Island") {
			Destroy (gameObject);
		}
	}

}
