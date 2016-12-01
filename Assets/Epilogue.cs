using UnityEngine;
using System.Collections;

public class Epilogue : MonoBehaviour {
	TextControl text_controller;
	OverworldTextStorage text_storage;

	// Use this for initialization
	void Start () {
		text_controller = GameObject.FindObjectOfType<TextControl> ();
		text_storage = GameObject.FindObjectOfType<OverworldTextStorage> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
