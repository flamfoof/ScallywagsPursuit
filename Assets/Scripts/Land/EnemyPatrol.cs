using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
	public List<Transform> islandPath = new List<Transform> ();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;

		foreach (Transform path in islandPath) {
			if (path != null) {
				Gizmos.DrawLine (transform.position, path.transform.position);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
	}
}
