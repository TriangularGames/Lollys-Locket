using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // Moving variables
    private Rigidbody2D rb;
    public float speed = 2.5f;
    bool mvHoz = false;
    bool mvVer = false;

    //Animation
    public Animator an;

    //Footstep noises
    private AudioSource AS;

    //Chase stuff
    public bool isInFoyer = false;
    public bool isGettingChased;

    //Actual walking
    private bool isWalking;

    private PlayerInteraction interact;

    Vector2 movement;

    void Start()
    {
        interact = GetComponent<PlayerInteraction>();
        rb = GetComponent<Rigidbody2D>();
        AS = GetComponent<AudioSource>();
        an = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0 && !interact.interacting)
        {
            if (mvHoz)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                
            }
            else if (mvVer)
            {
                movement.y = Input.GetAxisRaw("Vertical");
            }
        }
        else
        {
            mvHoz = Input.GetAxisRaw("Horizontal") != 0;
            movement.x = Input.GetAxisRaw("Horizontal");
            mvVer = Input.GetAxisRaw("Vertical") != 0;
            movement.y = Input.GetAxisRaw("Vertical");
        }

        if (!interact.interacting)
        {
            an.SetFloat("Hor", movement.x);
            an.SetFloat("Ver", movement.y);

            an.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!interact.interacting)
        {
            if (rb.velocity.x != 0 || (Input.GetAxisRaw("Vertical") != 0))
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            if (isWalking)
            {
                if (!AS.isPlaying)
                    AS.Play();
            }
            else
            {
                AS.Stop();
            }

            rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
        }
        else
        {
            an.SetFloat("Hor", 0);
            an.SetFloat("Ver", 0);

            an.SetFloat("Speed", 0);
            AS.Stop();
            isWalking = false;
            rb.velocity = new Vector2(0, 0);
        }



    }


}
