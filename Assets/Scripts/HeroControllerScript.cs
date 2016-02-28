using UnityEngine;
using System.Collections;

//Velocity range utility class#######
[System.Serializable]
public class VelocityRange
{
    //Public Instance Variable
    public float minimum;
    public float maximum;

    //Constructor#####
    public VelocityRange(float minimum, float maximum)
    {
        this.minimum = minimum;
        this.maximum = maximum;
    }
}

public class HeroControllerScript : MonoBehaviour
{

    //Public Instance variables
    public VelocityRange velocityRange;
    public float moveForce;
    public float jumpForce;
    public Transform groundCheck;
    public Transform camera;

    //public GameController gameController;


    //Private Instance Variables
    private Animator _animator;
    private float _move;
    private float _jump;
    private bool _facingRight;
    private Transform _transform;
    private Rigidbody2D _rigidBody2D;
    private bool _isGrounded;

    // Use this for initialization
    void Start()
    {
        //Initialize Public Variables
        this.velocityRange = new VelocityRange(300f, 2000f);
        // this.moveForce = 50f;
        //this.jumpForce = 500f;
        //Initialize Private Varibles 
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
        this._rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        this._move = 0f;
        this._jump = 0f;
        //set default animation  to "idle"
        //this._animator.SetInteger("AnimState", 0);
        this._facingRight = true;
        // place the hero in the starting position
        this._spawn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // this._isGrounded = true;
        Vector3 currentPosition = new Vector3(this._transform.position.x, this._transform.position.y, -10f);
        this.camera.position = currentPosition;
        this._isGrounded = Physics2D.Linecast(this._transform.position, this.groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(this._transform.position, this.groundCheck.position);
        float forceX = 0f;
        float forceY = 0f;

        //ge absolute value of velocity for our gameobject
        float absVelX = Mathf.Abs(this._rigidBody2D.velocity.x);
        float absVelY = Mathf.Abs(this._rigidBody2D.velocity.y);
        Debug.Log(_isGrounded);
        //check if the player is grounded
        if (this._isGrounded)
        {
            //gets a number between -1 to 1 for both horizontaland vertical axis
            this._move = Input.GetAxis("Horizontal");
            this._jump = Input.GetAxis("Vertical");
            if (this._move != 0)
            {
                if (this._move > 0)
                {
                    //movement force
                    if (absVelX < this.velocityRange.maximum)
                    {
                        forceX = this.moveForce;
                    }
                    this._facingRight = true;
                    this._flip();
                }
                if (this._move < 0)
                {
                    //movement force
                    if (absVelX < this.velocityRange.maximum)
                    {
                        forceX = -this.moveForce;
                    }
                    this._facingRight = false;
                    this._flip();
                }
                //call the walk animation
                this._animator.SetInteger("AnimState", 1);
            }
            else
            {
                //set to idle
                this._animator.SetInteger("AnimState", 0);
            }
            if (this._jump > 0)
            {
                //jump force
                if (absVelY < this.velocityRange.maximum)
                {
                    forceY = this.jumpForce;
                }

            }
        }
        else
        {
            //call the jump animation
            this._animator.SetInteger("AnimState", 2);
        }
        //Apply forces to the player
        this._rigidBody2D.AddForce(new Vector2(forceX, forceY));
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        /*if (other.gameObject.CompareTag("Coin"))
        {
            this._coinSound.Play();
            Destroy(other.gameObject);
            this.gameController.ScoreValue += 10;
        }

        if (other.gameObject.CompareTag("SpikedWheel"))
        {
            this._hurtSound.Play();
            this.gameController.LivesValue--;
        }*/


        if (other.gameObject.CompareTag("Death"))
        {
            this._spawn();
            //this._hurtSound.Play();
           // this.gameController.LivesValue--;
        }
    }

    //Private Methods

    private void _flip()
    {
        if (this._facingRight)
        {
            this._transform.localScale = new Vector2(2.05f, 1.6f);
        }
        else
        {
            this._transform.localScale = new Vector2(-2.05f, 1.6f);
        }
    }

    private void _spawn()
    {
        this._transform.position = new Vector3(-2009, 500f, 0);
    }
}
