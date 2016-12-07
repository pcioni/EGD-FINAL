using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageMenus : MonoBehaviour {
	//TOP LEVEL
	public GameObject main_menu_interface;
	public GameObject poof_interface;

	//POOF
	//int mode = 2;
	public GameObject gamesList;
	public GameObject friendsList;

	//GAMES
	int current_level;
	public GameObject play_button;
	string[] levelSceneNames = { "Level 1 Overworld.unity", "Level 2 Overworld.unity", "Level 3 Overworld.unity", "survival.unity", "arena.unity", "mmo2.unity" };
	string[] current_level_dates = { "June 28th, 2006", "Feb. 7th, 2009", "May 14th, 2013", "The Future" };
	public string selectedLevel;
	public Sprite[] logoSprites;

	//FRIENDS
	public Sprite[] profileSprites;

	// Use this for initialization
	void Start () {
		//get the level from Information
		current_level = GameObject.FindObjectOfType<Information>().GetLevelNumber();

		//enable buttons based on how far we are
		Button[] g= gamesList.GetComponentsInChildren<Button>();
		for (int x = 0; x < g.Length; x++) {
			if (x < current_level) {
				g [x].interactable = true;
			} else {
				g [x].interactable = false;
			}
		}
		//enable buttons based on how far we are
		VerticalLayoutGroup[] f= friendsList.GetComponentsInChildren<VerticalLayoutGroup>();
		for (int x = 0; x < f.Length; x++) {
			if (f [x].name == "Friends List") continue;
			if (x <= current_level) {
				foreach (Button buttn in f[x].gameObject.GetComponentsInChildren<Button>())
					buttn.interactable = true;
			} else {
				foreach (Button buttn in f[x].gameObject.GetComponentsInChildren<Button>())
					buttn.interactable = false;
			}
		}
		poof_interface.SetActive (true);
		SetMode (2);
		GameObject.Find ("Date").GetComponent<Text> ().text = current_level_dates [current_level - 1];
		poof_interface.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (poof_interface.activeInHierarchy)
			UpdateTime ();
	}

	public void PoofOnPoofOff(){
		if (poof_interface.activeInHierarchy) {
			poof_interface.SetActive (false);
			main_menu_interface.SetActive (true);
		} 
		else {
			poof_interface.SetActive (true);
			SetMode (2);
			GameObject.Find ("About You Button").GetComponent<Button>().Select();
			main_menu_interface.SetActive (false);
		}
	}

	public void SetMode(int m){
		//enable the correct side panel
		switch (m) {
		case 0: //Friends
			gamesList.SetActive (false);
			friendsList.SetActive (true);
			break;
		case 1: //Games
			gamesList.SetActive (true);
			friendsList.SetActive (false);
			break;
		case 2: //About You
			gamesList.SetActive (false);
			friendsList.SetActive (false);
			break;
		}
		//enable the correct middle panel
		switch (m) {
		case 0: //Friends
			if (current_level > 1)
				SetFriendInfo ("Cody");
			else {
				SetCenterPanelText ("","No friends to show","");
				GameObject.Find ("Logo").GetComponent<Image> ().sprite = null;
			}
			play_button.SetActive (false);
			break;
		case 1: // Games
			SetGameInfo(0);
			play_button.SetActive (true);
			break;
		case 2: //About You
			SetCenterPanelText ("I like adventure and action games best, though " +
			"party games are growing on me. If I got to choose one super " +
			"power it would be the power to fly.",
				"DragonBlaster40 (Sam)",
				"Fantastic dancer.");
			GameObject.Find ("Logo").GetComponent<Image> ().sprite = 
				Resources.Load<Sprite> ("Character Headshots/sam");
			play_button.SetActive (false);
			break;
		}
		//mode = m;
	}

	public void SetFriendInfo(string friend_name){
		string[] s = GetComponent<MenuMenuTextStorage> ().getFriendInfo (friend_name);
		SetCenterPanelText (s [0], s [1], s [2]);
		Sprite friend_image = Resources.Load<Sprite> ("Character Headshots/" + friend_name.ToString());
		if (friend_image != null) {
			GameObject.Find ("Logo").GetComponent<Image> ().sprite = friend_image;
		} else {
			GameObject.Find ("Logo").GetComponent<Image> ().sprite = null;
		}
		//selectedFriend = levelSceneNames[n];
	}

	public void SetGameInfo(int n){
		string[] s = GetComponent<MenuMenuTextStorage> ().getGameInfo (n);
		SetCenterPanelText (s [0], s [1], s [2]);
		selectedLevel = levelSceneNames[n];
		Sprite game_image = Resources.Load<Sprite> ("Title Placards/" + n.ToString());
		//print (game_image);
		if (game_image != null) {
			GameObject.Find ("Logo").GetComponent<Image> ().sprite = game_image;
		} else {
			GameObject.Find ("Logo").GetComponent<Image> ().sprite = null;
		}
		//TODO: Set play_button's onClick here
	}

	public void SetCenterPanelText(string descr, string title, string quick_facts){
		GameObject.Find ("Description Text").GetComponent<Text> ().text = descr;
		GameObject.Find ("Game Title Text").GetComponent<Text> ().text = title;
		GameObject.Find ("Quick Facts").GetComponent<Text> ().text = quick_facts;
	}

	public void OpenLevelScene(){
		SceneManager.LoadScene ("Level "+ selectedLevel.Split(' ')[1] +" Overworld");
	}

	void UpdateTime(){
		int hour = System.DateTime.Now.Hour;
		string am_pm = "";
		if (hour >= 12) {
			if (hour != 12) hour -= 12;
			am_pm = "pm";
		} else {
			if (hour == 0)hour = 12;
			am_pm = "am";
		}
		string minute = System.DateTime.Now.Minute.ToString ();
		if (minute.Length < 2)
			minute = "0" + minute;
		string current_time = hour +":"+ minute + am_pm;
		GameObject.Find ("Time").GetComponent<Text> ().text = current_time;
	}
		
}
