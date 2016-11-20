using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    public float maxSpeed = 10f;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
	private Animator animator;
	public TextControl text_control;
	public Camera currentCamera;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator> ();
        ClampToGround();
    }
	
    private void FixedUpdate() {
        HandleInput();
    }

    private void HandleInput() {
        HandleMovement();
    }

    private void HandleMovement() {
		if (text_control.back.activeInHierarchy){
			rb.velocity = Vector2.zero;
			animator.SetInteger ("Direction", 0);
			return; //don't move if talking
		}
        //InputManager is currently set to 1/10 of a second to smooth input from +-1 to 0.
        float dx = Input.GetAxis("Horizontal");
        //float dy = Input.GetAxis("Vertical");

		if (dx > 0) {
			animator.SetInteger ("Direction", 1);
		} else if (dx < 0) {
			animator.SetInteger ("Direction", -1);
		} else {
			animator.SetInteger ("Direction", 0);
		}

        rb.velocity = new Vector2(dx * maxSpeed, rb.velocity.y);
		//animator.SetFloat ("Speed", rb.velocity.x);
    }
    
    //puts the player on the ground. Useful after teleporting, vertical movement, etc.
    public void ClampToGround()
    {
        //calculates raycast from the edge of the player. RAYCASTS ARE SET TO IGNORE TRIGGER COLLIDERS
        Vector2 self = new Vector2(transform.position.x, transform.position.y - boxCollider.bounds.size.y);
        RaycastHit2D hit = Physics2D.Raycast(self, -Vector2.up);
        if (hit.collider != null)
        {
            //Debug.Log("clamping");
            //Debug.Log(hit.point);
            //Player is adjust upwards equal to half othe boxcollider height
            transform.position = new Vector2(hit.point.x, hit.point.y + boxCollider.bounds.size.y / 2);
        }
    }

}
