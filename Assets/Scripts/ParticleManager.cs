using UnityEngine;
using System.Collections;

public static class ParticleManager {

	/// <summary>
	/// Triggers the specified particle effect. Including a user AND target
	/// will cause the system to move from user to target. Including just
	/// one of those will cause the system to occur at that object.
	/// Returns the duration of the particle effect.
	/// </summary>
	/// <returns>The duration of particle effect.</returns>
	public static float doEffect(string attack, FightBehavior target){
		GameObject temp = (GameObject)GameObject.Instantiate (Resources.Load ("Particles/"+attack));	
		temp.transform.position = target.transform.position;
		return temp.GetComponent<ParticleBehavior>().GetSecondsOfParticleEffect();
	}

	public static float doEffect(string attack, FightBehavior user, FightBehavior target){
		GameObject temp = (GameObject)GameObject.Instantiate (Resources.Load ("Particles/"+attack));	
		temp.GetComponent<ParticleBehavior>().moveTo_start = user.transform;
		temp.GetComponent<ParticleBehavior>().moveTo_finish = target.transform;
		return temp.GetComponent<ParticleBehavior>().GetSecondsOfParticleEffect();
	}
}
