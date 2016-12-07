using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soldier : FightBehavior {


	public override void setName ()
	{
		character_name = "Soldier";
		setAIStats (60);
	}

	public override string examine ()
	{
		return "He's barely a step above cannon fodder. But he's going for getting points.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (this, good_guy);

		int action = Random.Range (0, 100);

		if (action < 60) {
			result.Add (character_name + " fires at " + target.character_name + "!");
			result.Add (target.damage (Random.Range(10, 20), character_name, ParticleManager.doEffect ("machine gun hit", target)));
		} else if (action < 80) {
			result.Add (character_name + " chucks a grenade at " + target.character_name + "!");
			result.Add (target.damage (Random.Range(15, 30), character_name, ParticleManager.doEffect ("grenade", this, target)));
		} else if (action < 93 && health < 40) {
			result.Add (character_name + " applies a bandage to his wounds!");
			result.Add (heal (25));
		} else if (managey.getBadGuys ().Count < 5) {
			result.Add (character_name + " calls in some backup!");
			managey.newCharacterArrives ("Soldier", false);
		} else {
			result.Add (character_name + " fires at " + target.character_name + "!");
			result.Add (target.damage (20, character_name, ParticleManager.doEffect ("machine gun hit", target)));
		}
		return result;

	}

}
