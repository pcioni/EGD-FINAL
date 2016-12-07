using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dragon : FightBehavior {



	public override void setName ()
	{
		character_name = "Dragon";
		setAIStats (250);
	}

	public override string examine ()
	{
		return "Definitely not exctinct yet... but it's going to be.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (this, good_guy);

		int action = Random.Range (0, 100);

		if (action < 35) {
			result.Add (character_name + " claws at " + target.character_name + "!");
			result.Add (target.damage (30, character_name, ParticleManager.doEffect ("claw", target)));
		} else if (action < 70) {
			result.AddRange (Abilities.useAbility ("Fireball", this, target));
		} else if (action < 90) {
			result.AddRange (Abilities.useAbility ("Eruption", this, managey));
		} else {
			result.AddRange (Abilities.useAbility ("Singe", this, managey));
		}
		return result;

	}

}
