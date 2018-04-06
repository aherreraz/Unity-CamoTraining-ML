using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
	public float r, g, b;
	public float timeAlive;

	private SpriteRenderer sRenderer;
	private Collider2D sCollider;

	private void OnMouseDown()
	{
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
