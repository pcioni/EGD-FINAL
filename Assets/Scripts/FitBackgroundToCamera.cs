using UnityEngine;
using System.Collections;

/// <summary>
/// Put this script on the background sprite.
/// </summary>
public class FitBackgroundToCamera : MonoBehaviour {
	private bool already_aligned = false;
	public GameObject my_camera;

	// Use this for initialization
	void Start () {
		if (!(FindObjectOfType<BattleManager>() || FindObjectOfType<ManageMenus>() || FindObjectOfType<Epilogue>()) /*&& transform.parent.GetComponent<IdentifyFirstScene>().firstScene*/) {
			my_camera = transform.parent.gameObject.GetComponentInChildren<Camera> ().gameObject;
			Align (my_camera.transform);
		}
		else if (FindObjectOfType<BattleManager>() || FindObjectOfType<ManageMenus>() || FindObjectOfType<Epilogue>()){
			Align (GameObject.FindObjectOfType<Camera>().transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Align(Transform t){
		if (already_aligned) return;
		//print ("aligning " + name);
		transform.localScale = Vector3.one;

		float width = GetComponent<SpriteRenderer> ().bounds.size.x;
		float height = GetComponent<SpriteRenderer> ().bounds.size.y;

		float screenHeight = Camera.main.orthographicSize * 2.0f;
		float screenWidth = screenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3(screenWidth / width, screenHeight / height, 0);
		transform.position = t.transform.position;
		transform.position = new Vector3 (transform.position.x, transform.position.y, 10);

		already_aligned = true;
	}
}
