﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Slime : FightBehavior {

	public override void setName ()
	{
		character_name = "Slime";
		setAIStats (30);
	}

	public override string examine ()
	{
		return "A slime: a gelatinous concoction whose only purpose in life is to feed and grow.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (this, good_guy);

		int action = Random.Range (0, 100);
		if (action < 50 && health != max_health) {
			result.Add (character_name + " takes some time to recompose its matter.");
			result.Add (heal (20));

		} else {
			result.Add (character_name + " painfully wraps around " + target.character_name + "'s leg!");
			result.Add (target.damage (10, character_name, ParticleManager.doEffect ("goo", target)));
		}
		return result;
	}

}
