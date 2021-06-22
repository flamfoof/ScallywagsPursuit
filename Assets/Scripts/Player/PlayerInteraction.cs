using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour {
	[Header("Player Stats")]
	public float score = 0;
	public float targetScore = 2500.0f;

	[Header("Island Data, Don't Touch")]
	public LandData landData;
	public LandObject targetLand;
	public GameObject closestIsland;
	public bool nearIsland;
	public bool nearBase;
	public bool nearEnemyBase;
	public bool nearSalvage;
	public bool hasCapn;
	public bool spawnedCapnHint = false;

	[Header("UI Stuff")]
	public Text scoreText;
	public Text islandNameText;
	public Text islandResourceText;
	public Text salvageText;
	public GameObject islandCaptainText;
	public GameObject EnemyMenu;
	public GameObject islandMenu;
	public GameObject salvageMenu;
	public GameObject textBox;
	public Text theText;

	public GameObject buttonHelper;
	public LevelController levelCont;
	public Salvage salvageCont;
	//For Tutorial
	public int salvageCount = 0;
	public bool canSalvage = true;
	public bool canLoot = true;
	public int islandCounter = 0;

	public int sceneLevelNumber = 3;
	public GameObject selectedSalvage;
	public bool isPressed = false;

	//Audio
	public AudioClip islandSFX;
	public AudioSource islandSource;

	// Use this for initialization
	void Start () {
		hasCapn = false;
		scoreText.text = "Score: " + score;
		levelCont = levelCont.GetComponent<LevelController> ();
		//Audio
		islandSource.clip = islandSFX;
	}
	
	// Update is called once per frame
	void Update () {
		if (nearIsland || nearBase || nearEnemyBase || nearSalvage) {
			
			if ((Input.GetKeyDown (KeyCode.B) && canLoot || Input.GetButtonDown ("Fire2")) && closestIsland != null && canLoot) {
				Debug.Log ("Interacting with island");
				//Audio
				islandSource.Play();

				landData.GetIslandInfo (closestIsland.GetComponent<Land> ().landData.id);
				targetLand = landData.GetLandData (closestIsland.GetComponent<Land> ().landData.id);
				islandNameText.text = targetLand.name;
				islandResourceText.text = "Gold:  " + Mathf.FloorToInt(targetLand.resource) + "\nSalvage:     " + Mathf.FloorToInt(targetLand.wood);
				if (targetLand.hasCapn && score > targetScore) {
					islandCaptainText.SetActive (true);
				}
				islandMenu.SetActive (true);
				islandCounter += 1;
			} else if ((Input.GetKeyDown (KeyCode.B) || Input.GetButtonDown ("Fire2")) && nearBase) {
				Win ();
			} else if ((Input.GetKeyDown (KeyCode.B) || Input.GetButtonDown ("Fire2")) && nearEnemyBase) {
				Debug.Log ("Near enemy base, watch out");
				GetComponent<PlayerMove> ().canMove = false;
				EnemyMenu.SetActive (true);
			} else if ((Input.GetKeyDown (KeyCode.B) || Input.GetButtonDown ("Fire2")) && nearSalvage) {
				salvageText.text = "There are wood debris that can be used to repair.\n Salvage:  " + "10";
				salvageMenu.SetActive (true);
			}
		}
		if (textBox.activeSelf) {

			if ((Input.GetButtonDown ("Fire1") || Input.GetKeyDown (KeyCode.Return)) && !isPressed) {
				textBox.SetActive (false);
			}
		}
	}

	public void UpdateScore(int newScore)
	{
		score += newScore;
		scoreText.text = "Gold: " + score;
		if (!spawnedCapnHint && score > targetScore) {
			Debug.Log ("Captain dialogue activate");
			textBox.SetActive (true);
			theText.text = "Lets go look around and see if we can find some clues to his whereabouts";
			while (!landData.captainIsAssigned) {
				for(int i = 0; i < landData.islands.Count; i++)
				{
					if (landData.islands [i].hasCapn == true)
						landData.captainIsAssigned = true;
				}
				int rand = Random.Range (0, landData.islands.Count);
				if (!landData.captainIsAssigned) {
					Debug.Log (rand);
					landData.islands [rand].hasCapn = true;
				}
				if(landData.captainIsAssigned)
				{
					landData.islandWithCaptain = landData.islands[rand].name;
					Debug.Log("Captain is at: " + landData.islandWithCaptain);
				}
			}
			isPressed = true;
			StartCoroutine (ButtonUp ());
			spawnedCapnHint = true;
		}
	}
	private IEnumerator ButtonUp()
	{
		if (isPressed == true) {
			yield return new WaitForSeconds (0.3f);
			isPressed = false;
		}
	}
	public void UpdateHealth(int heal)
	{
		GetComponent<PlayerHealth> ().health += heal;
		if (GetComponent<PlayerHealth> ().health >= GetComponent<PlayerHealth> ().healthMax) {
			GetComponent<PlayerHealth> ().health = GetComponent<PlayerHealth> ().healthMax;
		}
		GetComponent<PlayerHealth> ().UpdateSprite ();
	}

	//this is played from the level controller script from pushing the button.
	public void Loot()
	{
		if (targetLand == null) {
			Debug.Log ("Too far from island");
		} else {
			islandMenu.SetActive (false);
			UpdateHealth (Mathf.FloorToInt (targetLand.wood));
			UpdateScore (Mathf.FloorToInt(targetLand.resource));

			targetLand.resource = 0;
			targetLand.wood = 0;
			if (targetLand.hasCapn && score > targetScore && spawnedCapnHint) {
				Debug.Log ("Has cap'n");
				hasCapn = true;
				targetLand.hasCapn = false;
				islandCaptainText.SetActive (false);
			}
		}

	}

	public void Salvage()
	{
		if (selectedSalvage != null && canSalvage) {
			salvageCont.SalvageObject (selectedSalvage);
			UpdateHealth (10);
			salvageMenu.SetActive (false);
			//For Tutorial
			salvageCount += 1;
		}
	}

	public void Win()
	{
		//change to win scene or something
		SceneManager.LoadScene ("Win Screen");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Island") {
			Debug.Log ("Island in sight");
			nearIsland = true;
			closestIsland = other.gameObject;
			buttonHelper.SetActive (true);
		}
		if (other.tag == "Base" && hasCapn == true) {
			nearBase = true;
			buttonHelper.SetActive (true);
		}
		if (other.tag == "EnemyB" && hasCapn == true) {
			nearEnemyBase = true;
			buttonHelper.SetActive (true);
		}
		if (other.tag == "Salvage" && canSalvage) {
//			Debug.Log ("Near a salvage site");
			selectedSalvage = other.gameObject;
			nearSalvage = true;
			salvageCont = other.GetComponent<Salvage> ();
			buttonHelper.SetActive (true);
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Island") {
//			Debug.Log ("Leaving island....");
			nearIsland = false;
			closestIsland = null;
			buttonHelper.SetActive (false);
			islandMenu.SetActive (false);
			if (islandMenu.activeSelf) {
				islandMenu.SetActive (false);
			}
		}
		if (other.tag == "Base") {
			nearBase = false;
			buttonHelper.SetActive (false);
		}
		if (other.tag == "EnemyB") {
			nearEnemyBase = false;
			buttonHelper.SetActive (false);
		}
		if (other.tag == "Salvage") {
			Debug.Log ("Leaving a salvage site");
			selectedSalvage = null;
			nearSalvage = false;
			salvageCont = null;
			buttonHelper.SetActive (false);
		}
	}
}
