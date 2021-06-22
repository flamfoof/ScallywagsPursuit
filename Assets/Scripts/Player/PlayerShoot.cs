using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShoot : MonoBehaviour {
	
	public float bulletSpeed;
	public bool delay = false;
	public bool canShoot = true;
	public float delayShootTime = 0.5f;
	public GameObject cannonL;
	public GameObject cannonR;
	public Rigidbody2D bullets;
	private bool direction = false;
	private PlayerMove moveDir;
	private float angle;
	private float velY;
	private float velX;
	private GameObject cd;
	//Trigger firing support code
	float shoot;
    Animator anim;

	//Audio stuff
	public AudioClip shootSFX;
	public AudioSource shootSource;

	// Use this for initialization
	void Start () {
		moveDir = GetComponent<PlayerMove> ();
		cd = GameObject.FindGameObjectWithTag ("cd");
		StartCoroutine (DelayShooting ());

		//Audio
		shootSource.clip = shootSFX;

        anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.Z)) {
			Shoot ();
		}*/

		//Trigger firing support code
		if (GetComponent<PlayerMove> ().canMove) {
			canShoot = true;
		} else if (!GetComponent<PlayerMove> ().canMove) {
			canShoot = false;
		}

		shoot = Input.GetAxis ("Shoot");

//		Debug.LogError (shoot);
		if(canShoot && !cd.GetComponent<CannonCD>().cooling) {
//			Debug.Log ("canShoot: " + canShoot + "\nCannon CD: " + cd.GetComponent<CannonCD> ().cooling + "\nShoot value: " + shoot + "\nPlayer isdead?: " + GetComponent<PlayerHealth>().isDead);
//			Debug.Log ("Input right is pushed: " + Input.GetButtonDown ("ShootRight"));
			if ((shoot > 0 || Input.GetButtonDown("ShootLeft")) && !GetComponent<PlayerHealth>().isDead) {
				cd.GetComponent<CannonCD> ().CballCDOn ();
//				Debug.Log("Bullet function left");
				Shoot ("Left");
				//changed the anim parameters to add triggers. It's in the shoot function now.
                //anim.SetInteger("state",2);

			}
			if ((shoot < 0 || Input.GetButtonDown("ShootRight")) &&!GetComponent<PlayerHealth>().isDead ) {
				cd.GetComponent<CannonCD> ().CballCDOn ();
//				Debug.Log("Bullet function right");
				Shoot ("Right");
				//changed the anim parameters to add triggers. It's in the shoot function now.
                //anim.SetInteger("state",1);

             
            }
          
            //Debug.Log (cannonL.transform.rotation);
        }
	}
		

	void Shoot(string dir)
	{
		if (!delay) {
			delay = true;
			if (moveDir.dir != Vector2.zero) {
				angle = moveDir.angle;
			}
			if (dir == "Left") {
				anim.SetTrigger("FLeft");
				//Vector3 offsetL = new Vector3 (cannonL.transform.position.x, cannonL.transform.position.y, cannonL.transform.position.z); 
//				Debug.Log("Bullet has been instantiated");
				Rigidbody2D projectileL = Instantiate (bullets, cannonL.transform.position, Quaternion.Euler (0.0f, 0.0f, transform.eulerAngles.z + 90.0f));
				projectileL.transform.tag = "PlayerBullet";
//				Debug.Log (transform.eulerAngles.z + 90.0f);
				projectileL.AddRelativeForce (new Vector2 (0, bulletSpeed));
			}
			if (dir == "Right") {
				anim.SetTrigger("FRight");
//				Debug.Log("Bullet has been instantiated");
				//Vector3 offsetR = new Vector3 (cannonR.transform.position.x, cannonR.transform.position.y, cannonR.transform.position.z); 
				Rigidbody2D projectileR = Instantiate (bullets, cannonR.transform.position, Quaternion.Euler (0.0f, 0.0f, transform.eulerAngles.z - 90.0f));
				projectileR.transform.tag = "PlayerBullet";
				projectileR.AddRelativeForce (new Vector2 (0, bulletSpeed));
                
            }
			StartCoroutine (DelayShooting ());
			//Audio
			shootSource.Play();
		}
	}

	private IEnumerator DelayShooting()
	{
		yield return new WaitForSeconds (delayShootTime);
		delay = false;
	}
}
