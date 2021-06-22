using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
	public float spawnTimer = 15.0f;
	public bool isSpawn = true;
	public int numShips;
	public float refreshCountTimer = 5.0f;

	public int maxEnemyCount = 6;
	public GameObject enemyShip;
	public Transform spawnLocation;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnShips ());
		numShips = 0;
		StartCoroutine (CountShips ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator SpawnShips()
	{
		while (isSpawn) {
			if (maxEnemyCount > numShips) {
//				Debug.Log (spawnTimer);
				yield return new WaitForSeconds (spawnTimer);
				GameObject newShip = Instantiate (enemyShip, spawnLocation.position, Quaternion.identity);
				newShip.GetComponent<EnemyPathing> ().currentPoint = transform;

				numShips = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			} else {
				isSpawn = false;
			}
		}
	}

	private IEnumerator CountShips()
	{
		while (true) {
			yield return new WaitForSeconds (refreshCountTimer);
			numShips = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			//Debug.Log ("Number of ships in game: " + numShips);

			if (maxEnemyCount >= numShips && !isSpawn) {
				isSpawn = true;
				//Debug.Log ("spawning the co routine");
				StartCoroutine (SpawnShips ());
			}
		}
	}
}
