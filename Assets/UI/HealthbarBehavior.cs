using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthbarBehavior : MonoBehaviour {
	public GameObject text_prefab;
	public GameObject text;

	// Use this for initialization
	void Start () {
		text = (GameObject) Instantiate(text_prefab, Vector3.zero, Quaternion.identity);
		text.transform.parent = GameObject.Find ("Floating Character Canvas").transform;
	}
	
	// Update is called once per frame
	void Update () {
		text.transform.position = new Vector3(transform.position.x, transform.position.y+0.1f);
	}

	public void SetHealth(float percentage){
		transform.localScale = new Vector3 (percentage/100.0f, 1, 1);
		text.GetComponent<Text> ().text =  percentage.ToString();
	}
}
