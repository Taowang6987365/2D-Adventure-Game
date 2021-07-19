using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(Controller2D))]
public class PlayerController : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = 0.8f;
    public float moveSpeed = 6f;
    public float setTime = 0.1f;
    public float animationTime = 0.6f;
    public float fallModifier = 0.8f;
    public bool facingRight = true;
    public bool bAttack;//for stand attack anim
    public bool isFinished;
    public bool isRunning;
    public bool canHit;
    public bool isHitByEnemy;
    public bool doOnce;

    public BoxCollider2D boxCollider;
    public Animator animator;
    public Vector3 velocity;
    public AudioClip[] audioClips;//slash,jump,hurt,death
    public AudioSource audioSource;
    public static bool isMoveable;
    public static float gravity;
    public static float VelocityX;
    public static float VelocityY;
    public static PlayerController playerControllerInstance;

    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;
    float jumpVelocity;
    float velocityXSmoothing;
    bool jumped;

    Controller2D controller;
    [SerializeField] private float timer;
    [SerializeField] private GameObject guide;
    [SerializeField] private int playerID = 0;
    [SerializeField] private Rewired.Player player;

    public delegate void boxHit();
    public boxHit hit;

    public float attackVol;
    public float jumpVol;
    public float hurtVol;
    public float deathVol;

    public Rewired.Player Player { get => player; set => player = value; }

    private void Awake()
    {
        playerControllerInstance = this;
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
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
        doOnce = false;
        player = ReInput.players.GetPlayer(playerID);
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

        if (player.GetButtonDown("Jump") && controller.collisions.below)
        {
            PlaySound(1,jumpVol);
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

        if (player.GetButton("Accelerate") && animator.GetFloat("Speed") >= 5.5f)
        {
            animator.SetBool("IsRunning", true);
            moveSpeed = 9f;
            isRunning = true;
        }
        else if (player.GetButton("Accelerate") && animator.GetFloat("Speed") <= 0.5f)
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
            Vector2 input = new Vector2(player.GetAxisRaw("Move Horizontal"), player.GetAxisRaw("Move Vertical"));
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

            velocity.y += gravity * Time.fixedDeltaTime * 0.9f;
            controller.Move(velocity * Time.fixedDeltaTime, input);
        }
        else
        {
            velocity.x = 0;
        }
    }

    public void HitPlayer()
    {
        if (PlayerStatus.instance.lives > 0)
        {
            PlaySound(2,hurtVol);
            StartCoroutine(PlayerHurt());
        }
    }

    public void Death()
    {
        PlaySound(3,deathVol);
        if (controller.collisions.below)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    internal void ResetPosition()
    {
        transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
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
        PlaySound(4,1f);
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
        if (player.GetButtonDown("Attack") && Mathf.Abs(velocity.x) <= 0.5f && Controller2D.isGounded)
        {
            PlaySound(0,attackVol);
            isMoveable = false;
            if (Controller2D.isPlayerHit && !doOnce)
            {
                doOnce = true;
                pushablebox pb = Controller2D.hitItem.GetComponent<pushablebox>();
                hit = new boxHit(pb.GetHit);
            }
            StartCoroutine(AttackAnim());
        }
    }

    void PlayerMoveAttack()
    {
        if (Controller2D.isGounded)
        {
            if (player.GetButtonDown("Attack") && !doOnce)
            {
                PlaySound(0,attackVol);
                if (Controller2D.isPlayerHit)
                {
                    doOnce = true;
                    pushablebox pb = Controller2D.hitItem.GetComponent<pushablebox>();
                    hit = new boxHit(pb.GetHit);
                }
                StartCoroutine(WalkAttackAnim());
            }
        }
    }

    IEnumerator AttackAnim()
    {
        bAttack = true;
        animator.SetBool("IsAttack", true);
        yield return new WaitForSeconds(0.45f);
        animator.SetBool("IsAttack", false);
        hit?.Invoke();
        isMoveable = true;
        bAttack = false;
        doOnce = false;
    }

    IEnumerator WalkAttackAnim()
    {
        bAttack = true;
        animator.SetBool("IsWalkingAttack", true);
        yield return new WaitForSeconds(0.45f);
        animator.SetBool("IsWalkingAttack", false);
        hit?.Invoke();
        bAttack = false;
        doOnce = false;
    }

    IEnumerator PlayerHurt()
    {
        animator.SetBool("isHurt", true);
        yield return new WaitForSeconds(0.2f);
        PlayerStatus.instance.lives--;
        animator.SetBool("isHurt", false);
    }

    IEnumerator PlayerDeath()
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("isDead", false);
    }

    public void ShowGuide(int logID)
    {
        Debug.Log("trig");
        guide.SetActive(true);
        guide.GetComponent<GuideNPC>().ShowUp(logID);
    }

    public void PlaySound(int id, float vol)
    {
        audioSource.clip = audioClips[id];
        audioSource.volume = vol;
        audioSource.PlayOneShot(audioSource.clip);
    }
}

