using UnityEngine;
using System.Collections;

public class CharacterBehavior : MonoBehaviour {
	public int health = 100;
	GameObject myHealthBar;
	public GameObject healthbar_prefab;

	// Use this for initialization
	void Start () {
		GameObject bar = Instantiate (healthbar_prefab);
		bar.transform.parent = GameObject.Find ("Floating Character Canvas").transform;
		bar.transform.localScale = Vector3.one;
		myHealthBar = bar;
		print (health);
	}
	
	// Update is called once per frame
	void Update () {
		//calculate the distance the healthbar sits as a function of how
		//tall our character is
		float half_height = GetComponent<SpriteRenderer>().bounds.extents.y;
		float height_above = .1f * half_height + half_height;
		myHealthBar.transform.position = new Vector3(transform.position.x,
			transform.position.y + height_above, 0);

		//TEST
		if (Input.GetKeyDown (KeyCode.P) && name == "Sam") {
			TakeDamage (10);
		}
	}

	/// <summary>
	/// Causes the character to take damage and 
	/// update his UI healthbar to reflect it.
	/// </summary>
	/// <param name="amount">Must be float between 0 and 1.</param>
	public void TakeDamage(int amount){
		print ("Subtracting " + amount + " from " + health);
		health -= amount;
		myHealthBar.GetComponent<HealthbarBehavior> ().SetHealth (health);
		print (health);
	}
}
