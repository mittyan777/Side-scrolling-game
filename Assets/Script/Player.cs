using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float max_MoveSpeed;
    [SerializeField] float Add_MoveSpeed;
    float Moving_Speed;
    SpriteRenderer spriteRenderer;
    GameObject parentGameObject;
    public ParticleSystem effect;
    [SerializeField]AudioSource jumpSource;
    [SerializeField]AudioSource HitSource;

    //ジャンプ関係
    [SerializeField] float Add_JumpPower;
    const float min_JumpPower = 5f;
    const float max_JumpPower = 12f;
    const int max_JumpHold = 30;

    private int holdJumpFrame = 0;
    private bool isJumpCharging = false;
    private bool isGrounded = true;
    private bool IsDead = false;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
      
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsDead) return;

        //移動
        Move();
        //ジャンプ
        Jump();
    }

    private void Jump()
    {
        //ジャンプ開始
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumpCharging)
        {
            jumpSource.Play();
            isJumpCharging = true;
            holdJumpFrame = 0;
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, min_JumpPower);
        }

        //ジャンプ中
        if (Input.GetKey(KeyCode.Space) && isJumpCharging)
        {
        
            //ジャンプする
            if (holdJumpFrame < max_JumpHold)
            {
                holdJumpFrame++;
                Debug.Log(holdJumpFrame);

                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + Add_JumpPower);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) || holdJumpFrame >= max_JumpHold)
        {
            isJumpCharging = false;
            holdJumpFrame = 0;
        }
    }
    private void Move()
    {
        float input = 0f;

        if (Input.GetKey("a")) input -= 1f;
        if (Input.GetKey("d")) input += 1f;

        // 入力に応じて加速
        if (input != 0)
        {
            
            if (animator.GetBool("jump") == false)
            {
                animator.SetBool("walk", true);
            }
            spriteRenderer.flipX = (input < 0); // 左向きならFlip

            //滑らかに逆方向へ引き返す
            if (input < 0 && Moving_Speed > 0)
            {
                //左向きから右向きへ
                Moving_Speed += Add_MoveSpeed;
                if (Moving_Speed > 0) Moving_Speed = 0;
                Debug.Log("LeftRight");
            }
            else if (input > 0 && Moving_Speed < 0)
            {
                //右向きから左向きへ
                Moving_Speed -= Add_MoveSpeed;
                if (Moving_Speed < 0) Moving_Speed = 0;
                Debug.Log("RightLeft");
            }

            Moving_Speed += Add_MoveSpeed * input;
            Moving_Speed = Mathf.Clamp(Moving_Speed, -max_MoveSpeed, max_MoveSpeed);
        }
        else
        {
            animator.SetBool("walk", false);
            // 減速（慣性のような動き）
            if (Moving_Speed > 0)
            {
                Moving_Speed -= Add_MoveSpeed * 5f;
                if (Moving_Speed < 0) Moving_Speed = 0;
            }
            else if (Moving_Speed < 0)
            {
                Moving_Speed += Add_MoveSpeed * 5f;
                if (Moving_Speed > 0) Moving_Speed = 0;
            }
        }
        Debug.Log(Moving_Speed);
        transform.position += transform.right * Moving_Speed * Time.deltaTime;
    }

    //死亡判定
    public bool Get_Player_IsDead() { return IsDead; }
    void Killing_Player()
    {
        IsDead = true;
    }

    void Jumpa()
    {
        float powerRatio = Mathf.Clamp01((float)holdJumpFrame / max_JumpHold);
        float JumpPower = Mathf.Lerp(min_JumpPower, max_JumpPower, powerRatio);
        rb.velocity = new Vector2(rb.velocity.x, JumpPower);


        isJumpCharging = false;
        isGrounded = false;
        holdJumpFrame = 0;
        Debug.Log("Jump!!");
    }

    //地面判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "StageHole")
        {
            Killing_Player();
        }
        if (collision.gameObject.tag == "MoveFloor")
        {
            isGrounded = true;
            transform.SetParent(collision.transform);
            animator.SetBool("jump", false);
        }

    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bloc" || collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            animator.SetBool("jump", false);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bloc" || collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
            animator.SetBool("walk", false);
            animator.SetBool("jump", true);
        }
        if (collision.gameObject.tag == "MoveFloor")
        {
            isGrounded = false;
            transform.parent = null;
            animator.SetBool("jump", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hitbox")
        {
            HitSource.Play();
            effect.Play();
            parentGameObject = collision.transform.parent.gameObject;
            rb.velocity = new Vector2(rb.velocity.x, 10);
            Destroy(parentGameObject);
        }
    }
}
