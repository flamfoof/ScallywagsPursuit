using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextBoxScript : MonoBehaviour {

	public GameObject textBox;
	public GameObject Player;
	public GameObject Barrel;
	public Text thetext;

	public TextAsset file;
	public string[] textLine;

	public int currentLine;
	public int endLine;

	public bool canProgress = true;
	public bool warningText = false;
	public bool playerMoved = false;
	public bool playerMovedUp = false;
	public bool playerMovedLeft = false;
	public bool playerMovedDown = false;
	public bool playerMovedRight = false;

	public bool tutorial;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		Player.GetComponent<PlayerMove>().canMove = false;
		Player.GetComponent<PlayerShoot>().canShoot = false;


		if (file != null) {
			textBox.SetActive (true);
			textLine = (file.text.Split ('\n'));
		} else if (!tutorial) {
			//Debug.Log ("No script, so closing text manager");
			//textBox.SetActive (false);
			textBox.SetActive (true);
			thetext.text = "Gather 2500 Gold!";
		} else {
			Debug.Log ("No script, so closing text manager");
			textBox.SetActive (false);
		}

		if (!tutorial) {
			
		}

		if (endLine == 0) {

			endLine = textLine.Length - 1;
		}

	}

	// Update is called once per frame
	void Update () {
		if (file != null && !warningText) {
			if (!textBox.activeSelf) {
				textBox.SetActive (true);
			}
			thetext.text = textLine [currentLine];

			if (canProgress) {
				if (Input.GetKeyDown (KeyCode.Return) || Input.GetButtonDown ("Fire1")) {

					currentLine += 1;
				}
			}
			

			if (currentLine == 6) {

				Player.GetComponent<PlayerMove> ().canMove = true;
				Player.GetComponent<PlayerInteraction> ().canLoot = false;
			}

			if (currentLine == 7) {
				Player.GetComponent<PlayerMove> ().canMove = true;
				Player.GetComponent<PlayerInteraction> ().canLoot = false;

				if (!playerMoved) {
					canProgress = false;
					textBox.SetActive (false);
//					Debug.Log ("Player Y: " + Player.GetComponent<PlayerMove> ().GetDirectionY ());
//					Debug.Log ("Player X: " + Player.GetComponent<PlayerMove> ().GetDirectionX ());
					if (Player.GetComponent<PlayerMove> ().GetDirectionY () > 0.3f) {
						playerMoved = true;
					} else if (Player.GetComponent<PlayerMove> ().GetDirectionY () < -0.3f) {
						playerMoved = true;
					}

					if (Player.GetComponent<PlayerMove> ().GetDirectionX () > 0.3f) {
						playerMoved = true;
					} else if (Player.GetComponent<PlayerMove> ().GetDirectionX () < -0.3f) {
						playerMoved = true;
					}

				} else {
					textBox.SetActive (true);
					canProgress = true;
				}
			}

			if (currentLine == 8 && playerMoved) {
				
				Player.GetComponent<PlayerMove> ().canMove = false;
				Player.GetComponent<PlayerShoot> ().canShoot = true;

			}

			if (currentLine == 9) {

				Player.GetComponent<PlayerMove> ().canMove = false;
				Player.GetComponent<PlayerShoot> ().canShoot = false;

			}

			if (currentLine == 12) {

				Player.GetComponent<PlayerMove> ().canMove = true;
				Player.GetComponent<PlayerShoot> ().canShoot = true;
				textBox.SetActive (false);
				canProgress = false;

			}

			if (currentLine == 12 && !Barrel.activeInHierarchy) {

				Player.GetComponent<PlayerMove> ().canMove = false;
				Player.GetComponent<PlayerShoot> ().canShoot = false;
				textBox.SetActive (true);
				canProgress = true;
				Player.GetComponent<PlayerInteraction> ().canSalvage = false;
			}

			if (currentLine == 14) {

				Player.GetComponent<PlayerMove> ().canMove = true;
				Player.GetComponent<PlayerShoot> ().canShoot = true;
				textBox.SetActive (false);
				canProgress = false;
				Player.GetComponent<PlayerInteraction> ().canSalvage = true;

			}

			if (Player.GetComponent<PlayerInteraction> ().salvageCount == 1) {

				Player.GetComponent<PlayerMove> ().canMove = false;
				Player.GetComponent<PlayerShoot> ().canShoot = false;
				textBox.SetActive (true);
				canProgress = true;
				currentLine += 1;
				Player.GetComponent<PlayerInteraction> ().salvageCount++;
			}
			
			if (currentLine == 15 && Player.GetComponent<PlayerInteraction> ().salvageCount == 1) {

				Player.GetComponent<PlayerMove> ().canMove = false;
				Player.GetComponent<PlayerShoot> ().canShoot = false;
				textBox.SetActive (true);

			}

			if (currentLine == 19) {

				Player.GetComponent<PlayerMove> ().canMove = true;
				Player.GetComponent<PlayerShoot> ().canShoot = true;
				textBox.SetActive (false);
				canProgress = false;
				Player.GetComponent<PlayerInteraction> ().canLoot = true;
			}

			if (Player.GetComponent<PlayerInteraction> ().score > 0 && Player.GetComponent<PlayerInteraction> ().islandCounter == 1) {

				Player.GetComponent<PlayerMove> ().canMove = false;
				Player.GetComponent<PlayerShoot> ().canShoot = false;
				textBox.SetActive (true);
				currentLine += 1;
				Player.GetComponent<PlayerInteraction> ().islandCounter++;
			}
			
			if (currentLine == 20 && Player.GetComponent<PlayerInteraction> ().score > 0) {

				Player.GetComponent<PlayerMove> ().canMove = false;
				Player.GetComponent<PlayerShoot> ().canShoot = false;
				textBox.SetActive (true);
				canProgress = true;
			}

			if (currentLine == 21) {
			
				Player.GetComponent<PlayerMove> ().canMove = true;
				Player.GetComponent<PlayerShoot> ().canShoot = true;
				SceneManager.LoadScene (2);

			}

		} 
		if (warningText == true) {
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetButtonDown ("Fire1")) {
				textBox.SetActive (false);
				warningText = false;
			}

		}

	}
}
