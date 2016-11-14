﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * These flags are used to indicate specific actions for the parsers.
 */ 
public enum flags {
    INTERRUPT = '$',
    LINEBREAK = '#',
    PAUSE = '_',
    CHOICE = '@',
    CHOICEINDEX = '%',
    CLEARANDPRINT = '~'
}


/*
 * A handy library for parsing  string text and checking for flags.
 * A lot of these functions are ineffecient since they repeat a lot of operations.
 * 
 * ALL STRINGS SHOULD BE IN THIS FORM:
 * FLAG1 | FLAG2 | FLAG3 | DIALOGUE TEXT
 * e.g. 
 * "@'HELLO THERE':5/'WHY, HELLO':6 | !beep.wav | *glitter.prefab | 'And just who are you?'"
 * 
 * Trailing whitespace BEFORE or AFTER a | is acceptable, as we call Trim()
 * 
 * TODO: make this not god awfully ineffecient. 
 */
public class StringParser : MonoBehaviour {

    private Dictionary<char, int> flagMap = new Dictionary<char, int> {
            {'@', 0},
            {'#', 0},
            {'_', 0},
            {'~', 0},
            {'%', 0},
            {'$', 0},
			{'*', 0},
			{'>', 0},
			{'!', 0}
        };

	private void resetFlagMap() {
		flagMap = new Dictionary<char, int> {
			{'@', 0},
			{'#', 0},
			{'_', 0},
			{'~', 0},
			{'%', 0},
			{'$', 0},
			{'*', 0},
			{'>', 0},
			{'!', 0}
		};
	}

    // Returns a Dict of flags and their occurences count. flagMap[flag] > 0 -> flag is present in string.
	public Dictionary<char, int> CreateFlagDict(string str) {
		resetFlagMap();

		string[] splitStr = str.Split('|');

		foreach (string segment in splitStr) {
			if (flagMap.ContainsKey(segment[0]))
				flagMap[segment[0]]++;
        }

        return flagMap;
    }

	//returns SPEAKER in index 0, DIALOGUE in index 1
	//takes a string of form "SPEAKER|DIALOGUE
	public string[] ParseString(string s) {
		string[] result = new string[2];
		string[] split = s.Split('|');
		result[0] = split[0];
		result[1] = split[1];
		return result;
	}

	//pass this function the original string.
	public List<string> getChoices(string s) {
		List<string> choices = new List<string>();
		string[] splitString = s.Split('|');

		string choicesString = null;
		foreach (string segment in splitString) {
			if (segment[0] == '@')
				choicesString = segment;
		}

		for (int i = 0; i < choicesString.Length; i++) {

		}

		return choices;

	}

    //Checks if the given char (arg provided as a flags.enum) present.
    //pass in the string as well to avoid needing to call CreateFlagDict independently.
    public bool ContainsFlag(string str, flags flag) {
        CreateFlagDict(str);
        return flagMap[(char)flag] > 0;
    }


}