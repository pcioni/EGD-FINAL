using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (scrollbar.value == 1) return;
			Vector3 pos = transform.position;
			transform.position = new Vector3 (pos.x, pos.y - 33, pos.z);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (scrollbar.value == 0) return;
			Vector3 pos = transform.position;
			transform.position = new Vector3 (pos.x, pos.y + 33, pos.z);
		}
	}

	public void AddButtons(string[] button_array){
		scrollbar.value = 1;
		GetComponentsInChildren<Button> () [0].Select ();

	}

	void AutoScrollButtons(int direction){
		
	}

}
