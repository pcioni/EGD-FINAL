using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A giant storage system for all the text in the game.
/// </summary>
public class OverworldTextStorage : MonoBehaviour {
	public int level_number;
	Dictionary<string, string[]> dialogue;
	Dictionary<string, string[]> level1;
	Dictionary<string, string[]> level2;
	Dictionary<string, string[]> level3;

	// Use this for initialization
	void Awake () {
		//Initialize
		dialogue = new Dictionary<string, string[]> ();
		level1 = new Dictionary<string, string[]> ();
		level2 = new Dictionary<string, string[]> ();
		level3 = new Dictionary<string, string[]> ();

		//Set the correct one
		switch (level_number) {
		case 1:
			dialogue = level1;
			break;
		case 2:
			dialogue = level2;
			break;
		case 3:
			dialogue = level3;
			break;
		}

		//LEVEL 1
		level1.Add("Marketplace Amelia", new string[]{
			"SAM|Hey.",
			"AMELIA|Busy right now.",
			"SAM|Then why are you playing the game if you're busy?",
			"AMELIA|I'm busy PLAYING the game. I have to figure out " +
			"which locations give the most loot fastest.",
			"SAM|Can I join up with you?",
			"AMELIA|You'd just slow me down.",
			"SAM|But it would be more fun together.",
			"AMELIA|_Sorry, were you saying something?_~Ah, nope, I'm " +
			"here to play the game, not make friends."
		});
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string[] RetrieveDialogue(string id){
		return dialogue [id];
	}
}
