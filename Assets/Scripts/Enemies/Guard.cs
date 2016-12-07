using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guard : FightBehavior {



	public override void setName ()
	{
		character_name = "Guard";
		setAIStats (75);
	}

	public override string examine ()
	{
		return "Chivalry personified. Except for the religion thing.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (this, good_guy);

		int action = Random.Range (0, 100);

		if (action < 35) {
			result.Add (character_name + " slashes at " + target.character_name + "!");
			result.Add (target.damage (20, character_name, ParticleManager.doEffect ("claw", target)));
		} else if (action < 50) {
			result.Add (character_name + " performs some flashy huge manuever against " + target.character_name + "!");
			result.Add (target.damage (30, character_name, ParticleManager.doEffect ("claw", target)));
			result.Add ("... and manages to injure himself in the process.");
			result.Add (damage (10, character_name));
		} else if (action < 90 && health < 50) {
			result.AddRange (Abilities.useAbility ("Heal", this, target));
		} else {
			result.Add ("Screams shrilly for someone to come help!");
			result.Add ("...");
			result.Add ("Courageously...");
			managey.newCharacterArrives ("Guard", getAlignment());
		}
		return result;

	}

}
