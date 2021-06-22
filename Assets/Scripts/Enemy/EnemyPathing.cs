using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
	public enum EnemyState{Patrol, Chase};
	public EnemyState enemyState;

	public float distance = 100.0f;
	public float chaseTime = 5.0f;
	public int chaseCounter = 0;
	public int chaseLeave = 10;
	public GameObject player;

	List<Transform> possiblePath = new List<Transform>();
	GameObject[] islands;
	public EnemyPatrol currentPathNode;
	public Transform previousPoint;
	public Transform currentPoint;
	public Transform nextPoint;


	// Use this for initialization
	void Awake () {
		//FindInitialPatrol ();
		islands = GameObject.FindGameObjectsWithTag ("Island");
		currentPathNode.islandPath = GameObject.FindGameObjectWithTag ("EnemyB").GetComponent<EnemyPatrol>().islandPath;

		FindSurroundingIslands ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Start()
	{
		StartCoroutine (ChaseTime ());
	}
	// Update is called once per frame
	void Update () {
		if (enemyState == EnemyState.Chase) {
			ChasePlayer ();
		}
//		Debug.Log ("Plyaer pos: " + player.transform.position);
	}

	public void FindInitialPatrol()
	{
		if (currentPoint == null) {
			int indexClosest = 0;
			for(int i = 0; i < islands.Length; i++){
				float newDistance = Vector3.Distance (transform.position, islands [i].transform.position);
				if (newDistance < distance) {
					indexClosest = i;
					distance = newDistance;
				}
			}
			nextPoint = islands [indexClosest].transform;
		}
	}

	public void FindSurroundingIslands()
	{
		for (int i = 0; i < currentPathNode.islandPath.Count; i++) {
			possiblePath.Add (currentPathNode.islandPath [i].transform);
		}

//		Debug.Log ("Find next point");
		nextPoint = possiblePath [Random.Range (0, possiblePath.Count)].transform;

		GetComponent<EnemyMove> ().target = nextPoint;
		ClearList (possiblePath);
	}

	void ClearList(List<Transform> path)
	{
		path.RemoveRange (0, path.Count);
	}

	public void ChasePlayer()
	{
		enemyState = EnemyState.Chase;
		GetComponent<EnemyMove> ().oldTarget = GetComponent<EnemyMove> ().target;
		GetComponent<EnemyMove> ().target = player.transform;

	}

	public IEnumerator ChaseTime()
	{
		while (enemyState == EnemyState.Chase && Vector2.Distance(transform.position, player.transform.position) > 1.0f && chaseCounter <= chaseLeave) {
			Debug.Log ("Starting to lose sight...");
			yield return new WaitForSeconds (chaseTime);
			if (Vector2.Distance (transform.position, player.transform.position) <= 1.0f) {
				Debug.Log ("Player is pretty close");
				chaseCounter = 0;
			}
			chaseCounter++;
		}
		enemyState = EnemyState.Patrol;
		GetComponent<EnemyMove> ().target = nextPoint;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if ((other.transform.tag == "Island" || other.transform.tag == "Base") && enemyState == EnemyState.Patrol && transform.tag == "Enemy") {
			
			previousPoint = currentPoint;
			currentPoint = other.transform;
			currentPathNode.islandPath = other.GetComponent<EnemyPatrol> ().islandPath;
			StartCoroutine (GetComponent<EnemyMove> ().DockShip ());
			if (currentPoint != previousPoint) {
				FindSurroundingIslands ();
			}
		}
	}
}
