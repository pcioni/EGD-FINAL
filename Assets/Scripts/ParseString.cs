using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParseString : MonoBehaviour {

    /*
     * A handy library for parsing string text and checking for flags.
     */

    private Dictionary<char, int> flagMap = null;

    public enum flags {
        INTERRUPT = '$',
        LINEBREAK = '#',
        PAUSE = '_',
        CHOICE = '@',
        CHOICEINDEX = '%',
        CLEARANDPRINT = '~'
    }

    // Returns a Dict of flags and their occurences count. flagMap[flag] > 0 -> flag is present in string.
    public Dictionary<char, int> CreatFlagDict(string str) {
        flagMap = new Dictionary<char, int> {
            {'@', 0},
            {'#', 0},
            {'_', 0},
            {'~', 0},
            {'%', 0},
            {'$', 0}
        };

        foreach (char c in str) {
            if (flagMap.ContainsKey(c))
                flagMap[c]++;
        }

        return flagMap;
    }

    //Checks if the given char (arg provided as a flags.enum) present.
    //pass in the string as well to avoid needing to call CreateFlagDict independently.
    public bool DoesContainFlag(string str, char flag) {
        if (flagMap == null)
            CreatFlagDict(str);

        return flagMap[flag] > 0;
    }

}