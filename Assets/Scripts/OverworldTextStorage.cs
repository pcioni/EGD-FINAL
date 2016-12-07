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
			"SIGN|Press Spacebar to read signs.",
			"SIGN|Press Shift to skip the typing animation on messages that are " +
			"just so long that you don't have the patience to sit there and wait " +
			"and wait and wait and wait and wait and WAIT and wait."
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
			"AMELIA| Ah, nope, I'm here to play the game, not make friends.$",
			"AMELIA| Why don't you go distract that person over there who " +
			"isn't me."
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
			"|You got the Potion.$",
			"|Enjoy."
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
			"Sam| Let's go train! I heard there's some wilderness off to our left.",
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
			"|You got a used [disappointing item].$",
			"|Enjoy."
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
			"|You got the [disappointing item]$",
			"|Enjoy"
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
			"Cody| Eh..$",
			"Nico| You're missing out on all this awesome right here. " +
			"What a shame. I feel bad for you."
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




		//LEVEL 2
		level2.Add ("Amelia 1", new string[] {
			"Amelia| We've got a lot of points to take if we're going to win this.",
			"Sam| Well hello to you too. How was your week?",
			"Amelia| Points Alpha, Beta, and Charlie have to be taken and held until the end of the round for our team to win. I am not in the mood to lose to any team that calls themselves the Leisurely Chillers.",
			"Sam| Well, maybe they'll have a very relaxed capture style.",
			"Amelia| Don't you have anything useful to be doing, nerd?"
		});

        level2.Add("Treasure Chest 1", new string[] {
            "Chest| Private you come here looking for a reward when you haven't even done anything??",
            "Chest| Civilian life has made you soft and needy!",
            "Chest| Am I expected to reward this behavior?",
            "Chest| Am I expected to condone your whining and sniveling?",
            "Chest| Do you want a participation trophy, private?",
            "Chest| Will that make you happy?",
            "|The chest actually does contain a Participation Trophy. You are led to believe that the chest was just antagonizing you to build character.$",
			"|You are led to believe that the chest was just antagonizing you to build character."
        });

        level2.Add("Nico 1", new string[] {
            "Nico| I've got enough grenades in my pocket to cause quite a stir. I think I'm ready to go. How's your week going Sam?",
            "Sam| Not too bad. Lots of homework from World History- I don't see who's supposed to care about that stuff.",
            "Nico| I like World History- lots of great role models. Gandhi, Margaret Thatcher, Robert Oppenheimer, Maximilien Robespierre...",
        });


        level2.Add("Cody 1", new string[] {
            "Cody| Did you talk to the girl from your history class today?",
            "Sam| Chickened out... Not everyone can land a great relationship like you, dude.",
            "Cody| You'll meet someone, I know it! And you haven't even talked to my girlfriend yet, how do you know you're going to like her?",
            "Sam| If you like her, she must be great.",
            "Amelia| Cody, is that girl here yet? She makes me regret more and more that we let her be the fifth soldier on the team.",
            "Cody| You won't regret it - she fights like a wildcat when she's in battle. It's so cool...",
            "Amelia| Do not get mushy when you are talking to me. I could care less about your feelings about this slow, silly girl of yours.$",
			"Cody|Boy oh boy, she's almost here."
        });

        level2.Add("Amanda 2", new string[] {
            "Amanda| hey everybody, sorry i'm late!",
            "Amelia| (Ew, does she only type in lowercase letters??)",
            "Sam| It's good to meet you Amanda! Are you ready to win some territory and some glory??",
            "Amanda| so ready!!!",
            "Cody| That's my Mandy!",
            "Amelia| (So gross! So gross!)",
        });

        level2.Add("Cody 2", new string[] {
            "|Cody is too busy sending private messages to Amanda to talk to you."
        });

        level2.Add("Nico 2", new string[] {
            "Nico| This is a good turn of events if you ask me!"
        });

        level2.Add("Amelia 2", new string[] {
            "Amelia| I looked at her stats, I don't think she's competent enough for this team.",
            "Sam| She's like twice my level in this game- she's only 300 EXP from unlocking some epic weapon.",
            "Amelia| I don't think she's qualified...",
            "Nico| Well did you notice her profile picture? She's got all the qualifications I need to see!",
            "Amelia| You're a pig, Nico!",
        });

        level2.Add("Enemy Soldier 3", new string[] {
            "Amelia| Buzz off dude, this is our point now!",
            "Sam|   (Hey, can't I handle this? I thought I was team leader!)",
            "Amelia| (Fine, do it)",
            "Sam| ..._ Oh forget it, let's fight."
        });

        level2.Add("Amanda 4", new string[] {
            "Amanda| well let's set up shop.",
            "Cody| Yes, let's.",
            "Nico| What do you mean, gorgeous?",
            "Cody| She wants to guard this point. Seems like a good idea.",
            "Nico| The gorgeous comment was not meant for you Cody.",
            "Cody| Darn...",
            "Amanda| hey, you're both going to make me jealous.",
            "Amelia| What a stupid idea, no!",
            "Cody| But I am gorgeous!",
            "Amelia| No, we have to capture the rest of the points, you idiots! Come on!",
            "Sam| Guys! If we keep arguing here the other team is going to come back!",
            "Nico| You mean that team right there?$",
            "Amanda| see! if we hadn't been here we would have lost the point. glad we've all seen reason now.",
            "Amelia| Reason to knock your teeth out. We would have captured the other points by now!",
            "Cody| Amelia, you need to give Amanda a say.",
            "Sam| Guys, everybody just needs to listen to each other's arguments and we can decide from there.",
            "Nico| This seems like a socially critical moment. I'm going to go ahead and agree with Amanda for the sole reason that she's hot.",
            "Amelia| YOU'RE ALL RIDICULOUS!!! I'll go capture Charlie BY MYSELF!",
            "Sam| Agh, the Fellowship is splitting! What to do?!_ I'm coming with you Amelia!"
        });

        level2.Add("Treasure Chest 5", new string[] {
            "Chest| Always with your hand out, aren't you boy?",
            "Chest| Always looking for some reward. Back in my day, we didn't HAVE rewards. We had more WORK. And we were happy for it!",
            "Chest| You're still here huh? What do you think I am, some sort of reward dispensing machine?",
            "Chest| Well I am, but that doesn't justify anything!",
            "|The chest contains a [decent item]. You are given the impression that the chest is only hard on you because it wants you to be strong enough to deal with life's challenges.$",
			"You are given the impression that the chest is only hard on you because it wants you to be strong enough to deal with life's challenges."
        });

        level2.Add("Enemy Soldier 5", new string[] { 
            "Enemy Soldier| This is our land dude. The two of you don't have a chance against the three of us.",
            "Amelia| Over your dead body, huh?",
            "Enemy Soldier| I didn't say that, no..",
            "Amelia| That's what I'm hearing!",
        });

        level2.Add("Amelia 6", new string[] {
            "Amelia| Phew, I needed that.",
            "Sam| Amelia that was brutal. I think I heard one of those guys crying. You couldn't have given them even a little break in that one?",
            "Amelia| Are you here to make friends or are you here to win?",
			"Sam| I'm here with my friends. They're my team. And right now we need teamwork. If you're here to win, you need to work as a team as well.",
            "Amelia| I get your point. You're right. There's no need to have everybody so mad at each other over a silly thing like where we ought to go. _#Thanks for being a good friend, Sam. You keep me steady when I'm losing my cool.",
            "Sam| You're always cool.",
            "Amelia| You too, nerd. _Hey I wonder what the rest of them have gotten up to?",
        });

        level2.Add("Amanda 7", new string[] {
            "Amanda| They had a tank, what were we supposed to do?"
        });

        level2.Add("Amelia 7", new string[] {
            "Amelia| I can't believe you guys! We HAD that rally point and you let it go!",
            "Nico| To a tank!! Do you know what those things are named after? Sherman, the guy who burned down half of the American South because he wanted to see the ocean!",
            "Sam| I'm pretty sure that's inaccurate...",
            "Amelia| If we're going to win this, we've got to stick together and move FAST. Not like you guys have been doing.",
            "Sam| Amelia, stop being critical. Amanda, are you ready to try Amelia's way?",
            "Amanda| yeah I guess so.",
            "Sam| Good. Is everyone okay with this?",
            "Cody| Yeah!",
            "Amelia| Let's do it!",
            "Nico| You know I can't help but noticing..",
            "Sam| Please don't notice anything.",
            "Nico| That if you look at it a certain way..",
            "Sam| Don't look at it. Just be blind. No looking.",
            "Nico| Amanda's plan would have worked if Amy hadn't stormed off like she did.",
            "Sam| Nico why???",
            "Amelia| Amy? _AMY??",
            "Sam| He's just trying to get you riled up. He's an agent of chaos, like Loki, or the old ladies who cross the road just to stop traffic.",
            "Nico| I bet that kind of power gets addictive.",
            "Cody| Tank outside!!!",
        });
        level2.Add("Amelia 8", new string[] {
            "Cody| Amazing as usual Mandy!",
            "Nico| She's okay, Cody. She's pretty okay.",
            "Amelia| I'm leaving, Sam. And you're not coming with me.",
            "Sam| Amelia, please don't leave.",
            "Amelia| I'll capture the points myself and that will be that.",
            "Sam| Wait --$",
			"Sam| Just stay here, we'll figure it out."
        });
        level2.Add("Amanda 8", new string[] {
            "Amanda| why don't we split up and tackle two points at once. now that the tank is gone, we can totally take on anything they try to do.",
            "Sam| Yeah, yeah, I think that should work, but what about Am-",
            "Amanda| great!! cody and i will take point charlie. see you in a jiffy!",
        });
        level2.Add("Treasure Chest 9", new string[] {
            "Chest| Look who's back, sniveling like a little worm.",
            "Sam| Worms don't snivel.",
            "Chest| I gave you the best training I could, Private, but this insubordination, the fact that you have the nerve to come back here and ask me to bail you out once again...",
            "Sam| Bail me out?? You're a treasure chest! You give items! That's what you do!",
            "Chest| ...",
            "Sam| You know what? Nico, put a grenade in this treasure chest and shut the lid.",
            "Chest| That's just inhumane, Private. But that's the way war is. It'll harden you. Take away your humanity. Make you-",
            "Sam| Shut it. _..._ Okay, let's see what's inside.",
            "|You found a grenade!",
            "Sam| Oh come on.",
            "|And a [decent item]$",
			"|You think you see a glint of pride in the chest's locking mechanism."
        });

        level2.Add("Nico 9", new string[] {
            "Sam| Nico, why do you keep antagonizing Amelia like that. You know you upset her.",
            "Nico| I did? ..._ I guess I did take it too far. But it was funny!",
            "Sam| Do you really think that?",
            "Nico| Not really. Not anymore.",
            "Sam| Some mess we've got ourselves in now.",
            "Nico| You know...",
            "Nico| I really feel bad now.",
            "Sam| Well, why'd you do it in the first place?",
            "Nico| You're asking me to get introspective?",
            "Sam| Yeah, whatever that word means, that's what I am going for, yeah.",
            "Nico| Well, I feel like I've always been a-",
            "Sam| Squad!",
            "Nico| A squad, yes. A squad of 1, if you will. And it's worked out for a while, it did, but you can only get so far before you have to-",
            "Sam| Turn around!",
            "Nico| Exactly. Please don't tell anyone this. The thing I've been unwilling to confess until now is that$",
			"Nico| Well that was divertive."
        });

        level2.Add("Cody 10", new string[] {
            "Sam| How'd it go over here?",
            "Cody| The enemies were all dead when we got here. It's given us some quality time to stare lovingly at each other's avatars.",
            "Cody| Hehehe. Liking you is Amanda-tory.",
            "Nico| Okay yeah that's nauseating stop.",
        });

        level2.Add("Amelia 11", new string[] {
            "Amelia| You were right Nico.",
            "Nico| Aw yeah! General Sherman really did want to see the ocean!",
            "Amelia| No, you were right that I should check out Amanda's profile picture.",
            "Cody| Is there something wrong?",
            "Amelia| Yup. See, I thought her profile picture looked a little familiar. Have you ever seen the commercial where the girl and the guy are at the beach and the guy gets stolen away by a mermaid? It's a Fried Right Potato Chip ad.",
            "Nico| Ooh yeah, let me sing the ditty from it!",
            "Amelia| No",
            "Nico| Fried Right, Fried Right, eat too many and your clothes get tight!",
            "Sam| That does not seem like a good advertising strategy.",
            "Amelia| Amanda is not a girl. She's a guy. Pretending to be a girl.",
            "Nico| Wait what",
            "Amelia| Her profile picture bears a stunning resemblance to the mermaid from the potato chip ad.",
            "Sam| But that doesn't mean it isn't her in the picture.",
            "Amelia| HE, whoever he is, failed to crop the Fried Right logo out of the bottom right of the picture. There's still a little of it there.",
            "Amanda| HA! That's so wrong!",
            "Cody| Wait. Some of this actually makes sense.",
            "Amanda| You can't be serious",
            "Cody| The fact that you never talk about your personal life, the fact that you never talk over the microphone, the fact that your battle cry is “Let us fight like men because we are all indeed men!”",
            "Amanda| Wow, I forgot about that one.",
            "Cody| You lied to me! You led me on for two months!",
            "Amanda| No! It's not true!!",
            "Nico| Holy crap, the Potato Chip Mermaid really screwed Cody over.",
        });

        level2.Add("Treasure Chest 12", new string[] {
            "Chest| You thought your threats were clever, did you Private? Did you? It's time you learned some respect. I am armed with a series of explosive devices that will go off when I am opened. Try me. Just try me!",
            "Sam| Nah.",
            "|You put the entire Treasure Chest in your inventory.",
        });

        level2.Add("Cody 12", new string[] {
            "Sam| Are you alright?",
            "Cody| just hda my hraet brokn nope.",
            "Nico| He's been crying his eyes out since the Charlie incident. His keyboard is fritzing out from the mixture of tears and wet hands",
            "Sam| Gosh...",
            "Nico| See it's weird because his avatar is still smiling, even though I can expressly hear sobs coming from him over the microphone. Kinda creepy.",
            "Sam| Not helpful.",
            "Nico| It kills me to see him hurting like this. Much more effective than the plague.",
        });

        level2.Add("Amelia 12", new string[] {
            "Amelia| Come on Cody, we don't have time for this! We have to take Rally Point Alpha before someone picks up one of our other points!",
            "Sam| Seriously?! What are you, a robot? Heartless?",
            "Amelia| What?",
            "Sam| You're insensitive, and harsh. He's heartbroken!",
            "Nico| I'm with Sam on this one. It may be a videogame, but Cody had real feelings here. And you have no respect for that.",
            "Amelia| I...",
            "Nico| We should chase Amanda down. If nothing else, we can give Cody a steaming plate of revenge for his troubles.",
            "Sam| There's an apology in order here. Two actually. Amelia, you start.",
            "Amelia| Cody.. I'm sorry. I'm sorry that your girlfriend was a catfish, I'm sorry that I wasn't more sensitive... I'm sorry that I put the game before you.",
            "Cody| thank you",
            "Amelia| Let's do this thing.",
            "Cody| I can't face her. I can't. I'll just stay here.",
            "Sam| We won't be far. Just come find us when you're ready."
        });

        level2.Add("Amanda 13", new string[] {
            "Amanda| You know, I feel like we've grown a lot today.",
            "Sam| Why don't you cut the crap. You need to come back to base with us and apologize to Cody for what you did.",
            "Amanda| Oh Samuel, I think you're mistaken if you think I need to do anything.",
            "Amanda| I finally got that last bit of EXP. I bet you've never even seen a super weapon in this game.",
            "Sam| What don't you understand! You are a lowdown, tricking snake!",
            "Amanda| With a gun so big I just obliterated the entire other team with it. All of them, Sam-ilitude.",
            "Sam| I'm going to wipe that smile right off your face.",
        });

        level2.Add("Amanda 14", new string[] {
            "Amanda| You don't understand me! When you play as a guy, everybody treats you like you aren't there, like you don't matter. I play as a girl and then all of a sudden everybody's like, “Oh Mandy you're so pretty. Oh you're so special.” NO ONE EVER TREATS ME LIKE THAT!",
            "Sam| What you did was wrong. There's no justifying it.",
            "Amanda| Well why don't you try understanding me??",
            "Nico| What's there to understand? You broke a guy's heart to get just a little more attention for yourself.",
            "Amelia| I believe that a weapon can be taken away from a player if they are found guilty of abuse on a server. I can think of several things you've done today that could probably be called abuse.",
            "Amanda| you wouldn't",
            "Amelia| Apologize.",
            "Amanda| ... you're really upset aren't you.",
            "Cody| You better believe it.",
            "Amanda| i shouldn't have done what i did. i'm sorry",
            "Cody| No you're not. You don't mean a word of that. But it will do.",
            "|Amanda logged off.",
        });

        level2.Add("Cody 15", new string[] {
            "|Your team has captured the final control point. You win!",
            "Sam| Well, glad that's over. You did good, Cody.",
            "Cody| Thanks guys. Thank you all for being here for me.",
            "Nico| No worries dude.",
            "Nico| But I can't help but thinking...",
            "Sam| Don't say it. No. Don't say anything.",
            "Nico| How do we know Amy's a girl?",
            "Amelia| You guys know me pretty well right?",
            "Nico| Yeah, we've known you for quite a while now.",
            "Amelia| Would you believe that this is me?",
            "Poof|Amelia has sent you a web link: Grangertown Honor Roll- Amelia Ramirez makes Honor Roll again this year.",
            "Nico| Yeah, I buy it. You're a real girl. Hey, I'm sorry for all the trouble I caused this time around.",
            "Amelia| It wasn't just you. I didn't help anything.",
            "Cody| You know what, I have a good feeling, guys. I feel like it's all going to be alright.",
            "Sam| You better believe it dude.",
            "Nico| So Amelia, since you're a girl, how's the unequal pay and longer bathroom lines treating you?",
            "Sam| ...And we're done here.",
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
