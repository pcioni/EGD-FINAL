using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuMenuTextStorage : MonoBehaviour {

	string[] game1 = {		
			//Game 1
			//Description
			"Only the bravest and boldest may play in the realm of " +
			"Mamorpaga, a knights and dragons fantasy game of epic " +
			"proportions!\nForm a guild with your friends and battle to " +
			"become MOST EPIC KNIGHT. For players 12 and up.",
			//Title
			"Mamorpaga",
			//Quick Facts
			"What a fantastic name. -Andrew, Reviewer Magazine\n\n10/10 -Kevin Kortright"
	};

	string[] game2 = {	
			//Game 2
			//Description
			"Everyday's a party with new Super Ultra Mega Neato Awesome Party! " +
			"Play this game to show your friends how good you are at " +
			"rather inconsequential activities!",
			//Title
			"Party Game",
			//Quick Facts
			"I played this game and I liked it so you should like it. -Reviewer Magazine"
	};

	string[] game3 = {
			//Game 3
			//Description
			"Only manly people who like explosions need apply.",
			//Title
			"FPS Game",
			//Quick Facts
			"360-No Scoping N00bs is my go-to maneuver. -Reviewer Magazine"
	};
 
	string[] game4 = {
			//Game 4
			//Description
			"Survive. Don't die. Live on.",
			//Title
			"Survival",
			//Quick Facts
			"Stayin' alive, stayin' alive. -Reviewer Magazine"
	};
	string[] game5 = {
			//Game 5
			//Description
			"Team-based shooter where your wins depend entirely " +
			"on if you can complement each other's skills and wear " +
			"the flashiest, most expensive cosmetics.",
			//Title
			"Team Arena",
			//Quick Facts
			"We're all in this together once we know who we are." +
			"We're all stars! And we see that. -Reviewer Magazine"
	};
	string[] game6 =
		{
			//Game 6
			//Description
			"Remastered for a new generation of gamers, this epic " +
			"fantasy quest has more punch than ever, and enough dragons " +
			"to fill at least a small hockey stadium.",
			//Title
			"MMORPG Remastered Edition",
			//Quick Facts
			"Better graphics mean the game is better, right? -Reviewer Magazine"
	};

	string[][] gameInfo; 


	//FRIENDS
	string[] amelia = {
		//Description
		"I like to be around people who push me to get better. I don't like " +
		"to be around people who annoy me or are sissies, so if you are one " +
		"of those things, clear out.",
		//Name
		"Amazing456 (Amelia)",
		//Quick Facts
		"Straight-A student. Some day I'll be your boss.\n\nNot afraid of sharks even a little."
	};

	//FRIENDS
	string[] cody = {
		//Description
		"I can't wait to make some friends on Poof! If you're reading this, " +
		"you've probably played a game with me since you found my profile. " +
		"Add me!!!",
		//Name
		"Cooldy95 (Cody)",
		//Quick Facts
		"Great at building sandcastles.\n\nI love rollerskating."
	};

	//FRIENDS
	string[] nico = {
		//Description
		"If you're reading this you're too late. I've hacked into your computer " +
		"and am appropriating your Poof Wallet funds to myself. Thanks for the " +
		"money. If you'd like, message me and I will share some of your money " +
		"to you.",
		//Name
		"Sneaky2000 (Nico)",
		//Quick Facts
		"I'm actually YOUR MOM!\n\nBrilliant pickpocket."
	};


	Dictionary<string, string[]> friend_dictionary = new Dictionary<string, string[]>();

	void Start(){
		gameInfo = new string[][]{ game1, game2, game3, game4, game5, game6 };
		friend_dictionary.Add ("Amelia", amelia);
		friend_dictionary.Add ("Cody", cody);
		friend_dictionary.Add ("Nico", nico);
	}

	public string[] getGameInfo(int game_number){
		return gameInfo [game_number];
	}

	public string[] getFriendInfo(string friend_name){
		return friend_dictionary [friend_name];
	}
}