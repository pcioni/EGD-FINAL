using UnityEngine;
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
 */
public class StringParser {

    private Dictionary<char, int> flagMap = new Dictionary<char, int> {
            {'@', 0},
            {'#', 0},
            {'_', 0},
            {'%', 0},
            {'$', 0},
        };

	private void resetFlagMap() {
		flagMap = new Dictionary<char, int> {
			{'@', 0},
			{'#', 0},
			{'_', 0},
			{'%', 0},
			{'$', 0},
		};
	}

    // Returns a Dict of flags and their occurences count. flagMap[flag] > 0 -> flag is present in string.
	public Dictionary<char, int> CreateFlagDict(string str) {
		resetFlagMap();
  
		foreach (char c in str) {
			if (flagMap.ContainsKey(c))
				flagMap[c]++;
        }

        return flagMap;
    }

	//returns SPEAKER in index 0, DIALOGUE in index 1
	//takes a string of form "SPEAKER|DIALOGUE
	public string[] ParseNameDialogueString(string s) {
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

	    return choices;

	}

    //Checks if the given char (arg provided as a flags.enum) present.
    //pass in the string as well to avoid needing to call CreateFlagDict independently.
    public bool ContainsFlag(string str, flags flag) {
        CreateFlagDict(str);
        return flagMap[(char)flag] > 0;
    }


}