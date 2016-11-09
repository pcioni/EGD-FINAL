using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class AddScrollingButtonsToUI : MonoBehaviour {
	EventSystem event_system;
	Scrollbar scrollbar;

	// Use this for initialization
	void Start () {
		event_system = EventSystem.current.GetComponent<EventSystem> ();
		scrollbar = GameObject.Find ("Button List Scrollbar").GetComponent<Scrollbar> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) {
			//set scrollbar value to Selected's percentage through the option list
			GameObject button = event_system.currentSelectedGameObject;
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

	public void AddButtons(string[] button_array){
		scrollbar.value = 1;
		GetComponentsInChildren<Button> () [0].Select ();

	}

	void AutoScrollButtons(int direction){
		
	}

}
