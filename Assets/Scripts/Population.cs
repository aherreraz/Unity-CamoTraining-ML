using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
	public GameObject personPrefab;
	public int populationSize = 10;
	List<GameObject> population = new List<GameObject>();
	public static float elapsed = 0;

	private int trialTime = 10;
	private int generation = 1;
	
	private GUIStyle guiStyle = new GUIStyle();
	private void OnGUI()
	{
		guiStyle.fontSize = 50;
		guiStyle.normal.textColor = Color.white;
		GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
		GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int) elapsed, guiStyle);
	}
	private void Start()
	{
		for (int i = 0; i < populationSize; i++)
		{
			Vector3 pos = new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-4.5f, 4.5f), 0.0f);
			GameObject obj = Instantiate(personPrefab, pos, Quaternion.identity);
			obj.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
			obj.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
			obj.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
			population.Add(obj);
		}
	}
	private void Update()
	{
		elapsed += Time.deltaTime;
		if (elapsed > trialTime)
		{
			elapsed = 0;
		}
	}
}
