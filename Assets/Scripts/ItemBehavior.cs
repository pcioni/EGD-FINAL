using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemBehavior : MonoBehaviour {


	public string useItem(string itemName, FightBehavior user, FightBehavior target){
		switch (itemName) {
		case ("Potion"): 
			return target.heal (3);
		case ("Panacea Bottle"): 
			return target.removeNegativeEffects ();
		case ("Magic Lens"):
			return target.examine ();
		case ("The Kevin-Beater Bat"):
			return target.damage (5, user.character_name);
		case("The Orange Overlord"):
			return target.inflictStatus ("paralyzed", 5, user.character_name);
		default:
			return user.character_name + " uses a " + itemName + " on " + target.character_name + "!";
		}
	}

	public string useItem(string itemName, FightBehavior user, bool allies){
		if (allies) {
			return user.character_name + " uses a " + itemName + " on all of their allies!";
		} else {
			return user.character_name + " uses a " + itemName + " on the entire enemy team!";
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
