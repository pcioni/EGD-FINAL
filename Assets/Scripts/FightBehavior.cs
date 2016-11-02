using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightBehavior : MonoBehaviour {

	string turn_action;
	protected GameObject target;
	protected BattleManager managey;
	int health;
	int max_health;
	protected bool good_guy;
	Dictionary<string, int> effects;
	GameObject myHealthBar;
	public GameObject healthbar_prefab;
	int action_number;

	// Use this for initialization
	void Start () {
		turn_action = "AI";
		health = 3;
		max_health = 3;
		managey = FindObjectOfType<BattleManager> ();
		effects = new Dictionary<string, int> ();

		//Health bar stuff
		GameObject bar = Instantiate (healthbar_prefab);
		bar.transform.SetParent(GameObject.Find ("Floating Character Canvas").transform);
		bar.transform.localScale = Vector3.one;
		myHealthBar = bar;
		//calculate the distance the healthbar sits as a function of how
		//tall our character is
		float half_height = GetComponent<SpriteRenderer>().bounds.extents.y;
		float height_above = .1f * half_height + half_height;
		myHealthBar.transform.position = new Vector3(transform.position.x,
			transform.position.y + height_above, 0);
		myHealthBar.GetComponent<HealthbarBehavior> ().defaultHealth (health);
	}

	public void setAlignment(bool goodness){
		good_guy = goodness;
	}

	public virtual List<string> listActions(){
		List<string> result = new List<string> {"Pick an action for " + name + " to do this turn!", "Attack", "Ability", "Guard", "Item"};
		return result;
	}

	public virtual List<string> listAbilities(){
		List<string> result = new List<string> { "Pick an ability for " + name + " to use this turn!", "Poison", "Heal", "", "" };
		return result;
	}

	public virtual string examine(){
		return gameObject.name + ": This tells you all about this person!";
	}

	public string setAction(string action){
		target = null;
		turn_action = action;
		if (action == "attacks") {
			managey.NeedTargeting ('e');
			return gameObject.name + " will attack this turn!";
		} else if (action == "guards") {
			managey.NeedTargeting ('a');
			return gameObject.name + " will guard this turn!";
		}
		else {
			managey.NeedTargeting ('n');
			return gameObject.name + " doesn't understand the command: " + action;
		}
	}

	public string setAction(string action, int which){
		target = null;
		if (action == "item") {
			turn_action = "item";
			action_number = which;
			managey.NeedTargeting (managey.itemNeedsTargeting (action_number));
			return gameObject.name + " will use " + managey.getItemName (action_number) + " this turn!";
		} else if (action == "ability") {
			if (which == 1) {
				turn_action = "poison";
				action_number = 1;
				managey.NeedTargeting ('e');
				return gameObject.name + " will poison an enemy this turn!";
			} else {
				turn_action = "heal";
				action_number = 2;
				managey.NeedTargeting ('a');
				return gameObject.name + " will heal an ally this turn!";
			}
		} else {
			managey.NeedTargeting ('n');
			return gameObject.name + " doesn't understand the command: " + action;
		}
	}

	public void setTarget(GameObject tar){
		target = tar;
	}

	public string damage (int amount, string attacker){
		if (effects.ContainsKey ("guarded")) {
			return gameObject.name + "'s guard protects them from " + attacker + "'s attack!";
		}
		health -= amount;
		myHealthBar.GetComponent<HealthbarBehavior> ().SetHealth (health);
		if (health <= 0) {
			managey.kill (gameObject);
			return gameObject.name + " has been defeated by " + attacker + "!";
		}
		return gameObject.name + " takes " + amount + " damage from " + attacker;
	}

	public string heal (int amount){
		if (health + amount > max_health) {
			amount = max_health - health;
		}
		health += amount;
		myHealthBar.GetComponent<HealthbarBehavior> ().SetHealth (health);
		return gameObject.name + " regains " + amount + " health!";
	}

	public string removeNegativeEffects(){
		effects.Remove ("poison");
		return gameObject.name + " has been cleansed of all negative effects!";
	}

	public string inflictStatus (string status, int duration, GameObject inflictor){
		effects.Remove (status);
		effects.Add (status, duration);
		return gameObject.name + " was " + status + " by " + inflictor.name + "!";
	}

	public List<string> endTurn(){
		List<string> result = new List<string> ();
		if (effects.Count == 0) {
			return result;
		}
		string[] keys = new string[effects.Count];
		effects.Keys.CopyTo (keys, 0);
		foreach (string key in keys) {

			if (key == "poisoned") {
				result.Add (damage (1, "poison"));
			}

			effects[key] = effects[key] - 1;
			if (effects[key] <= 0) {
				effects.Remove (key);
				result.Add (gameObject.name + "'s " + key + " has worn off!");
			}
		}
		return result;
	}

	public virtual List<string> doAction(){

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
				result.Add (targets [x].GetComponent<FightBehavior> ().damage (9999, name));
			}
			return result;


		case ("attacks"):
			result.Add (gameObject.name + " attacks " + target.name);
			result.Add (target.GetComponent<FightBehavior> ().damage (1, name));
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
				result.Add (targets2 [x].GetComponent<FightBehavior> ().damage (1, name));
			}
			return result;

		case ("item"):
			result.Add (gameObject.name + " uses a " + managey.getItemName (action_number) + "!");
			result.Add (managey.useItem (action_number, gameObject, target));
			return result;

		case ("poison"):
			result.Add (gameObject.name + " shoots a poisonous dart at " + target.name);
			result.Add (target.GetComponent<FightBehavior> ().inflictStatus ("poisoned", Random.Range (2, 5), gameObject));
			return result;

		case("heal"):
			result.Add (gameObject.name + " heals " + target.name);
			result.Add (target.GetComponent<FightBehavior> ().heal (5));
			return result;

		default:
			if (Random.Range (1, 10) <= 5) {
				managey.newTarget (gameObject, good_guy);
				result.Add (gameObject.name + " hurls a fireball at " + target.name + "!");
				result.Add (target.GetComponent<FightBehavior> ().damage (1, name));
			} else {
				result.Add (gameObject.name + " says something mean to you...");
			}
			return result;
		}

	}
}
