using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreasureChest : FightBehavior {


	public override void setName ()
	{
		character_name = "Mimic";
		setAIStats (100);
	}

	public override string examine ()
	{
		return "The last of the Mahoganians.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (this, good_guy);

		int action = Random.Range (0, 100);

		if (action < 50) {
			result.Add (character_name + " clamps shut defensively!");
			result.Add (inflictStatus ("guarded", 2, character_name));
		} else if (action < 60) {
			result.Add (character_name + " spits up a potion!");
			result.Add (heal (30));
		} else if (action < 70) {
			result.Add (character_name + " spits up a grenade at " + target.character_name + "!");
			result.Add (target.damage(25, character_name, ParticleManager.doEffect("grenade", this, target)));
		} else if (action < 80) {
			result.Add (character_name + " spits up a poison flask on " + target.character_name + "!");
			result.AddRange (Abilities.useAbility("Poison", this, target));
		} else if (action < 90) {
			result.Add (character_name + " spits up multiple knives!");
			int amount = Random.Range (2, 6);
			for (int x = 0; x < amount; x++) {
				result.Add (target.damage (12, character_name, ParticleManager.doEffect ("thug attack", this, target)));
			}
		} else {
			result.Add (character_name + " spits up a...");
			result.Add ("...");
			result.Add ("I don't even know what that was, but it didn't do anything.");
		} 
		return result;

	}

}
