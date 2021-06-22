using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public int health = 40;
	public int healthMax = 4;
	public GameObject salvageObj;
	public Sprite healthy;
	public Sprite hurt;
	public Sprite almostDead;
	public float enemyFlashDelta = 0.1f;
	private Animator anim;
	private bool isDead;
	//Audio stuff
	public AudioClip hitSFX;
	public AudioSource hitSource;

	// Use this for initialization
	void Start () {
		//Audio
		hitSource.clip = hitSFX;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0 && !isDead) {
			isDead = true;
			StartCoroutine(EnemyDeath ());
		}
	}

	public void DealDamage(int damage)
	{
		health -= damage;

		UpdateSprite ();
		StartCoroutine (EnemyFlashDamaged ());
		//Audio
		hitSource.Play();
	}

	public void UpdateSprite()
	{
		if (health == healthMax) {
			anim.SetBool("Full", true);
			anim.SetBool("Healthy", true);
			anim.SetBool ("Hurt", false);
			anim.SetBool ("AlmostDead", false);
		}
		if (health >= healthMax * 0.75f) {
			//change sprite to healthy ship
			anim.SetBool("Full", true);
			anim.SetBool("Healthy", true);
			anim.SetBool ("Hurt", false);
			anim.SetBool ("AlmostDead", false);

		} else if (health >= healthMax * 0.25f) {
			//change sprite to slightly unhealthier ship
			anim.SetBool("Full", true);
			anim.SetBool("Healthy", true);
			anim.SetBool ("Hurt", true);
			anim.SetBool ("AlmostDead", false);

		} else if (health >= 0) {
			//Shit's on fire.
			anim.SetBool("Full", true);
			anim.SetBool("Healthy", true);
			anim.SetBool ("Hurt", true);
			anim.SetBool ("AlmostDead", true);
		}
	}

	public IEnumerator EnemyDeath()
	{
		//death animation 
		/*
		if (transform.tag == "Enemy") {
			
			anim.SetTrigger ("Dead");
		
			GetComponent<EnemyMove> ().enabled = false;
		}*/
		anim.SetTrigger ("Dead");
		GetComponent<BoxCollider2D> ().enabled = false;
		if (gameObject.name == "Barrel") {
			yield return new WaitForSeconds (0.0f);
		} else {
			yield return new WaitForSeconds (2.5f);
		}
		GameObject newSalvageObj = Instantiate(salvageObj, transform.position, Quaternion.identity);
		//testing an idea
		gameObject.SetActive(false);
		//Destroy(gameObject);
	}

	public IEnumerator EnemyFlashDamaged()
	{
		float maxAlpha = 1.0f;
		Color damageColor;
		damageColor = Color.red;

		for (int i = 0; i < 1; i++) {
			for (int j = 0; j < 5; j++) {
				damageColor.a = damageColor.a - ((maxAlpha - enemyFlashDelta) / 5);
				GetComponent<SpriteRenderer> ().color = damageColor;
				yield return new WaitForSeconds (0.01f);
			}

			for (int j = 0; j < 5; j++) {
				damageColor.a = damageColor.a - ((maxAlpha - enemyFlashDelta) / 5);
				GetComponent<SpriteRenderer> ().color = damageColor;
				yield return new WaitForSeconds (0.01f);
			}
		}

		GetComponent<SpriteRenderer> ().color = Color.white;
	}
}
