using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class King : FightBehavior {

	int slime_cooldown;

	public override void setName ()
	{
		character_name = "King";
		setAIStats (300);
		slime_cooldown = 2;
	}

	public override string examine ()
	{
		return "Regal. Powerful. Slimey.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (this, good_guy);

		slime_cooldown--;
		if (slime_cooldown <= 0 && managey.getBadGuys ().Count < 5) {
			result.Add ("A new slime slithers up beside the King's feet.");
			managey.newCharacterArrives ("Slime", false);
			slime_cooldown = Random.Range (2, 4);

		}

		int action = Random.Range (0, 100);

		if (action < 15) {
			result.AddRange (Abilities.useAbility ("Hush", this, managey));
		} else if (action < 25) {
			result.Add ("Slimes take a defensive stance around their king!");
			result.Add (inflictStatus ("guarded", 2, "the ring of slimes!"));
		} else if (action < 35) {
			result.Add ("Slimes take a offensive stance around their king!");
			result.Add ("The King taunts you to try attacking him now!");
			result.Add (inflictStatus ("reinforced", 2, "the ring of slimes!"));
		} else if (action < 60) {
			result.Add ("The King launches slime balls at everyone!");
			int number = Random.Range (2, 5);
			for (int x = 0; x < number; x++) {
				managey.newTarget (this, false);
				result.Add (target.damage (15, character_name, ParticleManager.doEffect ("goo", target)));
			}
		} else if (action < 80) {
			result.Add ("The King commands the slimes to consume " + target.character_name + "!");
			result.Add (target.damage(30, character_name, ParticleManager.doEffect ("goo", target)));
		} else {
			result.AddRange (Abilities.useAbility ("Paralyze", this, target));
		}
		return result;

	}

}
