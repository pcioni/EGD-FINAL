using UnityEngine;
using System.Collections;

/// <summary>
/// Put this script on the background sprite.
/// </summary>
public class FitBackgroundToCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = Vector3.zero;
		transform.localScale = Vector3.one;

		float width = GetComponent<SpriteRenderer> ().bounds.size.x;
		float height = GetComponent<SpriteRenderer> ().bounds.size.y;

		float screenHeight = Camera.main.orthographicSize * 2.0f;
		float screenWidth = screenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3(screenWidth / width, screenHeight / height, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
