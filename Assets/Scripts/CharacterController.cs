using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    public float maxSpeed = 10f;
    private Rigidbody2D rb;

    void Awake() {
    }

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
    private void FixedUpdate() {
        HandleInput();
    }

    private void HandleInput() {
        HandleMovement();
    }

    private void HandleMovement() {
        //InputManager is currently set to 1/10 of a second to smooth input from +-1 to 0.
        float dx = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dx * maxSpeed, rb.velocity.y);
    }

	void Update () {
	
	}
}
