using UnityEngine;
using System.Collections;

public class IdentifyFirstScene : MonoBehaviour {
	public bool firstScene = false;
	public string helpfulInstructions = "Click to open the script and read these." +
	                                     "When making a new overworld, it's easiest to copy a whole screen like" +
	                                     "Marketplace in as a template and move around whatever you need. Mark" +
	                                     "ONE screen block as firstScene." +
	                                     "Replacing Sprites on interactables and the background" +
	                                     "is the easiest way to make new ones." +
	                                     "Link up the doors- each door has a public reference to its" +
	                                     "destination. Make sure they all point where you want them to." +
	                                     "Place the main character in the first scene wherever you want him.";

	// Use this for initialization
	void Start () {
		if (firstScene) {
			foreach (Camera c in GameObject.FindObjectsOfType<Camera>()) {
				if (c.transform.parent.gameObject.GetComponent<IdentifyFirstScene>().firstScene){
					print ("ERROR: there can be only ONE first scene!!\n" +
						"At least "+name+" and "+c.transform.parent.gameObject.name+" have been designated as first_scene");
				}
				if (c.enabled == true && c.gameObject.name != GetComponentInChildren<Camera>().gameObject.name) {
					//if someone's camera other than mine is enabled, 
					//BELITTLE THEM FOR THEIR INSOLENCE!!
					c.enabled = false;
					//there can be only ONE first scene.
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
