using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmandaEnemy : FightBehavior {

	bool backup_called;


	public override void setName ()
	{
		character_name = "Amanda";
		setAIStats (200);
		backup_called = false;
	}

	public override string examine ()
	{
		return "He/She/It is not very happy about how this game has unfolded.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (this, good_guy);

		int action = Random.Range (0, 100);

		if (health < 100 && !backup_called) {
			result.Add (character_name + " has called for backup!");
			managey.newCharacterArrives ("Tank", false);
			backup_called = true;
		} else if (action < 25) {
			result.Add (character_name + " fires a rocket at " + target.character_name + "!");
			result.Add (target.damage (20, character_name));
			ParticleManager.doEffect ("fireball explosion", target);
		} else if (action < 50) {
			result.Add (character_name + " takes aim with his/her(?) sidearm and fires repeeatedly!");
			int number = Random.Range (2, 6);
			for (int x = 0; x < number; x++) {
				managey.newTarget (this, false);
				result.Add (target.damage (10, character_name, ParticleManager.doEffect ("machine gun hit", target)));
			}
		} else if (action < 90 && health < 150) {
			result.Add (character_name + " quickly utilizes a medical pack!");
			result.Add (heal (30));
		} else {
			result.Add (character_name + " sets off a smoke bomb at everyone's feet!");
			result.AddRange (Abilities.useAbility ("Smoke Bomb", this, managey));
		}
		return result;

	}

}
