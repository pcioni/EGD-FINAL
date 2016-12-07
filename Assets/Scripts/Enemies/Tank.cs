using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tank : FightBehavior {

	bool aimed;


	public override void setName ()
	{
		character_name = "Tank";
		setAIStats (200);
		aimed = false;
	}

	public override string examine ()
	{
		return "Big, heavy, and deadly. Your best option is to run away.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (this, good_guy);

		if (!aimed) {
			result.Add (character_name + " is lining up a shot...");
			aimed = true;
		} else {
			result.Add (character_name + " fires a tank shell directly at " + target.character_name + "!");
			result.Add (target.damage (40, character_name));
			aimed = false;
		}

		return result;
	}

}
