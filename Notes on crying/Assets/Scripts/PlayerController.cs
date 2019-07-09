using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // References
    [HideInInspector] public Animator animator;
    private Rigidbody2D body;
    public UIManager visuals;
    public ParticleSystem extra;
    public MainManager gameController;
    public AudioManager audio;


    // Variables
    [SerializeField] private float speed; // speed of the player
    private float slowspeed; // slowed speed of the player
    private Vector2 wishvelocity;
    private int lives;
    private bool takeitslow;
    public bool canText;
    public bool gameFinished;

    public bool onPhone;
    




    // Start is called before the first frame update
    void Awake()
    {
        // setups to make sure everything is alright
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        slowspeed = speed / 2f;
        lives = 3;
        canText = false;
        extra.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
        // if the player can interact with the phone 
        if (canText)
        {

            if (Input.GetKeyDown(KeyCode.Q) && !onPhone)
            {
                Debug.Log("pressed q");
                // Display phone
                visuals.DisplayPhone();
                gameController.InteractWithPhone(true);
                onPhone = true;
                return;
            }

            else if (Input.GetKeyDown(KeyCode.Q) && onPhone)
            {
                Debug.Log("pressed q");
                // hide phone
                visuals.HidePhone();
                gameController.InteractWithPhone(false);
                onPhone = false;
                return;
            }
        
        }

        // Check if the player wants to show their heart
        if (Input.GetKey("space"))
        {
            takeitslow = true;
        }
        else
        {
            takeitslow = false;
        }

        // check if the player wants to move to another distant place
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
        if (onPhone || gameFinished)
        {
            return;
        }
        lives--;
        audio.PlayRandPitched("drip");
        if (lives <= 0)
        {
            Debug.Log("gameover");
            StartCoroutine(gameController.GameOver());
        }
        visuals.RemoveHeart(lives);
    }
}
