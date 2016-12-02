using UnityEngine;
using System.Collections;

public static class ParticleManager {

	public static int doEffect(string attack, FightBehavior user, FightBehavior target){

		switch (attack) {
			
		case("fireball"):
			ParticleBehavior temp = (ParticleBehavior)GameObject.Instantiate (Resources.Load ("Particles/fireball"));
			temp.moveTo_start = user.transform;
			temp.moveTo_finish = target.transform;
			return 0;

		default:
			return 0;
		}

	}
}
