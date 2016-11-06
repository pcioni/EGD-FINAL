using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Amelia : FightBehavior {

	public override void setName ()
	{
		character_name = "Amelia";
	}

	public override string examine ()
	{
		return "Amelia, a strong-willed gamer who always strives to be the best.";
	}

	public override List<string> listAbilities ()
	{
		List<string> result = new List<string> { "Pick an ability for " + character_name + " to use this turn!", "Berserk", "Beat Rush", "Paralyze", "Heal" };
		return result;
	}

	public override string setAbility()
	{
		switch (action_number) {

		case(1):
			managey.NeedTargeting ('n');
			return character_name + " is psyching herself up!";

		case(2):
			managey.NeedTargeting ('e');
			return character_name + " is going to let an enemy have it this turn!";

		case(3):
			managey.NeedTargeting ('e');
			return character_name + " will aim to incapacitate an enemy this turn!";

		case(4):
			managey.NeedTargeting ('a');
			return character_name + " will cast heal on a teammate this turn!";

		default:
			managey.NeedTargeting ('n');
			return character_name + " doesn't know how to use that ability...";
		}

	}

	public override List<string> useAbility ()
	{
		List<string> result = new List<string>();
		switch (action_number) {

		case(1):
			result.Add (character_name + " has gone berserk!");
			inflictStatus ("berserk", Random.Range (2, 5), gameObject);
			return result;

		case(2):
			result.Add (character_name + " launches an onslaught of attacks on " + target.GetComponent<FightBehavior> ().character_name + "!");
			result.Add (target.GetComponent<FightBehavior> ().damage (3, character_name));
			return result;

		case(3):
			result.Add (character_name + " casts a paralyzing spell on " + target.GetComponent<FightBehavior> ().character_name + "!");
			result.Add (target.GetComponent<FightBehavior> ().inflictStatus ("paralyzed", Random.Range(2,5), gameObject));
			return result;

		case(4):
			result.Add (character_name + " casts a healing spell on " + target.GetComponent<FightBehavior> ().character_name + "!");
			result.Add (target.GetComponent<FightBehavior> ().heal (3));
			return result;

		default:
			result.Add (character_name + " doesn't recognize that ability!");
			return result;
		}
	}

}