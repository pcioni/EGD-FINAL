using UnityEngine;
using System.Collections;

public class MenuMenuTextStorage : MonoBehaviour {

	string[] game1 = {		
			//Game 1
			//Description
			"Only the bravest and boldest may play in the realm of " +
			"MMORPG, a knights and dragons fantasy game of epic " +
			"proportions!\nForm a guild with your friends and battle to " +
			"become MOST EPIC KNIGHT. For players 12 and up.",
			//Title
			"MMORPG",
			//Quick Facts
			"Spectacular graphics make this game a must try. -Reviewer Magazine"
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

	void Start(){
		gameInfo = new string[][]{ game1, game2, game3, game4, game5, game6 };
	}

	public string[] getGameInfo(int game_number){
		return gameInfo [game_number];
	}
}