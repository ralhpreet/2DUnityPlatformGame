using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {
    //Private Instance Variables
    private Animator _animator;
    private float _move;
    private float _jump;
    private bool _facingRight;
    private Transform _transform;

	// Use this for initialization
	void Start () {
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
        this._move = 0f;
        this._jump = 0f;
        //set default animation  to "idle"
        this._animator.SetInteger("AnimState", 0);
        this._facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
        this._move = Input.GetAxis("Horizontal");
        this._jump = Input.GetAxis("Vertical");
        if (this._move != 0)
        {
            if (this._move > 0)
            {
                this._facingRight = true;
                this._flip();
            }
            if (this._move < 0)
            {
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
            //call the jump animation
            this._animator.SetInteger("AnimState", 2);
        }
	}

    //Private Methods

    private void _flip(){
        if(this._facingRight){
            this._transform.localScale=new Vector2(2.05f,1.6f);
        }
        else
        {
            this._transform.localScale = new Vector2(-2.05f, 1.6f);
        }
    }
}
