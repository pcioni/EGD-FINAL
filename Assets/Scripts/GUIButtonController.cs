using UnityEngine;
using System.Collections;

public class GUIButtonController : MonoBehaviour {

    private bool displayInfo = false;
    private Vector3 displayInfoPos;

	// Use this for initialization
	void Awake () {
        Debug.Log(displayInfoPos);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        if (displayInfo) {
            displayInfoPos = Input.mousePosition;
            GUI.TextArea(new Rect(displayInfoPos.x, Screen.height - displayInfoPos.y, 200, 100), "DESCRIPTION", 1000);
        } 
    }


    public void MouseEnter() {
        displayInfo = true;
    }

    public void MouseExit() {
        displayInfo = false;
    }

}