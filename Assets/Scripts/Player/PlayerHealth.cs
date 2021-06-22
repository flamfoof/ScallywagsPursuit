using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	public int health = 60;
	public int healthMax = 60;
	public float deathTimer = 6.0f;
	public Sprite healthy;
	public Sprite hurt;
	public Sprite almostDead;
	public bool isDead = false;
	public float playerFlashDelta = 0.1f;
	public int playerDeathSceneChange = 4;
	public LevelController levelCont;

	//Audio stuff
	public AudioClip hitSFX;
	public AudioSource hitSource;

    Animator anim;


    // Use this for initialization
    void Start () {
		//Audio
		hitSource.clip = hitSFX;
        anim = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			GetComponent<PlayerMove> ().canMove = false;
			StartCoroutine(PlayerDeath ());
		}
	}

	public void DealDamage(int damage)
	{
		health -= damage;
		UpdateSprite ();
		StartCoroutine (PlayerFlashDamaged ());
		//Audio
		hitSource.Play();
	}

	public void UpdateSprite()
	{
		if (health == healthMax) {
			anim.SetBool ("Full", true);
			anim.SetBool ("Healthy", false);
			anim.SetBool ("Hurt", false);
			anim.SetBool ("AlmostDead", false);
		} else if (health >= healthMax * 0.75f) {
			//change sprite to healthy ship
			GetComponent<SpriteRenderer>().sprite = healthy;
			anim.SetBool ("Full", true);
			anim.SetBool ("Healthy", true);
			anim.SetBool ("Hurt", false);
			anim.SetBool ("AlmostDead", false);

		} else if (health >= healthMax * 0.25f) {
			//change sprite to slightly unhealthier ship
			GetComponent<SpriteRenderer>().sprite = hurt;
			anim.SetBool ("Full", true);
			anim.SetBool ("Healthy", true);
			anim.SetBool ("Hurt", true);
			anim.SetBool ("AlmostDead", false);

		} else if (health >= 0) {
			//Shit's on fire.
			GetComponent<SpriteRenderer>().sprite = almostDead;
			anim.SetBool ("Full", true);
			anim.SetBool ("Healthy", true);
			anim.SetBool ("Hurt", true);
			anim.SetBool ("AlmostDead", true);

		}
	}
	public IEnumerator PlayerDeath()
	{
		isDead = true;
        anim.SetInteger("state", 3);
        yield return new WaitForSeconds (deathTimer);
		SceneManager.LoadScene(playerDeathSceneChange);
		//death animation or game over screen or something.
	}

	public IEnumerator PlayerFlashDamaged()
	{
		float maxAlpha = 1.0f;
		Color damageColor;
		damageColor = Color.red;

		for (int i = 0; i < 1; i++) {
			for (int j = 0; j < 10; j++) {
				GetComponent<SpriteRenderer> ().color = damageColor;
				yield return new WaitForSeconds (0.02f);
			}

			for (int j = 0; j < 10; j++) {
				GetComponent<SpriteRenderer> ().color = damageColor;
				yield return new WaitForSeconds (0.02f);
			}
		}

		GetComponent<SpriteRenderer> ().color = Color.white;
	}
}
