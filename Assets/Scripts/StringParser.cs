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
 * A handy library for parsing string text and checking for flags.
 * A lot of these functions are ineffecient since they repeat a lot of operations.
 * 
 * ALL STRINGS SHOULD BE IN THIS FORM:
 * FLAG1 | FLAG2 | FLAG3 | DIALOGUE TEXT
 * e.g. 
 * "@'HELLO THERE':5,'WHY, HELLO':6 | !beep.wav | *glitter.prefab | 'And just who are you?'"
 * 
 * Trailing whitespace BEFORE or AFTER a | is acceptable, as we call Trim()
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

		str = str.Split('|');

		for (int i = 0; i < str.Length - 1; i++) { //don't need to check dialogue string for flags
			if (flagMap.ContainsKey(segment[0]))
                flagMap[c]++;
        }

        return flagMap;
    }

	public List<string> getChoices(string s) {
		List<string> choices = new List<string>();
		string[] splitString = s.Split('|');
		strFlags = splitString[0];

		bool reading = false;
		string choice = "";
		for (int i = 0; i < strFlags.Length; i++) {
			if (strFlags[i] == '@') 
				reading = true;
			if (reading) {

			}
		}

	}

    //Checks if the given char (arg provided as a flags.enum) present.
    //pass in the string as well to avoid needing to call CreateFlagDict independently.
    public bool ContainsFlag(string str, flags flag) {
        CreateFlagDict(str);
        return flagMap[(char)flag] > 0;
    }


}