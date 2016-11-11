using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightBehavior : MonoBehaviour {

	[HideInInspector]
	public string turn_action;
	protected FightBehavior target;
	protected BattleManager managey;
	int health;
	int max_health;
	int mana;
	int max_mana;
	protected bool good_guy;
	Dictionary<string, int> effects;
	protected List<string> abilities;
	List<int> ability_costs;
	GameObject myHealthBar;
	[HideInInspector]
	public int action_number;
	[HideInInspector]
	public string character_name;
	int strength;

	// Use this for initialization
	void Start () {
		turn_action = "AI";
		health = max_health = 3;
		mana = max_mana = 10;
		strength = 1;
		managey = FindObjectOfType<BattleManager> ();
		effects = new Dictionary<string, int> ();
		abilities = new List<string> ();
		setAbilities ();
		ability_costs = Abilities.calculateCosts (abilities);
		//Health bar stuff
		GameObject bar = (GameObject)Instantiate (Resources.Load("Healthbar"));
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
		setName();
	}

	public virtual void setName(){
		character_name = "Unknown Name";
	}

	public void setAlignment(bool goodness){
		good_guy = goodness;
	}

	public virtual void setAbilities(){
		abilities.Add ("Heal");
		abilities.Add ("Poison");
	}

	public virtual List<string> listActions(){
		List<string> result = new List<string> {"Pick an action for " + character_name + " to do this turn!", "Attack", "Ability", "Guard", "Item"};
		return result;
	}

	public List<string> listAbilities(){
		List<string> result = new List<string> { "Pick an ability for " + character_name + " to use this turn!" };
		for (int x = 0; x < abilities.Count; x++) {
			result.Add (abilities [x] + " - " + ability_costs [x]);
		}
		return result;
	}

	public virtual string examine(){
		return character_name + ": This tells you all about this person!";
	}

	public bool enoughMana(int which){
		if (ability_costs [which - 1] > mana) {
			return false;
		}
		return true;
	}

	public string setAction(string action){
		target = null;
		turn_action = action;
		if (action == "attacks") {
			managey.NeedTargeting ('e');
			return character_name + " will attack this turn!";
		} else if (action == "guards") {
			managey.NeedTargeting ('a');
			return character_name + " will guard this turn!";
		}
		else {
			managey.NeedTargeting ('n');
			return character_name + " doesn't understand the command: " + action;
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
			turn_action = "ability";
			action_number = which;
			managey.NeedTargeting (Abilities.abilityNeedsTargeting(abilities[which - 1]));
			return character_name + " will use " + abilities [which - 1] + " this turn!";
		} else {
			managey.NeedTargeting ('n');
			return character_name + " doesn't understand the command: " + action;
		}
	}

	public void setTarget(FightBehavior tar){
		target = tar;
	}

	public string damage (int amount, string attacker){
		if (effects.ContainsKey ("guarded")) {
			return character_name + "'s guard protects them from " + attacker + "'s attack!";
		}
		health -= amount;
		StopCoroutine ("damageFlash");
		StartCoroutine ("damageFlash");
		if (health < 0) {
			health = 0;
		}
		myHealthBar.GetComponent<HealthbarBehavior> ().SetHealth (health);
		if (health <= 0) {
			managey.kill (this);
			StopCoroutine ("damageFlash");
			StartCoroutine ("deathFlash");
			return character_name + " has been defeated by " + attacker + "!";
		}
		return character_name + " takes " + amount + " damage from " + attacker;
	}

	IEnumerator damageFlash(){
		SpriteRenderer rendy = GetComponent<SpriteRenderer> ();
		rendy.color = Color.white;
		float flash_rate = 0.2f;
		while (rendy.color.g > 0f) {
			rendy.color -= new Color (0f, flash_rate, flash_rate, 0f);
			yield return new WaitForEndOfFrame ();
		}
		while (rendy.color.g < 1f) {
			rendy.color += new Color (0f, flash_rate, flash_rate, 0f);
			yield return new WaitForEndOfFrame ();
		}
		while (rendy.color.g > 0f) {
			rendy.color -= new Color (0f, flash_rate, flash_rate, 0f);
			yield return new WaitForEndOfFrame ();
		}
		while (rendy.color.g < 1f) {
			rendy.color += new Color (0f, flash_rate, flash_rate, 0f);
			yield return new WaitForEndOfFrame ();
		}
		rendy.color = Color.white;
	}

	IEnumerator deathFlash(){
		SpriteRenderer rendy = GetComponent<SpriteRenderer> ();
		rendy.color = Color.red;
		float fade_rate = 0.01f;
		while (rendy.color.a > 0f) {
			rendy.color -= new Color (0f, 0f, 0f, fade_rate);
			yield return new WaitForEndOfFrame ();
		}
		rendy.color = Color.white;
		gameObject.SetActive (false);
	}

	public string heal (int amount){
		if (health + amount > max_health) {
			amount = max_health - health;
		}
		health += amount;
		myHealthBar.GetComponent<HealthbarBehavior> ().SetHealth (health);
		return character_name + " regains " + amount + " health!";
	}

	public string restoreMana (int amount){
		if (mana + amount > max_mana) {
			amount = max_mana - mana;
		}
		mana += amount;
		return character_name + " regains " + amount + " mana!";
	}

	public string removeNegativeEffects(){
		effects.Remove ("poisoned");
		effects.Remove ("paralyzed");
		return character_name + " has been cleansed of all negative effects!";
	}

	public string inflictStatus (string status, int duration, string inflictor){
		effects.Remove (status);
		effects.Add (status, duration);
		return character_name + " was " + status + " by " + inflictor + "!";
	}

	public virtual List<string> useAbility(){
		mana -= ability_costs [action_number - 1];
		return Abilities.useAbility(abilities[action_number - 1], this, target);
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
				result.Add (character_name + " is no longer " + key);
			}
		}
		return result;
	}

	public virtual List<string> doAction(){

		List<string> result = new List<string> ();

		if (target != null && !target.gameObject.activeSelf) {
			managey.newTarget (this, good_guy);
		}

		if (effects.ContainsKey ("berserk")) {
			managey.newTarget (this, true);
			result.Add (character_name + " goes berserk on " + target.character_name + "!");
			result.Add (target.damage (strength + 1, character_name));
			return result;
		} else if (effects.ContainsKey ("paralyzed") && Random.Range(1,3) == 1) {
			result.Add (character_name + " cannot bring themself to move due to their paralysis!");
			return result;
		}

		switch (turn_action) {

		case("AI"):
			return AIAction ();


		case ("attacks"):
			result.Add (character_name + " attacks " + target.character_name + "!");
			result.Add (target.damage (strength, character_name));
			return result;


		case ("guards"):
			result.Add (character_name + " guards " + target.character_name + "!");
			result.Add (target.inflictStatus ("guarded", 1, character_name));
			return result;

		case ("item"):
			result.Add (character_name + " uses a " + managey.getItemName (action_number) + "!");
			result.Add (managey.useItem (action_number, this, target));
			return result;

		case("ability"):
			result.AddRange (useAbility ());
			return result;

		default:
			return AIAction ();
		}

	}

	public virtual List<string> AIAction(){
		List<string> result = new List<string> ();
		if (Random.Range (1, 10) <= 5) {
			managey.newTarget (this, good_guy);
			result.Add (character_name + " hurls a fireball at " + target.character_name + "!");
			result.Add (target.damage (1, character_name));
		} else {
			result.Add (character_name + " says something mean to you...");
		}
		return result;
	}
}
