using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public int capsizeDamage = 20;
	public bool inRange = false;
	public bool capsizeMode = false;
	public float bulletSpeed = 10;

	public GameObject cannon;
	public float delayBetweenShots = 0.0f;
	public Rigidbody2D bullets;
	private EnemyMove moveDir;
	public bool test;
	private GameObject player;
	private float currSpeed;
	private EnemyMove enemyMove;

	//Audio stuff
	public AudioClip shootSFX;
	public AudioSource shootSource;

	// Use this for initialization
	void Start () {
		moveDir = GetComponent<EnemyMove> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		currSpeed = GetComponent<EnemyMove> ().speed;
		enemyMove = GetComponent<EnemyMove> ();

		//Audio
		shootSource.clip = shootSFX;
	}
	
	// Update is called once per frame
	void Update () {
		if (test) {
			if (Input.GetKeyDown (KeyCode.T)) {
				SpawnBullet ();
			}
			Debug.Log ("enemy health: " + GetComponent<EnemyHealth> ().health);

		}
		if (GetComponent<EnemyHealth> ().health <= 2) {
			//Debug.Log ("HP IS LOW, CAPSIZE THAT BASTARD");
			capsizeMode = true;
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, currSpeed * Time.deltaTime * 0.3f);
		}

	}

	public IEnumerator FireCannon()
	{
		while (inRange && !capsizeMode) {
			yield return new WaitForSeconds (delayBetweenShots);
//			Debug.Log ("Pew pew");
			if (!player.GetComponent<PlayerHealth> ().isDead) {
				SpawnBullet ();

				//Audio
				shootSource.Play();
			}

		}
	}

	public void SpawnBullet()
	{
		
		Rigidbody2D projectile = Instantiate (bullets, cannon.transform.position, Quaternion.Euler (0.0f, 0.0f, moveDir.angle));
		projectile.transform.tag = "EnemyBullet";

		//projectile.AddRelativeForce (new Vector2 (0, bulletSpeed));
		//projectile.AddForce (transform.forward * bulletSpeed);
		projectile.AddRelativeForce (new Vector2 (0, bulletSpeed));
	}

	//not done yet
	public IEnumerator Capsize()
	{
		while (inRange) {
			yield return new WaitForSeconds (delayBetweenShots);
			GetComponent<PlayerHealth> ().DealDamage (capsizeDamage);

		}
	}

	public void StartFiring()
	{
//		Debug.Log ("started firing co routine");
		inRange = true;
		StartCoroutine (FireCannon ());
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			//SceneManager.LoadScene (1);
			other.GetComponent<PlayerHealth>().DealDamage(capsizeDamage);
			StartCoroutine(GetComponent<EnemyHealth> ().EnemyDeath ());
			//Destroy (gameObject);
		}

	}
}
