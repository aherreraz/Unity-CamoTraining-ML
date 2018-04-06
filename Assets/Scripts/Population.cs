using System.Linq;
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
	private float mutationChance = 0.05f;
	
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
			obj.GetComponent<DNA>().sx = Random.Range(0.4f, 1.0f);
			obj.GetComponent<DNA>().sy = Random.Range(0.4f, 1.0f);
			population.Add(obj);
		}
	}
	private void Update()
	{
		elapsed += Time.deltaTime;
		if (elapsed > trialTime)
		{
			elapsed = 0;
			BreedNewPopulation();
		}
	}

	private float Combine(float gene1, float gene2, float mutationMin, float mutationMax)
	{
		if (Random.Range(0.0f, 1.0f) < mutationChance)
			return Random.Range(mutationMin, mutationMax);

		return Random.Range(0, 10) < 5 ? gene1 : gene2;
	}

	private GameObject Breed(GameObject parent1, GameObject parent2)
	{
		Vector3 pos = new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-4.5f, 4.5f), 0.0f);
		GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
		DNA dna1 = parent1.GetComponent<DNA>();
		DNA dna2 = parent2.GetComponent<DNA>();
		
		// Swap parent dna and mutate
		offspring.GetComponent<DNA>().r = Combine(dna1.r, dna2.r, 0.0f, 1.0f);
		offspring.GetComponent<DNA>().g = Combine(dna1.g, dna2.g, 0.0f, 1.0f);
		offspring.GetComponent<DNA>().b = Combine(dna1.b, dna2.b, 0.0f, 1.0f);
		offspring.GetComponent<DNA>().sx = Combine(dna1.sx, dna2.sx, 0.4f, 1.0f);
		offspring.GetComponent<DNA>().sy = Combine(dna1.sy, dna2.sy, 0.4f, 1.0f);

		return offspring;
	}

	private void BreedNewPopulation()
	{
		// Order by fitness
		List<GameObject> sortedList = population.OrderByDescending(o => o.GetComponent<DNA>().timeAlive).ToList();
		population.Clear();

		// Breed fittest half of sorted list 
		for (int i = 0; i < sortedList.Count / 2; i++)
		{
			population.Add(Breed(sortedList[i], sortedList[i + 1]));
			population.Add(Breed(sortedList[i + 1], sortedList[i]));
		}

		// Destroy previous population
		for (int i = 0; i < sortedList.Count; i++)
			Destroy(sortedList[i]);
		generation++;
	}
}
