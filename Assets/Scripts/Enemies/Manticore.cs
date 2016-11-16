using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manticore : FightBehavior {

	public override void setName ()
	{
		character_name = "Manticore";
		setAIStats (5);
	}

	public override string examine ()
	{
		return "A manticore: a Persian creature with the body of a lion, a human head, three rows of sharp teeth, and bat wings.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (this, good_guy);


		int action = Random.Range (0, 100);
		if (action < 25) {
			result.Add (character_name + " bites " + target.character_name + "!");
			result.Add (target.damage (1, character_name));
		} else if (action < 50) {
			result.Add (character_name + " mauls " + target.character_name + " violently!");
			result.Add (target.damage (2, character_name));
		} else {
			result.Add (character_name + " is keeping its distance...");
		}
		return result;
	}

}
