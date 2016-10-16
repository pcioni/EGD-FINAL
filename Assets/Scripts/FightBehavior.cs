using UnityEngine;
using System.Collections;

public class FightBehavior : MonoBehaviour {

	string turn_action;
	GameObject target;
	BattleManager managey;
	int health;
	bool good_guy;

	// Use this for initialization
	void Start () {
		turn_action = "AI";
		health = 3;
		managey = FindObjectOfType<BattleManager> ();
	}

	public void setAlignment(bool goodness){
		good_guy = goodness;
	}

	public string setAction(string action){
		turn_action = action;
		if (action == "attacks") {
			managey.NeedTargeting ('e');
			return gameObject.name + " will attack this turn!";
		} else if (action == "guards") {
			managey.NeedTargeting ('a');
			return gameObject.name + " will guard this turn!";
		} else if (action == "insta-kill") {
			managey.NeedTargeting ('n');
			return gameObject.name + " has invoked the win condition!";
		}
		else {
			managey.NeedTargeting ('n');
			return gameObject.name + " will " + action + " this turn!";
		}
	}

	public void setTarget(GameObject tar){
		target = tar;
	}

	public string damage (int amount, GameObject attacker){
		health -= amount;
		if (health <= 0) {
			managey.kill (gameObject);
			return gameObject.name + " has been defeated!";
		}
		return attacker.name + " attacks " + gameObject.name;;
	}

	public string doAction(){
		if (target != null && !target.activeSelf) {
			managey.newTarget (gameObject, good_guy);
		}
		switch (turn_action) {

		case ("insta-kill"):
			managey.KillEnemies ();
			return gameObject.name + " defeats all of the enemies!";

		case ("attacks"):
			managey.setPending (target.GetComponent<FightBehavior>().damage(3, gameObject));
			return gameObject.name + " attacks " + target.name;


		case ("guards"):
			return gameObject.name + " guards " + target.name;

		default:
			if (Random.Range (1, 10) <= 5) {
				return gameObject.name + " hurls a fireball at you!";
			} else {
				return gameObject.name + " says something mean to you...";
			}
		}
	}
}
