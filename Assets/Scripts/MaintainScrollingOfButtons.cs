using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class MaintainScrollingOfButtons : MonoBehaviour {
	EventSystem event_system;
	Scrollbar scrollbar;

	// Use this for initialization
	void Start () {
		event_system = EventSystem.current.GetComponent<EventSystem> ();
		scrollbar = GameObject.Find ("Button List Scrollbar").GetComponent<Scrollbar> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) 
			|| Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
			//set scrollbar value to Selected's percentage through the option list
			GameObject button = event_system.currentSelectedGameObject;
			if (button == null || button.GetComponent<Button>() == null) { 
				Button button_component = 
					GameObject.Find ("Button List Grid").GetComponentsInChildren<Button> () [0];
				button_component.Select ();
				button = button_component.gameObject;
			}
			GameObject button_parent = button.transform.parent.gameObject;
			Button[] button_array = button_parent.GetComponentsInChildren<Button> ();
			List<Button> button_list = button_array.ToList ();
			int index_of_button = 
				button_list.IndexOf (button.GetComponent<Button> ());
			float percentage_through_list = 
				(float) index_of_button / (button_list.Count-1);
			scrollbar.value = 1-percentage_through_list;
		}
	}

}

