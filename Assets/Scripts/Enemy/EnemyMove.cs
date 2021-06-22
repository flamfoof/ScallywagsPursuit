using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	public Transform target;
	public Transform oldTarget;
	public float speed;

	public float dragDefault = 1.0f;
	public float decel = 1.0f;

	public bool stopMoving = false;
	public float waitTime = 3.0f;
	public Vector2 dir;
	private Rigidbody2D rb;
	public float angle;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!stopMoving) {
			float step = speed * Time.deltaTime;

			if (GetComponent<EnemyPathing> ().enemyState == EnemyPathing.EnemyState.Chase) {
				
				target = GetComponent<EnemyPathing> ().player.transform;
			} else {
				if (oldTarget != null) {
					target = oldTarget;
					oldTarget = null;
				}
			}
			//transform.position = Vector3.MoveTowards (transform.position, target.position, step);


			dir = target.transform.position - transform.position;
			angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			//Debug.Log ("Dir before normalized: " + dir);
			//dir = dir.normalized;
			//Debug.Log ("After normalized: " + dir);
//			Debug.Log("Enemy dir: " + dir);
			rb.AddForce (dir * speed, ForceMode2D.Impulse);
		} else {
//			Debug.Log ("STOPPING");
			rb.AddForce (dir * 0);
		}

		if (stopMoving) {
			rb.drag = decel;
//			Debug.Log ("Enemy slowing down");
		} else {
			rb.drag = dragDefault;
		}
	}

	public IEnumerator DockShip()
	{
		stopMoving = true;

		yield return new WaitForSeconds (waitTime);
			
		stopMoving = false;
	}
}
