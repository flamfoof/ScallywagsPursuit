using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backToMM : MonoBehaviour {

	public float back = 5.0f;
	public int secs = 3;

	// Use this for initialization
	void Start () {
		StartCoroutine(WaitToLeave());
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator WaitToLeave()
	{
		yield return new WaitForSeconds(back);
		SceneManager.LoadScene("Main Menu 2");
		Debug.Log("Done waiting.");
	}
}