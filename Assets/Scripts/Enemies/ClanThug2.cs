using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClanThug2 : FightBehavior {

	public override void setName ()
	{
		character_name = "Thug";
		setAIStats (100);
	}

	public override string examine ()
	{
		return "One of the members of the clan: loves luring noobs and scamming people out of their money.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();


		managey.newTarget (this, false);


		int action = Random.Range (0, 100);
		if (action < 20) {
			result.AddRange (useAbility ("Poison"));
		} else if (action < 60) {
			result.Add (character_name + " fires a ball of pure energy at " + target.character_name + "!");
			result.Add (target.damage (20, character_name));
		} else if (action < 90) {
			result.Add (character_name + " spams attacks randomly at " + target.character_name + "!");
			result.Add (target.damage (Random.Range(10, 30), character_name));
		} else {
			managey.newTargetWeakest (this, true);
			result.AddRange (useAbility ("Heal"));
		}
		return result;
	}

}
