using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manticore : FightBehavior {

	public override void setName ()
	{
		character_name = "Manticore";
	}

	public override string examine ()
	{
		return "A manticore: a Persian creature with the body of a lion, a human head, three rows of sharp teeth, and bat wings.";
	}

	public override List<string> AIAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (gameObject, good_guy);


		int action = Random.Range (0, 100);
		if (action < 25) {
			result.Add (character_name + " bites " + target.GetComponent<FightBehavior>().character_name + "!");
			result.Add (target.GetComponent<FightBehavior> ().damage (1, name));
		} else if (action < 50) {
			result.Add (character_name + " mauls " + target.GetComponent<FightBehavior>().character_name + " violently!");
			result.Add (target.GetComponent<FightBehavior> ().damage (2, name));
		} else {
			result.Add (name + " is keeping its distance...");
		}
		return result;
	}

}
