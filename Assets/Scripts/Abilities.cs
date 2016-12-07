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

			case("Air Strike"):
				result.Add (4);
				continue;

			case("Frag"):
				result.Add (2);
				continue;

			case("Knife Frenzy"):
				result.Add (4);
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

		case("Frag"):
			return 'e';

		default:
			return 'n';

		}
	}

	public static List<string> useAbility(string ability, FightBehavior user, BattleManager managey){
		List<string> result = new List<string> ();
		List<FightBehavior> targets;

		switch (ability) {

		case("Revive Team"):
			result.AddRange (managey.reviveTeam (user.getAlignment()));
			return result;

		case("Smoke Bomb"):
			result.Add (user.character_name + " sets off a smoke bomb at everyone's feet!");
			result.AddRange (managey.applyStatusToAll ("blinded", user, Random.Range(1, 3)));
			return result;

		case("Air Strike"):
			if (user.getAlignment ()) {
				targets = managey.getBadGuys ();
			} else {
				targets = managey.getGoodGuys ();
			}
			for (int x = 0; x < targets.Count; x++) {
				result.Add (targets [x].damage (20, user.character_name, ParticleManager.doEffect("fireball explosion", targets[x])));
			}
			return result;

		case("Tremor"):
			result.Add (user.character_name + " creates a powerful shockwave in the ground beneath everyone!");
			result.AddRange (managey.applyStatusToAll ("paralyzed", user, Random.Range(1, 4)));
			return result;

		case("Singe"):
			result.Add (user.character_name + " scorches the entire field in red-hot flames!");
			result.AddRange (managey.applyStatusToAll ("burned", user, Random.Range(1, 3)));
			return result;

		case("Eruption"):
			result.Add (user.character_name + " begins to melt the ground beneath everyone!");
			if (user.getAlignment ()) {
				targets = managey.getBadGuys ();
			} else {
				targets = managey.getGoodGuys ();
			}
			for (int x = 0; x < targets.Count; x++) {
				result.Add (targets [x].damage (20, user.character_name, ParticleManager.doEffect("fireball", user, targets[x])));
			}
			return result;

		case("Hush"):
			result.Add (user.character_name + " summons a cloud of obscurity over the battlefield!");
			result.Add ("Abilities are restricted next turn!");
			result.AddRange (managey.applyStatusToAll ("silence", user, 2));
			return result;

		case("Knife Frenzy"):
			result.Add (user.character_name + " unleashes a flurry of knives upon the enemy!");
			if (user.getAlignment ()) {
				targets = managey.getBadGuys ();
			} else {
				targets = managey.getGoodGuys ();
			}
			int attack_number = Random.Range (2, 6);
			for (int x = 0; x < attack_number; x++) {
				int target_number = Random.Range (0, targets.Count);
				result.Add (targets [target_number].damage (15, user.character_name, ParticleManager.doEffect("thug attack", user, targets[target_number])));
			}
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
			ParticleManager.doEffect ("poison", target);
			result.Add (user.character_name + " summons poisonous clouds of gas around " + target.character_name + "!");
			result.Add (target.inflictStatus ("poisoned", Random.Range (2, 5), user.character_name));
			return result;

		case("Fireball"):
			result.Add (user.character_name + " launches a fireball at " + target.character_name + "!");
			result.Add (target.damage (20, user.character_name, ParticleManager.doEffect ("fireball", user, target)));
			if (Random.Range (0, 10) == 0) {
				result.Add (target.inflictStatus ("burned", Random.Range (2, 4), user.character_name));
			}
			return result;

		case("Lightning"):
			result.Add (user.character_name + " summons a lightning bolt upon " + target.character_name + "!");
			result.Add (target.damage (Random.Range (10, 30), user.character_name, ParticleManager.doEffect ("bolt", target)));
			return result;

		case("Icicle"):
			result.Add (user.character_name + " extrudes razor-sharp icicles below " + target.character_name + "!");
			result.Add (target.damage (20, user.character_name, ParticleManager.doEffect ("icicle", user, target)));
			return result;

		case("Berserk"):
			ParticleManager.doEffect ("enrage", user);
			result.Add (user.character_name + " has gone berserk!");
			user.inflictStatus ("berserk", Random.Range (2, 5), user.character_name);
			return result;

		case("Beat Rush"):
			result.Add (user.character_name + " launches an onslaught of attacks on " + target.character_name + "!");
			result.Add (target.damage (30, user.character_name, ParticleManager.doEffect ("beat rush", target)));
			return result;

		case("Paralyze"):
			result.Add (user.character_name + " casts a paralyzing spell on " + target.character_name + "!");
			result.Add (target.inflictStatus ("paralyzed", Random.Range(2,5), user.character_name));
			return result;

		case("Frag"):
			result.Add (user.character_name + " lobs a grenade at " + target.character_name + "!");
			result.Add (target.damage (30, user.character_name, ParticleManager.doEffect ("grenade", user, target)));
			return result;

		default:
			result.Add (user.character_name + " tried to use a non-existent ability!");
			return result;
		}
	}

}
