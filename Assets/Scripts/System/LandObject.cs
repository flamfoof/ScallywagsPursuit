using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Land", menuName = "Lands")]
public class LandObject : ScriptableObject {
	public int id;
	public string name;
	[Tooltip("Land Types are: Island, Continents")]
	public string type;
	public float income;
	public float wood;
	public float growthRate;
	public float resource;

	public bool hasCapn;

	[Header("Default Values")]
	public float defaultIncome;
	public float defaultWood;
	public float defaultGrowthRate;
	public float defaultResource;

	public void Reset()
	{
		income = defaultIncome;
		wood = defaultWood;
		resource = defaultResource;
		growthRate = defaultGrowthRate;
		hasCapn = false;
	}
}
