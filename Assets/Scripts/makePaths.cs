using UnityEngine;
using System.Collections;

public class makePaths : MonoBehaviour {
	public GameObject node;
	int node_number = 0;
	Vector2 last_node_loc = Vector2.zero; 
	float spacing = 0.3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			if ((new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y)
			    - last_node_loc).magnitude > spacing) {
				GameObject n = AddNode ();
				last_node_loc = n.transform.position;
			}
		}
	}

	GameObject AddNode(){
		GameObject new_node = (GameObject) Instantiate (
			node, 
			new Vector3(
				Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
				Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
				0), 
			Quaternion.identity);
		new_node.name = node_number.ToString ();
		node_number++;
		new_node.transform.parent = transform;
		return new_node;
	}
		
}
