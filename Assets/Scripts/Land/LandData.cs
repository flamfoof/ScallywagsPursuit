using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LandData : MonoBehaviour {

	public List<LandObject> islands = new List<LandObject>();
	public string islandWithCaptain;
	public bool captainIsAssigned;

		
	void Awake () {
		foreach (Transform child in gameObject.transform) {
			islands.Add (child.gameObject.GetComponent<Land>().landData);
			//Debug.Log (child.gameObject.GetComponent<Land> ().landData);
		}

		for (int i = 0; i < islands.Count; i++) {
			islands [i].id = i;
		}

		//for (int i = 0; i < 100; i++) {
		int rand = Random.Range (0, islands.Count);
		Debug.Log (rand);
		islands [rand].hasCapn = true;


			

		
		Debug.Log (islands [rand].name + " island has capn");
		//}
		/*
		for (int i = 0; i < 100; i++) {
			Debug.Log(Random.Range(0, islands.Count));
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
		//islands[0].GetComponent<Land>().
		if (Input.GetKeyDown (KeyCode.P)) {
			for (int i = 0; i < islands.Count; i++) {
				Debug.Log ("This island is called: " + islands [i].name + "\nIts captain is: " + islands [i].hasCapn);
			}

		}

		while (!captainIsAssigned) {
			for(int i = 0; i < islands.Count; i++)
			{
				if (islands [i].hasCapn == true)
					captainIsAssigned = true;
			}
			int rand = Random.Range (0, islands.Count);
			if (!captainIsAssigned) {
				
				Debug.Log (rand);
				islands [rand].hasCapn = true;
				islandWithCaptain = islands[rand].name;
			}
			if(captainIsAssigned)
			{
				
				Debug.Log("Captain is at: " + islandWithCaptain);
			}
		}
			
	}

	public void GetIslandInfo(int i)
	{
		Debug.Log ("This island is called: " 
			+ islands [i].name + "\nThis is an " + islands[i].type + ". Its ID is: " + islands[i].id
			+ " and its income is " + islands[i].income + ", wood is " 
			+ islands [i].wood + ", and its growth rate is " + islands [i].growthRate);
	}

	public LandObject GetLandData(int i)
	{
		return islands [i];
	}
}
