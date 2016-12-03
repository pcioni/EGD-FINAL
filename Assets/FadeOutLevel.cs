using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOutLevel : MonoBehaviour {
	Image fade_foreground;
	bool fading = false;
	AudioSource audioo;
	Information info;

	// Use this for initialization
	void Start () {
		fade_foreground = GetComponent<Image> ();
		audioo = GameObject.FindObjectOfType<AudioSource> ();
		info = GameObject.FindObjectOfType<Information> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Fade(){
		if (!fading) {
			fading = true;
			StartCoroutine (FadeOut ());
		}
	}

	IEnumerator FadeOut(){
		fade_foreground.enabled = true;
		while (fade_foreground.color.a < 1.0) {
			fade_foreground.color = new Color (
				fade_foreground.color.r,
				fade_foreground.color.g, 
				fade_foreground.color.b, 
				fade_foreground.color.a + .01f);
			audioo.volume = 1 - fade_foreground.color.a;
			yield return null;
		}
		info.IncrementLevelNumber ();
		SceneManager.LoadScene ("Main Menu");
	}
}
