using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemBehavior : MonoBehaviour {


	public string useItem(string itemName, GameObject user, GameObject target){
		switch (itemName) {
		case ("Potion"): 
			return target.GetComponent<FightBehavior> ().heal (3);
		case ("Panacea Bottle"): 
			return target.GetComponent<FightBehavior> ().removeNegativeEffects ();
		case ("Magic Lens"):
			return target.GetComponent<FightBehavior> ().examine ();
		case ("The Kevin-Beater Bat"):
			return target.GetComponent<FightBehavior> ().damage (5, user.name);
		default:
			return user.name + " uses a " + itemName + " on " + target.name + "!";
		}
	}

	public string useItem(string itemName, GameObject user, bool allies){
		if (allies) {
			return user.name + " uses a " + itemName + " on all of their allies!";
		} else {
			return user.name + " uses a " + itemName + " on the entire enemy team!";
		}
	}

	public char needsTargeting(string itemName){
		switch (itemName) {

		case("Potion"):
			return 'a';
		case("Panacea Bottle"):
			return 'a';
		case("Magic Lens"):
			return 'e';
		case("The Kevin-Beater Bat"):
			return 'e';
		default:
			return 'n';

		}
	}
}
