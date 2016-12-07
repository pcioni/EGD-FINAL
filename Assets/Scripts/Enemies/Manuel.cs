using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manuel : FightBehavior {

	bool backup_called;


	public override void setName ()
	{
		character_name = "Manuel";
		setAIStats (200);
		backup_called = false;
	}

	public override string examine ()
	{
		return "He/She/It is still not very happy.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (this, good_guy);

		int action = Random.Range (0, 100);

		if (health < 100 && !backup_called) {
			result.Add (character_name + " has called for backup!");
			managey.newCharacterArrives ("Clan Thug 1", false);
			managey.newCharacterArrives ("Clan Thug 2", false);
			backup_called = true;
		} else if (action < 25) {
			result.AddRange (Abilities.useAbility ("Lightning", this, target));
		} else if (action < 50) {
			result.Add (character_name + " unleashes a number of quick slashes!");
			int number = Random.Range (2, 6);
			for (int x = 0; x < number; x++) {
				managey.newTarget (this, false);
				result.Add (target.damage (10, character_name, ParticleManager.doEffect ("claw", target)));
			}
		} else if (action < 90 && health < 150) {
			result.AddRange (Abilities.useAbility ("Heal", this, this));
		} else {
			result.AddRange (Abilities.useAbility ("Tremor", this, managey));
		}
		return result;

	}

}
