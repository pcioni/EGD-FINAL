using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgressLevel : MonoBehaviour {
	public string usageInstructions = "Open script to read. " +
       "Enable Or Disable is organized by what state " +
       "overworld_progress is in. So if o_p is 4, the " +
       "code will run through everything in E Or D[4] " +
       "and enable everything in the enable array, " +
       "disable everything in the disable array. Doors" +
       "and speaking characters can change o_p when their" +
       "action completes- if they have a new_progress_number " +
       "!= -1, they will update the o_p when they complete " +
       "their first block of conversation." +
       "Sometimes, multiple things need to happen before a " +
       "level can progress. This is tracked with contingencies." +
       "Objects that need each other to be complete before " +
       "progressing the level keep reference to each other in " +
       "contingency arrays and once all of them have been " +
       "interacted with, the last of them visited updates progress." +
       "Objects in the contingencies array must all have the same" +
       "new_progress_number";
	[System.Serializable]
	public struct Progress {
		public GameObject[] enable;
		public GameObject[] disable;
	}
	public Progress[] EnableOrDisable;
	int overworld_progress = 0;

	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateOverworldProgress(int x){
		//doors will update your progress
		if (x > overworld_progress) {
			overworld_progress = x;
			EnableAndDisableObjects ();
		}
	}

	public int getOverworldProgress(){
		return overworld_progress;
	}

	void EnableAndDisableObjects(){
		Progress p = EnableOrDisable [overworld_progress];
		foreach (GameObject e in p.enable) {
			e.SetActive (true);
		}
		foreach (GameObject d in p.disable) {
			d.SetActive (false);
		}
	}

	public void ProgressTo(int number){
		int x = 0; 
		while (x <= number) {
			overworld_progress = x;
			EnableAndDisableObjects ();
			x++;
		}
	}

}
