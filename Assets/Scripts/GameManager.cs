using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    void Awake() {
        if (instance == null)
            instance = this;

        //Enforce singleton pattern.
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}