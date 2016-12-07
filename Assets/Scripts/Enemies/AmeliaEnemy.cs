using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmeliaEnemy : FightBehavior {


	public override void setName ()
	{
		character_name = "Amelia";
		setAIStats (140);
	}

	public override string examine ()
	{
		return "Wow, she's mad.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (this, good_guy);

		int action = Random.Range (0, 100);

		if (action < 25) {
			result.Add (character_name + " charges in with her fists bared at " + target.character_name + "!");
			result.AddRange (Abilities.useAbility ("Beat Rush", this, target));
		} else if (action < 50) {
			result.Add (character_name + " shoots off some rounds at " + target.character_name + " with tears in her eyes...");
			result.Add (target.damage (35, character_name, ParticleManager.doEffect ("machine gun hit", target)));
		} else if (action < 85 && health < 150) {
			result.Add (character_name + " shoots at " + target.character_name + " with siphoning bullets!");
			int amount = Random.Range (15, 30);
			result.Add (heal (amount));
			result.Add (target.damage (amount, character_name));
		} else {
			result.Add (character_name + " calls in an airstrike on her friends...");
			result.AddRange (Abilities.useAbility ("Air Strike", this, managey));
		}
		return result;

	}

}
