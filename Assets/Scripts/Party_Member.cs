using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Party_Member {
	
	public string name;
	public int health;
	public int max_health;
	public int mana;
	public int max_mana;

	public Party_Member(string namey, int healthy, int many){
		name = namey;
		health = max_health = healthy;
		mana = max_mana = many;
	}
}
