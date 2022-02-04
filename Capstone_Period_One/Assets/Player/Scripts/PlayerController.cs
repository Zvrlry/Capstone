using System.Collections; // unity default
using System.Collections.Generic; // unity default 
using UnityEngine; // unity engine default

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")] // header attribute
    public float moveSpeed;

    [Header("Animation")] // header attribute
    public Animator myAnim; // animator componenent 


    [Header("RigidBody")] // header attribute 
    public Rigidbody2D rb; // MUST name ***rb*** in other scripts
    public static PlayerController instance;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            // giving player top down movement by input axis
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        myAnim.SetFloat("moveX", rb.velocity.x); // setting float for animation in blend tree
        myAnim.SetFloat("moveY", rb.velocity.y); // setting float for animation in blend tree

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) // if moving left or right
        {
            if (canMove)
            {
                myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal")); // set float in blend tree
                myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical")); // Set float in blend tree
            }
        }
    }
}
