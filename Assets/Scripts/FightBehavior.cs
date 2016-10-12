using UnityEngine;
using System.Collections;

public class FightBehavior : MonoBehaviour {

	string turn_action;

	// Use this for initialization
	void Start () {
		turn_action = "AI";
	}

	public void setAction(string action){
		turn_action = action;
	}

	public void doAction(){
		Debug.Log (gameObject.name + " " + turn_action + "!");
		if (turn_action == "defeats all the enemies") {
			FindObjectOfType<BattleManager> ().KillEnemies ();
		} 
		else if (turn_action == "AI") {
			if (Random.Range (1, 10) <= 5) {
				Debug.Log (gameObject.name + " hurls a fireball at you!");
			} else {
				Debug.Log (gameObject.name + " says something mean to you...");
			}
		}
	}
	

}
