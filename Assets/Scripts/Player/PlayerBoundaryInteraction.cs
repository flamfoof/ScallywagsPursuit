using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBoundaryInteraction : MonoBehaviour {
	public bool warning = false;
	public bool inDanger = false;
	public float outsideBoundaryDeathTimer = 10.0f;
	public AudioSource bgm;
	public float bgmSoundInitial = 0.1f;
	public float bgmSoundDanger = 0.025f;
	public GameObject textbox;
	public TextBoxScript textboxManager;
	public Text text;
	private Light light;
	public int currentScene;

	private Coroutine deathTimerCoroutine = null;
	private Coroutine lightShining = null;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G)) {
			StopCoroutine (lightShining);
		}
	}
	IEnumerator StartDeathTimer()
	{
		yield return new WaitForSeconds (outsideBoundaryDeathTimer);
		Debug.Log ("u ded");
		///replace this part with death animation of kraken or something
		if (inDanger) {
			//SceneManager.LoadScene (2);
			StartCoroutine(GetComponent<PlayerHealth>().PlayerDeath());
		}
	}

	IEnumerator LightShow()
	{
		float lightIntensityInterval = 0.05f;
		float speed;
		float timer = outsideBoundaryDeathTimer;
		while (true) {
			yield return new WaitForSeconds(0.02f);

			speed = (outsideBoundaryDeathTimer / timer) * 5;
			if (timer >= 0.1f) {
				timer -= 0.02f;
			}
			if (light.intensity <= 0.1) {
				lightIntensityInterval = 0.05f;
			} else if (light.intensity >= 5) {
				lightIntensityInterval = -0.05f;
			}
			light.intensity += lightIntensityInterval * speed;
		}
	}

	IEnumerator DecreaseSoundVolume()
	{
		float decreaseInterval = (bgmSoundInitial - bgmSoundDanger) / 50;
		for (int i = 0; i < 50; i++) {
			//1 second change
			yield return new WaitForSeconds (0.02f);
			bgm.volume -= decreaseInterval;
		}
	}

	IEnumerator IncreaseSoundVolume()
	{
		float increaseInterval = (bgmSoundInitial - bgmSoundDanger) / 50;
		for (int i = 0; i < 50; i++) {
			//1 second change
			yield return new WaitForSeconds (0.02f);
			bgm.volume += increaseInterval;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "BoundaryWarning") {
			if (!warning) {
				warning = true;
				text.text = "You have come across this area known as the dead seas. If you travel any further you may come across unwanted dangers.";
				textboxManager.warningText = true;
				textbox.SetActive (true);
			}
		} else if (other.tag == "Boundary") {
			inDanger = true;
			deathTimerCoroutine = StartCoroutine (StartDeathTimer ());
			lightShining = StartCoroutine (LightShow());
			StartCoroutine (DecreaseSoundVolume ());
			light.enabled = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Boundary") {
			inDanger = false;
			StartCoroutine (IncreaseSoundVolume ());
			if (deathTimerCoroutine != null) {
				StopCoroutine (deathTimerCoroutine);
				StopCoroutine (lightShining);
				light.intensity = 0;
				light.enabled = false;
			}
		}
	}
}
