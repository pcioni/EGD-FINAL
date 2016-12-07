using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemBehavior : MonoBehaviour {


	public string useItem(string itemName, FightBehavior user, FightBehavior target){
		switch (itemName) {
		case ("Potion"): 
			FindObjectOfType<Information> ().useItem ("Potion");
			return target.heal (30);
		case ("Panacea Bottle"): 
			FindObjectOfType<Information> ().useItem ("Panacea Bottle");
			return target.removeNegativeEffects ();
		case ("Magic Lens"):
			FindObjectOfType<Information> ().useItem ("Magic Lens");
			return target.examine ();
		case ("The Kevin-Beater Bat"):
			FindObjectOfType<Information> ().useItem ("The Kevin-Beater Bat");
			return target.damage (50, user.character_name);
		case("The Orange Overlord"):
			FindObjectOfType<Information> ().useItem ("The Orange Overlord");
			return target.inflictStatus ("paralyzed", 5, user.character_name);
		case("Life Bottle"):
			FindObjectOfType<Information> ().useItem ("Life Bottle");
			return user.revive ();
		case("Incense"):
			FindObjectOfType<Information> ().useItem ("Incense");
			return target.restoreMana (10);
		case("Treasure Chest"):
			FindObjectOfType<Information> ().useItem ("Treasure Chest");
			return target.damage (50, user.character_name, ParticleManager.doEffect ("grenade", target));
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
		case("The Orange Overlord"):
			return 'e';
		case("Life Bottle"):
			return 'd';
		case ("Incense"):
			return 'a';
		case ("Treasure Chest"):
			return 'e';
		default:
			return 'n';

		}
	}
}
