using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cody2 : FightBehavior {

	public Sprite idle1;
	public Sprite idle2;
	public Sprite attack;
	public float animation_speed;
	int idle_state;
	SpriteRenderer rendy;

	public override void setName ()
	{
		character_name = "Cody";
		setStats ();
		rendy = GetComponent<SpriteRenderer> ();
		StartCoroutine (animate ());
	}

	public override string examine ()
	{
		return "Cody, a struggling youth that wants to prove himself useful to his friends.";
	}

	public override void setAbilities()
	{
		abilities.AddRange(new List<string> {"Paralyze", "Poison"});
	}

	IEnumerator animate(){
		yield return new WaitForSeconds (Random.Range (0.1f, 0.9f));
		idle_state = 1;
		while (true) {

			yield return new WaitForSeconds (animation_speed);

			if (idle_state == 1) {
				rendy.sprite = idle2;
				idle_state = 2;
			} else if (idle_state == 2) {
				rendy.sprite = idle1;
				idle_state = 1;
			} else {
				idle_state = 2;
			}

		}
	}

	public override void attackAnimation(){
		idle_state = 3;
		rendy.sprite = attack;
	}

}