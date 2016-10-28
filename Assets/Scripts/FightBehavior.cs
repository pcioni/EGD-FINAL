using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightBehavior : MonoBehaviour {

	string turn_action;
	GameObject target;
	BattleManager managey;
	int health;
	bool good_guy;
	Dictionary<string, int> effects;
	GameObject myHealthBar;
	public GameObject healthbar_prefab;

	// Use this for initialization
	void Start () {
		turn_action = "AI";
		health = 3;
		managey = FindObjectOfType<BattleManager> ();
		effects = new Dictionary<string, int> ();

		//Health bar stuff
		GameObject bar = Instantiate (healthbar_prefab);
		bar.transform.parent = GameObject.Find ("Floating Character Canvas").transform;
		bar.transform.localScale = Vector3.one;
		myHealthBar = bar;
		//calculate the distance the healthbar sits as a function of how
		//tall our character is
		float half_height = GetComponent<SpriteRenderer>().bounds.extents.y;
		float height_above = .1f * half_height + half_height;
		myHealthBar.transform.position = new Vector3(transform.position.x,
			transform.position.y + height_above, 0);
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
		} else if (action == "hail-mary") {
			managey.NeedTargeting ('n');
			return gameObject.name + " is preparing a big AoE attack!";
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
		if (effects.ContainsKey ("guarded")) {
			return gameObject.name + "'s guard protects them from " + attacker.name + "'s attack!";
		}
		health -= amount;
		myHealthBar.GetComponent<HealthbarBehavior> ().SetHealth (health);
		if (health <= 0) {
			managey.kill (gameObject);
			return gameObject.name + " has been defeated by " + attacker.name + "!";
		}
		return gameObject.name + " takes " + amount + " damage from " + attacker.name;
	}

	public string inflictStatus (string status, int duration, GameObject inflictor){
		effects.Remove (status);
		effects.Add (status, duration);
		return gameObject.name + " was " + status + " by " + inflictor.name + "!";
	}

	public List<string> doAction(){

		List<string> result = new List<string> ();

		if (target != null && !target.activeSelf) {
			managey.newTarget (gameObject, good_guy);
		}
		switch (turn_action) {

		case ("insta-kill"):
			result.Add (gameObject.name + " launches a devastating, insta-death attack!");
			List<GameObject> targets = new List<GameObject> ();
			targets.AddRange (managey.getBadGuys ());
			for (int x = 0; x < targets.Count; x++) {
				result.Add (targets [x].GetComponent<FightBehavior> ().damage (9999, gameObject));
			}
			return result;


		case ("attacks"):
			result.Add (gameObject.name + " attacks " + target.name);
			result.Add (target.GetComponent<FightBehavior> ().damage (1, gameObject));
			return result;


		case ("guards"):
			result.Add (gameObject.name + " guards " + target.name);
			result.Add (target.GetComponent<FightBehavior> ().inflictStatus ("guarded", 1, gameObject));
			return result;

		case ("hail-mary"):
			result.Add (gameObject.name + " launches a wave of attacks across the entire enemy line!");
			List<GameObject> targets2 = new List<GameObject> ();
			targets2.AddRange (managey.getBadGuys ());
			for (int x = 0; x < targets2.Count; x++) {
				result.Add (targets2 [x].GetComponent<FightBehavior> ().damage (1, gameObject));
			}
			return result;

		default:
			if (Random.Range (1, 10) <= 5) {
				managey.newTarget (gameObject, good_guy);
				result.Add (gameObject.name + " hurls a fireball at " + target.name + "!");
				result.Add (target.GetComponent<FightBehavior> ().damage (1, gameObject));
			} else {
				result.Add (gameObject.name + " says something mean to you...");
			}
			return result;
		}

	}
}
