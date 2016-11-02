using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class startHighlighted : MonoBehaviour {

	public bool start_highlighted = true;
	EventSystem es;

	// Use this for initialization
	void Start () {
		if (start_highlighted) {
			GetComponent<Button> ().Select ();
		}
		es = EventSystem.current.GetComponent<EventSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (es.currentSelectedGameObject == null) {
			if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.A) ||
			    Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.D) ||
			    Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow) ||
			    Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.RightArrow)) {
				GameObject.Find ("Choice Button 1").GetComponent<Button> ().Select ();
			}
		}
	}

	public void MouseExit(){
		EventSystem.current.GetComponent<EventSystem> ().SetSelectedGameObject (null);
		GameObject.Find ("EventSystem").GetComponent<EventSystem> ().SetSelectedGameObject (null);
	}
}
