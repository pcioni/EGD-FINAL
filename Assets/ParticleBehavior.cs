using UnityEngine;
using System.Collections;

public class ParticleBehavior : MonoBehaviour {

	private ParticleSystem ps;
	//travel time
	float travel_time_in_seconds = 1f;
	private float start_time;
	private float destination_time;
	//[Header("Options")]
	public bool destroyOnFinish;
	public bool spin;
	//public bool spinY;
	public Transform moveTo_start = null;
	public Transform moveTo_finish = null;
	//public string burst_name;
	public GameObject burst_obj;
	public bool arc;
	public GameObject current_path_node;
	public bool unravel;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
		start_time = Time.time;
		destination_time = Time.time + travel_time_in_seconds;
		if (moveTo_start != null && moveTo_finish != null) {
			transform.position = moveTo_start.position;
		} 
		else if (current_path_node != null) {
			transform.position = current_path_node.transform.position;
		}

		if (unravel) {
			StartCoroutine (Unravel ());
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(ps)
		{
			if(!ps.IsAlive() && destroyOnFinish)
			{
				DestroyThis();
			}
		}

		//Spin
		if (spin) {
			float spin_direction = -5f;
			if (moveTo_start != null && moveTo_finish != null
			    && moveTo_start.position.x > moveTo_finish.position.x)
				spin_direction = 5;
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 
												transform.eulerAngles.y, 
									transform.eulerAngles.z + spin_direction);
		}
		/*if (spinY) {
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 
												transform.eulerAngles.y-1, 
												transform.eulerAngles.z);
		}*/

		//MoveTo
		if (moveTo_start != null && moveTo_finish != null) {
			if (transform.position != moveTo_finish.transform.position) {
				//move in a certain time
				transform.position = Vector3.Lerp (moveTo_start.position, 
					moveTo_finish.position, (start_time + Time.time) / destination_time);

				//move at a certain speed
				//transform.position = Vector3.MoveTowards (transform.position, moveTo_finish.position, .15f);
			} 
			else {
				if (burst_obj != null)
					Instantiate (burst_obj, moveTo_finish.position, Quaternion.identity);
				//GameObject burst = Resources.Load<GameObject> ("Particles/" + burst_name);
				//print (burst.name);
				//burst.transform.position = moveTo_finish.position;
				DestroyThis ();
			}
			if (arc) {
				float percentage_across = (transform.position.x - moveTo_start.position.x)/(moveTo_finish.position.x - moveTo_start.position.x);
				//if (percentage_across > .5f)
					//percentage_across *= -1;
				float sine = Mathf.Sin((percentage_across*180)*Mathf.Deg2Rad);
				print (percentage_across);
				transform.position = new Vector3 (transform.position.x,
					moveTo_finish.position.y + sine,
					transform.position.z);
			}
		}

		//Path
		if (current_path_node != null) {
			//if we haven't reached our next goal
			if (transform.position != current_path_node.transform.position) {
				transform.position = Vector3.MoveTowards (transform.position, 
					current_path_node.transform.position,
					0.5f);
			}

			//else if we have
			else {
				//print ("we're at the node");
				SpriteRenderer[] nodes = current_path_node.transform.parent.GetComponentsInChildren<SpriteRenderer> ();
				int new_node = int.Parse (current_path_node.name) + 1;
				if (nodes.Length > new_node) {
					current_path_node = nodes [new_node].gameObject;
				} else {
					current_path_node = null;
				}
				//print (current_path_node.name);
			}
		}
	}

	IEnumerator Unravel(){
		yield return new WaitForSeconds (1f);
		GetComponent<ParticleSystem> ().startSpeed = 50;
		GetComponent<ParticleSystem> ().Emit (100);
		yield return new WaitForSeconds (1f);
		DestroyThis ();
		/* //While rather useless here, here's the code to change the rate of emission
		ParticleSystem.EmissionModule em = GetComponent<ParticleSystem> ().emission;
		ParticleSystem.MinMaxCurve rate = em.rate;
		rate.mode = ParticleSystemCurveMode.Constant;
		rate.constantMin = 0;
		rate.constantMax = 0;
		em.rate = rate;*/
	}

	void DestroyThis(){
		
		//alert Dan's code that the effect is done

		if (transform.parent != null) {
			Destroy (transform.parent.gameObject);
		}
		Destroy (gameObject);
	}
}
