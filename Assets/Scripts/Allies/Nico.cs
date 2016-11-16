using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nico : FightBehavior {

	public override void setName ()
	{
		character_name = "Nico";
		setStats ();
	}

	public override string examine ()
	{
		return "Nico, an eccentric prankster that the gang just can't quite seem to shake.";
	}
		
	public override void setAbilities()
	{
		abilities.AddRange(new List<string> {"Poison", "Poison", "Poison", "Beat Rush"});
	}

}