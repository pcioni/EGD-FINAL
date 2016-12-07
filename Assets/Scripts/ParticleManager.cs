using UnityEngine;
using System.Collections;

public static class ParticleManager {

	/// <summary>
	/// Triggers particle system, returns time for effect to run.
	/// </summary>
	public static float doEffect(string attack, FightBehavior target){
		GameObject temp = (GameObject)GameObject.Instantiate (Resources.Load ("Particles/"+attack));	
		temp.transform.position = target.transform.position;
		if (temp.GetComponent<ParticleBehavior> () != null) {
			ParticleBehavior pb = temp.GetComponent<ParticleBehavior> ();
			return pb.GetSecondsOfParticleEffect();
		} else {
			ParticleBehavior pb = temp.transform.GetComponentInChildren<ParticleBehavior> ();
			return pb.GetSecondsOfParticleEffect();
		}

	}

	public static void doEffect(string effect, Transform location){
		GameObject temp = (GameObject)GameObject.Instantiate (Resources.Load ("Particles/" + effect));
		temp.transform.position = location.position;
	}

	/// <summary>
	/// Fires the particle effect from user to target, returns the total time the effect runs, 
	/// including its travel time.
	/// </summary>
	public static float doEffect(string attack, FightBehavior user, FightBehavior target){
		GameObject temp = (GameObject)GameObject.Instantiate (Resources.Load ("Particles/"+attack));	
		ParticleBehavior pb = temp.GetComponent<ParticleBehavior>(); 
		if (pb == null)
			pb = pb.transform.GetComponentInChildren<ParticleBehavior> ();
		pb.moveTo_start = user.transform;
		pb.moveTo_finish = target.transform;
		return pb.GetSecondsOfParticleEffect();
	}
}
