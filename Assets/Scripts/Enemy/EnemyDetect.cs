using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour {

	public float radiusDetection = 5.0f;
	public GameObject player;
	public GameObject enemy;
	public bool playerFound = false;
	[Header("Debugging")]
	public int segment = 10;
	public float distDetection = 2.0f;
	private CircleCollider2D circleRadius;




	private LineRenderer debugCircle;
	// Use this for initialization
	void Start () {
		circleRadius = GetComponent<CircleCollider2D> ();
		circleRadius.radius = radiusDetection;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Awake()
	{
		DebugCircleRange ();
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (Vector3.Distance (transform.position, player.transform.position));
		if (Vector3.Distance (transform.position, player.transform.position) <= distDetection && !playerFound) {
			enemy.GetComponent<EnemyAttack> ().StartFiring ();
			playerFound = true;
		}
		if (playerFound) {
			enemy.GetComponent<EnemyPathing> ().ChasePlayer ();
		}
	}

	void DebugCircleRange()
	{
		debugCircle = gameObject.GetComponent<LineRenderer> ();

		debugCircle.positionCount = segment + 1;
		debugCircle.useWorldSpace = false;

		float x;
		float y;

		float angle = 5f;

		for(int i = 0; i < (segment + 1); i++)
		{
			x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusDetection;
			y = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusDetection;

			debugCircle.SetPosition(i, new Vector3(x, y, 0));

			angle += (360f/segment);
		}

	}
	public IEnumerator LostPlayer()
	{
		while (playerFound) {
			yield return new WaitForSeconds (5.0f);
			Debug.Log ("Enemy going back to patrol");
			playerFound = false;
			enemy.GetComponent<EnemyPathing> ().enemyState = EnemyPathing.EnemyState.Patrol;
			enemy.GetComponent<EnemyAttack> ().inRange = false;
			enemy.GetComponent<EnemyPathing> ().FindInitialPatrol ();
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") {
			Debug.Log ("Lost sight of player");
			StartCoroutine (LostPlayer ());
				
		}
	}
}
