using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manticore : FightBehavior {

	bool manticore_enraged;

	public override void setName ()
	{
		character_name = "Manticore";
		setAIStats (50);
		manticore_enraged = false;
	}

	public override string examine ()
	{
		return "A manticore: a Persian creature with the body of a lion, a human head, three rows of sharp teeth, and bat wings.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		if (!manticore_enraged && health < 3) {
			manticore_enraged = true;
			result.Add (character_name + " has become enraged by its wounds!");
		}

		managey.newTarget (this, good_guy);


		int action = Random.Range (0, 100);
		if (action < 25) {
			result.Add (character_name + " bites " + target.character_name + "!");
			result.Add (target.damage (10, character_name));
		} else if (action < 50 || manticore_enraged) {
			result.Add (character_name + " mauls " + target.character_name + " violently!");
			result.Add (target.damage (20, character_name));
		} else {
			result.Add (character_name + " is keeping its distance...");
		}
		return result;
	}

}
