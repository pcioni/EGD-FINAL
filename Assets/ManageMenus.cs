using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageMenus : MonoBehaviour {
	//TOP LEVEL
	public GameObject main_menu_interface;
	public GameObject poof_interface;

	//POOF
	int mode = 2;
	public GameObject gamesList;
	public GameObject friendsList;

	//GAMES
	public int current_level;
	public GameObject play_button;
	string[] levelSceneNames = { "mmo.unity", "party.unity", "fps.unity", "survival.unity", "arena.unity", "mmo2.unity" };
	public string selectedLevel;
	public Sprite[] logoSprites;

	//FRIENDS
	public Sprite[] profileSprites;

	// Use this for initialization
	void Start () {
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
		foreach (VerticalLayoutGroup thing in f) print (thing.name);
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
		poof_interface.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
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
			SetFriendInfo("Cody");
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
			play_button.SetActive (false);
			break;
		}
		mode = m;
	}

	public void SetFriendInfo(string friend_name){
		string[] s = GetComponent<MenuMenuTextStorage> ().getFriendInfo (friend_name);
		SetCenterPanelText (s [0], s [1], s [2]);
		//selectedFriend = levelSceneNames[n];
	}

	public void SetGameInfo(int n){
		string[] s = GetComponent<MenuMenuTextStorage> ().getGameInfo (n);
		SetCenterPanelText (s [0], s [1], s [2]);
		selectedLevel = levelSceneNames[n];
		Sprite game_image = Resources.Load<Sprite> ("Title Placards/" + n.ToString());
		print (game_image);
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
		SceneManager.LoadScene ("Overworld Test");
	}
		
}
