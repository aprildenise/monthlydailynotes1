using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // References
    public Animator animator;
    private Rigidbody2D body;
    public UIManager visuals;
    public ParticleSystem extra;
    public MainManager gameController;


    // Variables
    [SerializeField] private float speed; // speed of the player
    private float slowspeed; // slowed speed of the player
    private Vector2 wishvelocity;
    private int lives;
    private bool takeitslow;




    // Start is called before the first frame update
    void Awake()
    {
        // setups to make sure everything is alright
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        slowspeed = speed / 2f;
        lives = 3;
        extra.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // check if the player wants to move to another distant place

        // Check if the player wants to show their heart
        if (Input.GetButton("Cancel"))
        {
            takeitslow = true;
        }
        else
        {
            takeitslow = false;
        }

        Vector2 playerwishes = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (takeitslow)
        {
            wishvelocity = playerwishes.normalized * slowspeed;
            animator.SetBool("showingheart", true);
        }
        else
        {
            wishvelocity = playerwishes.normalized * speed;
            animator.SetBool("showingheart", false);
        }
        

    }


    public void PlayParticle()
    {
        extra.Play();
    }


    private void FixedUpdate()
    {
        // move player to distant lands based on their wishes in Update
        body.MovePosition(body.position + wishvelocity * Time.fixedDeltaTime);

    }

    public void LoseLife()
    {
        lives--;
        if (lives == 0)
        {
            Debug.Log("gameover");
            return;
        }
        visuals.RemoveHeart(lives);
    }
}
