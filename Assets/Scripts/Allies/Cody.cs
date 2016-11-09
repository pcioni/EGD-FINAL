using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cody : FightBehavior {

	public override void setName ()
	{
		character_name = "Cody";
	}

	public override string examine ()
	{
		return "Cody, a struggling youth that wants to prove himself useful to his friends.";
	}

	public override void setAbilities()
	{
		abilities.AddRange(new List<string> {"Fireball", "Poison", "Icicle", "Paralyze"});
	}

}