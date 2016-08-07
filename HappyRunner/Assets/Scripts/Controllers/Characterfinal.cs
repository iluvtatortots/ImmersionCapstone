using UnityEngine;
using System.Collections;

public class Characterfinal : MonoBehaviour
{
    /*
	public 	float speed = 10f;
	public 	bool isJumping = false;				// Condition for whether the player should jump.	
	public 	float jumpForce = 1000f;		// Amount of force added when the player jumps.
	public 	bool gamestarted = false;		// Is the player currently running?
	private bool grounded = false;			// Whether or not the player is grounded.
	private Rigidbody2D rigidBody;			// Reference to the player's rigidbody
    */
    public float speed = 10f;
    public bool gamestarted = false;
    private bool isJumping = false;
    private bool grounded = false;
    public AudioSource jump;

    [SerializeField]
    protected Rigidbody2D body = null;

    [Header("Jump Values")]
    [SerializeField]
    protected float jumpVertForce = 500f;

    [SerializeField]
    protected int minJumpCount = 7;

    [SerializeField]
    protected int maxJumpCount = 30;

    [SerializeField]
    protected LayerMask groundedCheckLayerMask = new LayerMask();

    protected int jumpCount = 0;
    public Animator anim;                  // Reference to the player's animator component.

    void Awake()
    {
        
    }

    //the moment our character hits the ground we set the grounded to true 
    void OnCollisionEnter2D(Collision2D hit)
	{
		grounded = true;
	}
	
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        /*
        if (Input.GetButtonDown("Fire1"))
        {

            
            // If the jump button is pressed and the player is grounded and the character is running forward then the player should jump.
	     	if( (grounded == true) && (gamestarted == true))						
			{
				jump = true;
				grounded = false;
				anim.SetTrigger("Jump");
			}
		    // if the game is set now to start the character will start to run forward in the FixedUpdate
			else
			{
				gamestarted = true;
				anim.SetTrigger("Start");
			}
			
		}

		anim.SetBool("Grounded", grounded);
        */




        //Jump function on jump button press
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded )
            {
                Jump();
            }
            
        }

        

        anim.SetBool("Grounded", grounded);

        //Player begins to fall if jump button is released while minimum jump time has passed, or if maximum jump time is reached.
        if ((!Input.GetButton("Jump") && jumpCount >= minJumpCount) || jumpCount >= maxJumpCount)
        {
            isJumping = false;
            jumpCount = 0;
        }
    }


	//everything in the physics we set in the fixupdate 
	void FixedUpdate ()
	{
		// if game is started we move character forward and update the animator...
		/*if (gamestarted == true) 
		{
			body.velocity = new Vector2( speed , body.velocity.y  );
		}*/

        // If jump is set to true we are now adding quickly aforce to push the character up
        if (isJumping)
        {

            //Adds to the fixed update frames accumulated during the jump
            jumpCount++;

            //Increases height based on fixed update time and adjusted jump speed
            //this.transform.Translate(Vector3.up * AdjustJumpSpeed(jumpVertForce) * Time.fixedDeltaTime);
            body.AddForce(new Vector2(0, AdjustJumpSpeed(jumpVertForce)));

            //Resets player's y velocity so he/she won't fall while jumping
            body.velocity = new Vector2(body.velocity.x, 0f);
        }

    }

    private void Jump()
    {

        //Changes player state
        isJumping = true;
        grounded = false;

        //this.transform.Translate(Vector3.up * jumpVertForce * Time.deltaTime);
        body.AddForce(new Vector2(0f, jumpVertForce));
        jump.Play();
        anim.SetTrigger("Jump");
    }

    //Adjust jump speed and height based on the player's current states
    private float AdjustJumpSpeed(float initialJumpSpeed)
    {
        float currentJumpSpeed = initialJumpSpeed;
        currentJumpSpeed *= ((float)(maxJumpCount - jumpCount) / (float)maxJumpCount);
        return currentJumpSpeed;
    }

    
    private bool GroundedCheck()
    {
        Vector2 leftGroundedCheckStart = gameObject.transform.position + Vector3.left * .3f;
        Vector2 leftGroundedCheckEnd = gameObject.transform.position - Vector3.up * .55f + Vector3.left * .3f;
        Vector2 rightGroundedCheckStart = gameObject.transform.position + Vector3.right * .3f;
        Vector2 rightGroundedCheckEnd = gameObject.transform.position - Vector3.up * .55f + Vector3.right * .3f;

        bool leftGroundedCheck = Physics2D.Linecast(leftGroundedCheckStart, leftGroundedCheckEnd, groundedCheckLayerMask)/* || Physics2D.Linecast(gameObject.transform.position + Vector3.left * .3f, gameObject.transform.position - Vector3.up * .5f + Vector3.left * .3f)*/;
        bool rightGroundedCheck = Physics2D.Linecast(rightGroundedCheckStart, rightGroundedCheckEnd, groundedCheckLayerMask)/* || Physics2D.Linecast(gameObject.transform.position + Vector3.right * .3f, gameObject.transform.position - Vector3.up * .5f + Vector3.right * .3f)*/;

        Debug.DrawLine(leftGroundedCheckStart, leftGroundedCheckEnd);
        Debug.DrawLine(rightGroundedCheckStart, rightGroundedCheckEnd);

        return ((leftGroundedCheck ||
            rightGroundedCheck) &&
            //body.velocity.y < 0.1f &&
            //body.velocity.y > -0.1f &&
            !isJumping);
    }


}
