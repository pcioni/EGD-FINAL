using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThugLeader : FightBehavior {

	int reinforcement_counter;

	public override void setName ()
	{
		character_name = "Thug Leader";
		setAIStats (100);
		reinforcement_counter = 0;
	}

	public override string examine ()
	{
		return "The leader of the Vanguard Alliance. Has no patience for low-level scum.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		if (managey.getBadGuys ().Count == 1) {
			reinforcement_counter++;
		}

		if (reinforcement_counter >= 2) {
			result.Add (character_name + " messages his clan members for back-up!");
			result.AddRange (Abilities.useAbility ("Revive Team", this, managey));
			reinforcement_counter = 0;
			return result;
		}


		int action = Random.Range (0, 100);
		if (action < 20) {
			result.AddRange (Abilities.useAbility("Poison", this, target));
		} else if (action < 60) {
			result.Add (character_name + " swings violently at " + target.character_name + "!");
			result.Add (target.damage (15, character_name));
		} else if (action < 90) {
			result.Add (character_name + " throws multiple knives at " + target.character_name + "!");
			int number = Random.Range (1, 6);
			for (int x = 0; x < number; x++) {
				managey.newTarget (this, false);
				result.Add (target.damage (5, character_name));
			}
		} else {
			managey.newTargetWeakest (this, true);
			result.Add (character_name + " scarfs down food at an alarming rate!");
			result.Add (heal (20));
		}
		return result;
	}

}
