using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Abilities {

	public static List<int> calculateCosts(List<string> abilities){
		List<int> result = new List<int> ();
		for (int x = 0; x < abilities.Count; x++) {
			switch (abilities [x]) {

			case("Heal"):
				result.Add (2);
				continue;

			case("Poison"):
				result.Add (2);
				continue;

			case("Fireball"):
				result.Add (2);
				continue;

			case("Lightning"):
				result.Add (3);
				continue;

			case("Icicle"):
				result.Add (2);
				continue;

			case("Berserk"):
				result.Add (1);
				continue;

			case("Beat Rush"):
				result.Add (3);
				continue;

			case("Paralyze"):
				result.Add (2);
				continue;

			default:
				result.Add (0);
				continue;
			}
		}
		return result;
	}

	public static char abilityNeedsTargeting(string ability){
		switch (ability) {

		case("Heal"):
			return 'a';

		case("Poison"):
			return 'e';

		case("Fireball"):
			return 'e';

		case("Lightning"):
			return 'e';

		case("Icicle"):
			return 'e';

		case("Berserk"):
			return 'n';

		case("Beat Rush"):
			return 'e';

		case("Paralyze"):
			return 'e';

		default:
			return 'n';

		}
	}

	public static List<string> useAbility(string ability, FightBehavior user, BattleManager managey){
		List<string> result = new List<string> ();

		switch (ability) {

		case("Revive Team"):
			result.AddRange (managey.reviveTeam (user.getAlignment()));
			return result;
		default:
			result.Add (user.character_name + " tried to use a non-existent ability!");
			return result;
		}
	}

	public static List<string> useAbility(string ability, FightBehavior user, FightBehavior target){
		List<string> result = new List<string> ();

		switch (ability) {

		case("Heal"):
			result.Add (user.character_name + " casts a healing spell on " + target.character_name + "!");
			result.Add (target.heal (30));
			return result;

		case("Poison"):
			result.Add (user.character_name + " summons poisonous clouds of gas around " + target.character_name + "!");
			result.Add (target.inflictStatus ("poisoned", Random.Range (2, 5), user.character_name));
			return result;

		case("Fireball"):
			result.Add (user.character_name + " launches a fireball at " + target.character_name + "!");
			result.Add (target.damage (20, user.character_name));
			return result;

		case("Lightning"):
			result.Add (user.character_name + " summons a lightning bolt upon " + target.character_name + "!");
			result.Add (target.damage (Random.Range (10, 30), user.character_name));
			return result;

		case("Icicle"):
			result.Add (user.character_name + " extrudes razor-sharp icicles below " + target.character_name + "!");
			result.Add (target.damage (20, user.character_name));
			return result;

		case("Berserk"):
			result.Add (user.character_name + " has gone berserk!");
			user.inflictStatus ("berserk", Random.Range (2, 5), user.character_name);
			return result;

		case("Beat Rush"):
			result.Add (user.character_name + " launches an onslaught of attacks on " + target.character_name + "!");
			result.Add (target.damage (30, user.character_name));
			return result;

		case("Paralyze"):
			result.Add (user.character_name + " casts a paralyzing spell on " + target.character_name + "!");
			result.Add (target.inflictStatus ("paralyzed", Random.Range(2,5), user.character_name));
			return result;

		default:
			result.Add (user.character_name + " tried to use a non-existent ability!");
			return result;
		}
	}

}
