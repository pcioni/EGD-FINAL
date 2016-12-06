using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turret : FightBehavior {

	public override void setName ()
	{
		character_name = "Turret";
		setAIStats (50);
	}

	public override string examine ()
	{
		return "An automated turret set to target anyone not emitting a specific frequency.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (this, good_guy);


		int action = Random.Range (0, 100);
		if (action < 25 && health < 30) {
			result.Add (character_name + " begins some internal repairs!");
			result.Add (heal(25));
			ParticleManager.doEffect ("claw", target);
		} else if (action < 75) {
			result.Add (character_name + " fires a barrage of shots at " + target.character_name + "!");
			result.Add (target.damage (25, character_name));
			ParticleManager.doEffect ("machine gun", target);
		} else {
			result.Add (character_name + " fires a miniature rocket at " + target.character_name + "!");
			result.Add (target.damage (30, character_name));
			ParticleManager.doEffect ("fireball explosion", target);
		}
		return result;
	}

}
