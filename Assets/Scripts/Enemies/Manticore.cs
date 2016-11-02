using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manticore : FightBehavior {

	public override string examine ()
	{
		return "A manticore: a Persian creature with the body of a lion, a human head, three rows of sharp teeth, and bat wings.";
	}

	public override List<string> doAction ()
	{
		List<string> result = new List<string> ();

		managey.newTarget (gameObject, good_guy);


		int action = Random.Range (0, 100);
		if (action < 25) {
			result.Add (name + " bites " + target.name + "!");
			result.Add (target.GetComponent<FightBehavior> ().damage (1, name));
		} else if (action < 50) {
			result.Add (name + " mauls " + target.name + " violently!");
			result.Add (target.GetComponent<FightBehavior> ().damage (2, name));
		} else {
			result.Add (name + " is keeping its distance...");
		}
		return result;
	}

}
