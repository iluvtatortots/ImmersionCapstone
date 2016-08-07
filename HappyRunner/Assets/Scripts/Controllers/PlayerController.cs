using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

        private bool isJumping = false;

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

        // Update is called once per frame
        void Update()
        {

            //Jump function on jump button press
            if (Input.GetButtonDown("Jump"))
            {
                if (GroundedCheck())
                {
                    Jump();
                }
            }

            //Player begins to fall if jump button is released while minimum jump time has passed, or if maximum jump time is reached.
            if ((!Input.GetButton("Jump") && jumpCount >= minJumpCount) || jumpCount >= maxJumpCount)
            {
                isJumping = false;
                jumpCount = 0;
            }
        }

        private void FixedUpdate()
        {
            //Increases height while player continues to hold jump button
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

        //Changes player state to jumping. Increase movement speed based on perfect and good jump timing.
        private void Jump()
        {

            //Changes player state
            isJumping = true;

            //this.transform.Translate(Vector3.up * jumpVertForce * Time.deltaTime);
            body.AddForce(new Vector2(0f, jumpVertForce));
        }

        //Adjust jump speed and height based on the player's current states
        private float AdjustJumpSpeed(float initialJumpSpeed)
        {
            float currentJumpSpeed = initialJumpSpeed;
            currentJumpSpeed *= ((float)(maxJumpCount - jumpCount) / (float)maxJumpCount);
            return currentJumpSpeed;
        }

        //Checks grounded state based on two short raycasts, the jump state, and player vertical velocity.
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
