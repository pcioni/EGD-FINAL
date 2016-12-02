using UnityEngine;
using System.Collections;

public static class ParticleManager {

	/// <summary>
	/// Triggers particle system, returns time for effect to run.
	/// </summary>
	public static float doEffect(string attack, FightBehavior target){
		GameObject temp = (GameObject)GameObject.Instantiate (Resources.Load ("Particles/"+attack));	
		temp.transform.position = target.transform.position;
		return temp.GetComponent<ParticleBehavior>().GetSecondsOfParticleEffect();
	}

	/// <summary>
	/// Fires the particle effect from user to target, returns the total time the effect runs, 
	/// including its travel time.
	/// </summary>
	public static float doEffect(string attack, FightBehavior user, FightBehavior target){
		GameObject temp = (GameObject)GameObject.Instantiate (Resources.Load ("Particles/"+attack));	
		temp.GetComponent<ParticleBehavior>().moveTo_start = user.transform;
		temp.GetComponent<ParticleBehavior>().moveTo_finish = target.transform;
		return temp.GetComponent<ParticleBehavior>().GetSecondsOfParticleEffect();
	}
}
