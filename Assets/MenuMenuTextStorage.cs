using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuMenuTextStorage : MonoBehaviour {

	string[] game1 = {		
			//Game 1
			//Description
			"Adventure! Achievement! Triumph! Experience the " +
			"thrill of this medieval fantasy RPG of epic " +
			"proportions!\nJoin a guild with your friends and fight " +
			"hard to earn your glory!",
			//Title
			"Mamorpaga",
			//Quick Facts
		"The unexpected hit of 2006.\n★★★★★ - Game Review Daily"
	};

	string[] game2 = {	
			//Game 2
			//Description
			"Test your reaction time, aim, and determination in the grueling " +
			"world of Combat Zone, where every second counts. Use an array of " +
			"firearms and explosives to decimate the other team and prove " +
			"yourself the best of the best.",
			//Title
			"Combat Zone",
			//Quick Facts
			"I played this game and I liked it so you should like it. -Reviewer Magazine"
	};

	string[] game3 = {
			//Game 3
			//Description
			"Reimagined for 2013, Mamorpaga Remastered is nothing like you've ever seen before. " +
			"Get your guilds together, fair ladies and gallant gentleman, this is one quest you " +
			"won't want to miss.",
			//Title
			"Mamorpaga Remastered",
			//Quick Facts
			"Like fine wine, it's only improved with age. -Reviewer Magazine"
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