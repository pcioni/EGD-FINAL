using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sam : FightBehavior {

	public override void setName ()
	{
		character_name = "Sam";
	}

	public override string examine ()
	{
		return "Sam, a kind-hearted gamer who would do anything to protect his friends.";
	}

	public override List<string> listAbilities ()
	{
		List<string> result = new List<string> { "Pick an ability for " + character_name + " to use this turn!", "Fireball", "Lightning", "Icicle", "Heal" };
		return result;
	}

	public override string setAbility()
	{
		switch (action_number) {

		case(1):
			managey.NeedTargeting ('e');
			return character_name + " will cast fireball at an enemy this turn!";

		case(2):
			managey.NeedTargeting ('e');
			return character_name + " will cast lightning at an enemy this turn!";

		case(3):
			managey.NeedTargeting ('e');
			return character_name + " will cast icicle at an enemy this turn!";

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
			result.Add (character_name + " launches a fireball at " + target.GetComponent<FightBehavior> ().character_name + "!");
			result.Add (target.GetComponent<FightBehavior> ().damage (2, character_name));
			return result;

		case(2):
			result.Add (character_name + " summons lightning to strike " + target.GetComponent<FightBehavior> ().character_name + "!");
			result.Add (target.GetComponent<FightBehavior> ().damage (2, character_name));
			return result;

		case(3):
			result.Add (character_name + " extrudes sharp icicles beneath " + target.GetComponent<FightBehavior> ().character_name + "!");
			result.Add (target.GetComponent<FightBehavior> ().damage (2, character_name));
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