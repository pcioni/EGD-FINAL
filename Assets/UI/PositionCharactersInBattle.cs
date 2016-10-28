﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionCharactersInBattle : MonoBehaviour {
	public enum Arrangment{Diagonal, DiagonalReversed}
	public Arrangment arrangement;
	List<GameObject> characters;
	//^ this is likely just the way we do this while merging everything
	Bounds b;
	public bool heroes =  true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ArrangeCharacters(List<GameObject> heroes, List<GameObject> enemies){
		if (name.Contains ("Hero")) {
			characters = heroes;
		} 
		else if (name.Contains ("Enemy")) {
			characters = enemies;
		} 
		else {
			print ("ERROR: Bounds should be named Hero Group Bounds and Enemy Group Bounds respectively.");
		}

		Vector3 left = Vector3.zero;
		Vector3 right = Vector3.zero;

		Bounds b = GetComponent<BoxCollider2D> ().bounds;

		switch (arrangement) {
		case Arrangment.Diagonal:
			left = b.min;
			right = b.max;
			break;
		case Arrangment.DiagonalReversed:
			left = new Vector3 (b.min.x, b.max.y, 0);
			right = new Vector3 (b.max.x, b.min.y, 0);
			break;
		}

		Debug.DrawLine (left, right);

		//for start
		b = GetComponent<BoxCollider2D> ().bounds;
		for (int x = 0; x < characters.Count; x++) {
			float percentage = (x + 1) / (characters.Count + 1.0f);
			characters [x].transform.position = Vector2.Lerp (left, right, percentage); 
			if (arrangement == Arrangment.Diagonal)
				characters [x].GetComponent<SpriteRenderer> ().sortingOrder = characters.Length - x;
			else
				characters [x].GetComponent<SpriteRenderer> ().sortingOrder = x;
		}
	}
}
