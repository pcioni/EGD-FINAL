using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClanThug1 : FightBehavior {

	public override void setName ()
	{
		character_name = "Brute";
		setAIStats (100);
	}

	public override string examine ()
	{
		return "One of the members of the clan: enjoys trolling, griefing, and harassing furries in internet forums.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();


		managey.newTarget (this, false);
		target = managey.getGoodGuys () [0];


		int action = Random.Range (0, 100);
		if (action < 20) {
			result.Add (character_name + " kicks a cloud of dirt into " + target.character_name + "'s eyes!");
			result.Add (target.inflictStatus ("blinded", Random.Range (1, 4), character_name));
		} else if (action < 60) {
			result.Add (character_name + " slashes at " + target.character_name + "!");
			result.Add (target.damage (20, character_name));
		} else if (action < 90) {
			result.Add (character_name + " kicks " + target.character_name + " in the shins!");
			result.Add (target.damage (10, character_name));
		} else {
			managey.newTargetWeakest (this, true);
			result.Add (character_name + " throws a healing potion to " + target.character_name + "!");
			result.Add (target.heal (20));
		}
		return result;
	}

}
