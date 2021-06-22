using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour {

	private Animator anim;
	public float incomeIncreaseTimer = 2.0f;
	public LandObject landData;


	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		landData.Reset ();
		StartCoroutine (IslandScaleIncrease ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator IslandScaleIncrease()
	{
		while (true) {
			yield return new WaitForSeconds (incomeIncreaseTimer);
//			Debug.Log ("income increased");
			landData.wood += landData.income/10.0f;
			landData.resource += (landData.resource * landData.growthRate) + landData.income;
			landData.income += landData.growthRate;
			//landData.growthRate += Mathf.FloorToInt(landData.growthRate / 2);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
//			Debug.Log ("Ship incoming");
			anim.SetBool ("playerNear", true);
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") {
//			Debug.Log ("Ship leaving");
			anim.SetBool ("playerNear", false);
		}
	}
}
