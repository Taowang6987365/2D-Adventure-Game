using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerController : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = 0.4f;
    public float moveSpeed = 6f;
    public float setTime = 0.1f;
    public bool facingRight = true;
    public bool bAttack;//for stand attack anim
    public bool isFinished;
    public bool isRunning;
    public bool canHit;
    public bool isHitByEnemy;
    public Animator animator;
    public Vector3 velocity;
    public static bool isMoveable;
    public static float gravity;
    public static float VelocityX;
    public static float VelocityY;
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;
    float jumpVelocity;
    float velocityXSmoothing;
    bool jumped;

    Controller2D controller;
    [SerializeField] private float timer;
    public float animationTime = 0.6f;

    public float fallModifier = 0.8f;
    [SerializeField] private GameObject guide;

    private void Awake()
    {
        //Simulate gravity
        //x = vt + 1/2at^2 (v = 0)
        //a = 2x / t^2
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2) * fallModifier;
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    void Start()
    {
        isMoveable = true;
        controller = GetComponent<Controller2D>();
        jumped = false;
        timer = setTime;
    }

    private void Update()
    {
        VelocityX = velocity.x;
        VelocityY += gravity;
        animator.SetFloat("Speed", Mathf.Abs(velocity.x));

        if (Mathf.Abs(velocity.x) < 0.01)
        {
            velocity.x = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {

            Controller2D.isGounded = false;
            jumped = true;
        }

        //flip sprite
        if (velocity.x > 1f && !facingRight)
        {
            CharacterFlip();
        }
        else if (velocity.x < 0 && facingRight)
        {
            CharacterFlip();
        }

        PlayerJump();

        PushItems();

        if (!bAttack)
        {
            PlayerAttack();
            PlayerMoveAttack();
        }

        if (Input.GetKey(KeyCode.LeftShift) && animator.GetFloat("Speed") >= 5.5f)
        {
            animator.SetBool("IsRunning", true);
            moveSpeed = 9f;
            isRunning = true;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && animator.GetFloat("Speed") <= 0.5f)
        {
            animator.SetBool("IsRunning", false);
        }
        else
        {
            animator.SetBool("IsRunning", false);
            moveSpeed = 6f;
        }

        if (PushBox.isPushing)
        {
            moveSpeed = 6f;
        }
    }

    void FixedUpdate()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        //when stand attack, disable player movement
        if (isMoveable)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            //if player stands on something and try to jump
            if (jumped)
            {
                velocity.y = jumpVelocity;
                jumped = false;
            }

            float targetVelocityX = input.x * moveSpeed;
            //Mathf.SmoothDamp:
            //Gradually changes a value towards a desired goal over time.
            velocity.x = Mathf.SmoothDamp(
                velocity.x,
                targetVelocityX,
                ref velocityXSmoothing,
                (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne
                );

            velocity.y += gravity * Time.fixedDeltaTime;
            controller.Move(velocity * Time.fixedDeltaTime, input);
        }
        else
        {
            velocity.x = 0;
        }
    }

    public void HitPlayer()
    {
        StartCoroutine(PlayerHurt());
    }

    internal void ResetPosition()
    {
        transform.position = Vector2.zero;
    }

    void CharacterFlip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void DoubleJump()
    {
        velocity.y = 0f;
        velocity.y += jumpVelocity * 1.1f;
    }

    void PushItems()
    {
        if (controller.collisions.below && PushBox.isPushing)
        {
            if (Mathf.Abs(velocity.x) > 3f)
            {
                timer -= Time.deltaTime;
                animator.SetBool("IsPushing", true);
            }
            else
            {
                PushBox.isPushing = false;
                if (!isFinished)
                {
                    velocity.x = 0;
                    isFinished = true;
                }
            }
        }
        else
        {
            if (timer <= 0f)
            {
                isFinished = false;
                timer = setTime;
            }
            animator.SetBool("IsPushing", false);
        }
    }

    void PlayerJump()
    {
        if (!Controller2D.isGounded)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
    }

    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.J) && Mathf.Abs(velocity.x) <= 0.5f && Controller2D.isGounded)
        {
            isMoveable = false;
            if (PushBox.distanceX <= PushBox.distance)
            {
                if (Controller2D.hitItem.tag == "PushItems")
                {
                    canHit = true;
                }
                else
                {
                    return;
                }
            }
            StartCoroutine(AttackAnim());
        }
    }

    void PlayerMoveAttack()
    {
        if (Controller2D.isGounded)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                StartCoroutine(WalkAttackAnim());
            }
        }
    }

    IEnumerator AttackAnim()
    {
        bAttack = true;
        animator.SetBool("IsAttack", true);
        yield return new WaitForSeconds(0.45f);
        if (canHit)
        {
            PushBox._pushableBox.GetHit();
        }
        animator.SetBool("IsAttack", false);
        isMoveable = true;
        bAttack = false;
        canHit = false;
    }

    IEnumerator WalkAttackAnim()
    {
        bAttack = true;
        animator.SetBool("IsWalkingAttack", true);
        yield return new WaitForSeconds(0.45f);
        animator.SetBool("IsWalkingAttack", false);
        bAttack = false;
    }

    IEnumerator PlayerHurt()
    {
        animator.SetBool("isHurt", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isHurt", false);
    }
    public void ShowGuide(string phrase)
    {
        guide.SetActive(true);
        guide.GetComponent<GuideNPC>().ShowUp(phrase);
    }
}

