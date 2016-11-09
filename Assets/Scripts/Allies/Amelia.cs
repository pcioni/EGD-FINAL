using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Amelia : FightBehavior {

	public override void setName ()
	{
		character_name = "Amelia";
	}

	public override string examine ()
	{
		return "Amelia, a strong-willed gamer who always strives to be the best.";
	}

	public override void setAbilities()
	{
		abilities.AddRange(new List<string> {"Berserk", "Beat Rush", "Paralyze", "Heal"});
	}

}