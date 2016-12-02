using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A giant storage system for all the text in the game.
/// </summary>
public class OverworldTextStorage : MonoBehaviour {
	public int level_number;
	Dictionary<string, string[]> dialogue;
	Dictionary<string, string[]> level1;
	Dictionary<string, string[]> level2;
	Dictionary<string, string[]> level3;
	Dictionary<string, string[]> epilogue;

	// Use this for initialization
	void Awake () {
		//Initialize
		dialogue = new Dictionary<string, string[]> ();
		level1 = new Dictionary<string, string[]> ();
		level2 = new Dictionary<string, string[]> ();
		level3 = new Dictionary<string, string[]> ();
		epilogue = new Dictionary<string, string[]> ();

		//Set the correct one
		switch (level_number) {
		case 1:
			dialogue = level1;
			break;
		case 2:
			dialogue = level2;
			break;
		case 3:
			dialogue = level3;
			break;
		case 5:
			dialogue = epilogue;
			break;
		}

		//LEVEL 1
		level1.Add("Sign", new string[]{
			"SIGN|Welcome to Mamorpaga!",
			"SIGN|Press Spacebar to read signs."
		});

		level1.Add("Marketplace Amelia", new string[]{
			"SAM|Hey.",
			"AMELIA|Busy right now.",
			"SAM|Then why are you playing the game if you're busy?",
			"AMELIA|I'm busy PLAYING the game. I have to figure out " +
			"which locations give the most loot fastest.",
			"SAM|Can I join up with you?",
			"AMELIA|You'd just slow me down.",
			"SAM|But it would be more fun together.",
			"AMELIA|_Sorry, were you saying something?",
			"AMELIA| Ah, nope, I'm here to play the game, not make friends."
		});

		level1.Add("Marketplace Thug", new string[]{
			"SAM|Hey, I'm new, can you give me any advice?",
			"THUG 1|New, huh?",
			"SAM|You better believe it!",
			"THUG 1|I'm going to wipe the floor with you!",
			"SAM|What???",
			"THUG 1|Brace yourself noob, here comes a battle!"
		});

		level1.Add("Treasure Chest 1", new string[]{
			"CHEST 1|Wow! You found a solid chunk of gold!",
			"SAM|Wow!",
			"CHEST 1|It's solid!",
			"SAM|Wow!",
			"CHEST 1|It's gold!",
			"SAM|Really??!",
			"CHEST 1|Nope.",
			"|You got the Potion."
		});

		level1.Add ("Darkness Thug", new string[] {
			"Sam| Hi, I'm a new player! Can you give me any advice?",
			"THUG LEADER| Hahaha! U are funny. I don't give advice to n00bs!! ",
			"Sam| What! Hey, that's mean- you can't just go saying that on the internet!!",
			"THUG LEADER| U gonna do something ABOUT it??",
			"Sam| YEAH! _ (What can I do about it?!) ~ Oh yeah, you're in for it now!",
			"THUG LEADER| U gonna fight me??",
			"Sam| YEAH! (Holy crap, NO!!)",
			"THUG LEADER| Fine, your guild, my guild, Eastern Caves in an hour.",
			"Sam| YEAH!!! (Aaaaaah!!)",
			"THUG LEADER|Actually..._I've always been bad at waiting. How about a preview?", 
			"THUG LEADER|GET HIM!$",
			"THUG LEADER| I hope you're ready to die, kid.",
			"Sam| So ready! Or - rather - ready to watch YOU die! I mean, not in real life, that would be weird if I was at your death bed - but like",
			"THUG LEADER| ...$",
			"THUG LEADER| Why do you keep talking to me.",
			"Sam| I want to see how many different things you'll say.$",
			"THUG LEADER| Why do you keep talking to me.",
			"Sam| Well, that answers that question."
		});

		level1.Add ("Marketplace Cody", new string[] {
			"Cody| Hey, there was a guy in a bandana here before. He took my prized " +
			"[item that Thug Leader will have in battle]. ",
			"Sam| What?? I know that guy! He's a total jerk! ",
			"Cody| Right? He told me he could give me a [rare item] if I just gave him something from " +
			"my inventory.",
			"Sam| Wow, what a great deal!",
			"Cody| Right?? #But then when I gave him the thing, he took it and left.",
			"Sam| We're going after him. ",
			"Cody| We are?",
			"Sam| Yeah! You and me, we're in a guild! The Fireclomplers!",
			"Cody| We ARE?",
			"Sam| And we're gonna go to his base, and kick his butt, and the butts of all the people in his guild!",
			"Cody| We ARE??",
			"Sam| And then we're gonna take all their loot.",
			"Cody| We _ ARE??",
			"Sam| Yes.",
			"Cody| Well alright then!",
			"|Cody joined your party."
		});

		level1.Add ("Great Outdoors Thug", new string[] {
			"Thug 1| Hey it's you again!",
			"Cody| Oh, hey there..",
			"Thug 1| You're the kid we wrecked earlier! Hahaha!",
			"Sam| That's funny, I was going to say the same about you.",
			"Thug 1| That is BESIDS the POINT!!",
			"Sam| How about you have a rematch with both of us?",
			"Cody| (Dude, how are you standing up to this guy? You're amazing!)",
			"Sam| (I don't know, I just feel more confident with someone here to watch my back, I guess)",
			"Thug 1| That's it! You kids are gnna be so sorry!!"
		});

		level1.Add ("Treasure Chest 2", new string[] {
			"CHEST 1|Oh boy! Guess what you got!",
			"SAM|Oh please not this again",
			"CHEST 1|Go on, guess.",
			"SAM|GOLD?!?!?",
			"CHEST 1|WRONG! It's your very own [disappointing item]!",
			"|You got a used [disappointing item]."
		});

		level1.Add ("Marketplace Nico", new string[] {
			"Sam| Hey, would you like to join our guild?",
			"Nico| Nope.",
			"Sam| What why not? We just beat a guy up!",
			"Nico| Oh so just because you are violent and unhinged, I should follow you blindly?",
			"Sam| What, no, that's not what I meant!",
			"Nico| Out with you, you demagogue!",
			"Sam| What's that?",
			"Nico| Help! He's getting violent! AAAGH!",
			"Sam| You know what, you're not funny.$",

			"Nico| Hello, are you here for Alchemist's Anonymous?",
			"Sam| What? Weren't you just making a bunch of noise to end our last conversation?",
			"Nico| That was the old me. The new me has abandoned the sins and vices of his past.",
			"Sam| Alchemist's Anonymous?",
			"Nico| Just because you CAN transmute it, doesn't mean you should.",
			"Sam| Transmute?",
			"Nico| I wish I could transMUTE your face! AHAHA!$",

			"Nico| Beautiful dreamer, wake unto me! Starlight and dew drops are waiting for thee!",
			"Sam| What are you doing?",
			"Nico| I'm singing.",
			"Sam| No you're not, you're just typing.",
			"Nico| YOU CAN'T HEAR ME! HOW DO YOU KNOW! YOU DON'T KNOW MY LIFE!",
			"Sam| ...You suck, man.$",

			"Nico| No, no, stay out of here- people will think I'm lame like you if you stand too close."
		});

		level1.Add ("Great Outdoors Amelia", new string[] {
			"Sam| Hey",
			"Amelia| Hey! You're just in time!",
			"Cody| (Girl)",
			"Sam| (Yes I see that)#For what?",
			"Amelia| For monster hunting!",
			"Sam| Isn't it unwise to just go out into the wilderness and pick a fight with something?",
			"Cody| (Girl)",
			"Sam| (Yes I noticed.)",
			"Amelia| Nope! Are you guys in?",
			"Sam| I guess so.",
			"Cody| Girl.......~Imeanyes",
			"Amelia| Umm.",
			"Sam| He talks in slang. Like 'Girl, you rockin' that sweater so fly.'",
			"Cody| Girl, you._._. Girl.",
			"Amelia| ..._Well okay then. Here come some monsters now!",
			"|Amelia joined your party",
			"Amelia| Here goes nothing!"
		});

		level1.Add ("Treasure Chest 3", new string[] {
			"CHEST 1|Oh my! You won’t believe what you just found!",
			"SAM|Please don’t break my heart again",
			"CHEST 1|Your life will be forever changed...",
			"SAM| gold ?",
			"CHEST 1|...just not by this item.",
			"|You got the [disappointing item]"
		});

		level1.Add ("Cave Entrance Nico", new string[] {
			"Nico| I am here to join your party. Well, it wasn't a party without me, really.",
			"|Nico joined your party",
			"Sam| What no, you've been a jerk to me this whole time, no way.",
			"|Nico was removed from your party",
			"Nico| But you're going to need me! Isn't it magnanimous of me to join you?",
			"Amelia| Scram, Nico.",
			"Nico| Amelia, stay out of it. Sam can choose who he wants on his team.",
			"Sam| Sorry dude, you seem like a nice, cool guy...",
			"Cody| Eh..",
			"Sam| You seem like a cool guy...",
			"Cody| ...Eh...",
			"Sam| You seem like a guy, but I don't think this team is for you. We're just looking for someone with a different...",
			"Amelia| Background.",
			"Sam| A different background than you have, yeah!",
			"Nico| You'll miss me.",
			"Cody| Eh.."
		});

		level1.Add ("Enemy Camp Thug Leader", new string[] {
			"Thug Leader| Wow, I didn't think you were gonna show!",
			"Sam| You wish! You weren't very nice to Cody and I think you owe him an apology!",
			"Thug Leader| Wow, that was so forceful, I feel like I have to.",
			"Sam| ..._Well?",
			"Thug Leader| The feeling passed.#So this is ur amazing group. " +
			"This is all the trash u could find and hold on to. Hahaa.",
			"Thug Leader| Are u ready to get the crap beat out of u, noob?"
		});

		level1.Add ("Group Final", new string[] {
			"Amelia|Hey Sam, this was a lot of fun. I actually really like this guild.",
			"Nico|Yeah, can we keep on with this?",
			"Sam|Nico where did you even come from?",
			"Cody|Thanks for bringing us together Sam. Let's meet up again soon!",
			"Poof|You recieved a friend request from Cooldy456."
		});

		//EPILOGUE
		epilogue.Add ("epilogue", new string[] {
			//"EPILOGUE",
			"Sam Walker finally found his passion for academics, attending a " +
			"prestigious New York college to study Computer Science.",
			"Amelia Ramirez went on to study Chemical Engineering at the same " +
			"school as Sam._# They started dating in September of 2013.",
			"Lacking the financial means to pursue higher education, Cody Baron " +
			"entered the workforce as a sales clerk after high school graduation, " +
			"_but an essay contest led to his obtaining a full scholarship to study " +
			"Marine Biology at his dream school in Florida.",
			"Nico Da Silva tired of education, and left college after his first " +
			"semester._ He joined an improvisational comedy troupe in Los Angeles " +
			"and was called a rising star by three LA newspapers within two months " +
			"of his stage debut.",
			"Sam Walker was fond of recounting stories of his adventures with the " +
			"Fireclomplers, and he never forgot his happy days playing games with his " +
			"guild.",
			"CREDITS: #Programming by #Kevin Kortright #Daniel Haynes #and #Phil Cioni ",
			"Art by #Yijia Chen ##Music and Sound by #Mitchell Krueger ",
			"THE END##On behalf of our entire team, thank you for playing."

		});
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string[] RetrieveDialogue(string id){
		return dialogue [id];
	}
}
