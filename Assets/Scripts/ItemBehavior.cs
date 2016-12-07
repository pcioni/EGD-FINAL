using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemBehavior : MonoBehaviour {


	public List<string> useItem(string itemName, FightBehavior user, FightBehavior target){
		List<string> result = new List<string> ();
		switch (itemName) {
		case ("Potion"): 
			FindObjectOfType<Information> ().useItem ("Potion");
			result.Add (target.heal (30));
			return result;
		case ("Panacea Bottle"): 
			FindObjectOfType<Information> ().useItem ("Panacea Bottle");
			result.Add (target.removeNegativeEffects ());
			return result;
		case ("Magic Lens"):
			FindObjectOfType<Information> ().useItem ("Magic Lens");
			result.Add (target.examine ());
			return result;
		case ("The Kevin-Beater Bat"):
			FindObjectOfType<Information> ().useItem ("The Kevin-Beater Bat");
			result.Add(target.damage (50, user.character_name));
			return result;
		case("The Orange Overlord"):
			FindObjectOfType<Information> ().useItem ("The Orange Overlord");
			result.Add(target.inflictStatus ("paralyzed", 5, user.character_name));
			return result;
		case("Life Bottle"):
			FindObjectOfType<Information> ().useItem ("Life Bottle");
			result.Add(user.revive ());
			return result;
		case("Incense"):
			FindObjectOfType<Information> ().useItem ("Incense");
			result.Add(target.restoreMana (10));
			return result;
		case("Treasure Chest"):
			FindObjectOfType<Information> ().useItem ("Treasure Chest");
			result.Add(target.damage (50, user.character_name, ParticleManager.doEffect ("grenade", target)));
			return result;
		case("Participation Trophy"):
			result.Add ("You proudly raise your trophy in the air for all to see.");
			result.Add ("Nothing happens except for you briefly feeling good for yourself.");
			return result;
		case("Grenade"):
			result.Add (target.damage (30, user.character_name, ParticleManager.doEffect ("grenade", target)));
			return result;
		default:
			result.Add (user.character_name + " uses a " + itemName + " on " + target.character_name + "!");
			result.Add ("...");
			result.Add ("It does nothing!");
			return result;
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
		case("Grenade"):
			return 'e';
		default:
			return 'n';

		}
	}
}
