using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sam : FightBehavior {

	public override void setName ()
	{
		character_name = "Sam";
		setStats ();
	}

	public override string examine ()
	{
		return "Sam, a kind-hearted gamer who would do anything to protect his friends.";
	}
		
	public override void setAbilities()
	{
		abilities.AddRange(new List<string> {"Fireball", "Lightning", "Icicle", "Heal"});
	}

}