using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
	public float r, g, b;
	bool alive = true;
	public float timeAlive = 0.0f;

	private SpriteRenderer sRenderer;
	private Collider2D sCollider;

	private void OnMouseDown()
	{
		alive = false;
		timeAlive = Population.elapsed;
		sRenderer.enabled = false;
		sCollider.enabled = false;
	}

	private void Start()
	{
		sRenderer = GetComponent<SpriteRenderer>();
		sCollider = GetComponent<Collider2D>();
		sRenderer.color = new Color(r, g, b);
	}
}
