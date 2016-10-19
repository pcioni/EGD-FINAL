using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KevinLogic : MonoBehaviour {
	public bool enableSinging = false;
	//okay, Kevin Logic jokes aside
	public GameObject choiceText;
	public GameObject longText;

	// Use this for initialization
	void Start () {
		choiceText = GameObject.Find ("UI Choice Text");
		//longText = GameObject.Find ("UI Long Text");
		if (longText == null)
			print ("did not find long text");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReceiveButtonSignal(string button){
		switch (button) 
		{
		case "1":
			choiceText.GetComponent<typeMessage> ().SetMessage ("Manticore took 40 damage!_~Who should Sam attack now?");
			break;
		case "2":
			choiceText.GetComponent<typeMessage> ().SetMessage ("Slime took 35 damage!_~Who should Sam attack now?");
			break;
		case "3":
			//choiceText.GetComponent<typeMessage> ().SetMessage ("No, why don't you take another shot at it?");
			break;
		case "4":
			//choiceText.GetComponent<typeMessage> ().SetMessage ("......_#You seem to be having quite a bit of trouble with this...");
			break;
		}
	}
}
