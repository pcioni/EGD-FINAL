﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthbarBehavior : MonoBehaviour {
	public GameObject text_prefab;
	public GameObject text;
	int max_health;

	// Use this for initialization
	void Start () {
		text = (GameObject) Instantiate(text_prefab, Vector3.zero, Quaternion.identity);
		text.transform.SetParent(GameObject.Find ("Floating Character Canvas").transform);
		text.GetComponent<Text> ().text = max_health.ToString();
		text.transform.position = new Vector3(transform.position.x, transform.position.y+0.1f);
	}

	public void defaultHealth(int current, int max){
		//print ("default health set to " + amount);
		max_health = max;
		SetHealth (current);
	}

	public void SetHealth(float amount){
		transform.localScale = new Vector3 (amount/max_health, 1, 1);
		text.GetComponent<Text> ().text =  amount.ToString();
	}
}
