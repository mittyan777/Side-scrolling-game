using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float MoveSpeed_Left;
    [SerializeField] float MoveSpeed_Right;
    SpriteRenderer spriteRenderer;
    GameObject parentGameObject;

    //ジャンプ関係
    const float min_JumpPower = 1f;
    const float max_JumpPower = 12f;
    const int max_HoldFrames = 15;

    private int holdFrameCount = 0;
    private bool isJumpCharging = false;
    private bool isGrounded = true;
    private bool IsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        //死んだら実行しない
        if (IsDead) return;

        //移動
        Move();
        //ジャンプ
        JumpState();
    }

    //ジャンプ前の動作
    private void JumpState()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumpCharging)
        {
            isJumpCharging = true;
            holdFrameCount = 0;
        }

        if (isJumpCharging)
        {
            holdFrameCount++;
            Debug.Log(holdFrameCount);
            //ジャンプする
            if (Input.GetKeyUp(KeyCode.Space) || holdFrameCount >= max_HoldFrames)
            {
                Jump();
                Debug.Log(isGrounded);
            }
        }
    }
    private void Move()
    {
        if (Input.GetKey("a"))
        {
            //左に進む
            if (MoveSpeed_Left > -5)
            {
                spriteRenderer.flipX = true;
                MoveSpeed_Left -= 0.1f;
            }
        }
        else
        {
            //減速する
            if (MoveSpeed_Left < 0)
            {
                MoveSpeed_Left += 0.1f;
            }
            else if (MoveSpeed_Left > 0.01f)
            {
                MoveSpeed_Left = 0;
            }
        }

        if (Input.GetKey("d"))
        {
            //右に進む
            if (MoveSpeed_Right < 5)
            {
                spriteRenderer.flipX = false;
                MoveSpeed_Right += 0.1f;
            }
        }
        else
        {
            //減速する
            if (MoveSpeed_Right > 0)
            {
                MoveSpeed_Right -= 0.1f;
            }
            else if (MoveSpeed_Right < 0.01f)
            {
                MoveSpeed_Right = 0;
            }
        }

        transform.position += transform.right * MoveSpeed_Left * Time.deltaTime;
        transform.position += transform.right * MoveSpeed_Right * Time.deltaTime;
    }

    //死亡判定
    public bool Get_Player_IsDead() { return IsDead; }

    void Killing_Player()
    {
        IsDead = true;
    }

    void Jump()
    {
        float powerRatio = Mathf.Clamp01((float)holdFrameCount / max_HoldFrames);
        float JumpPower = Mathf.Lerp(min_JumpPower, max_JumpPower, powerRatio);
        rb.velocity = new Vector2(rb.velocity.x, JumpPower);


        isJumpCharging = false;
        isGrounded = false;
        holdFrameCount = 0;
        Debug.Log("Jump!!");
    }

    //地面判定
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bloc" || collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            transform.parent = collision.gameObject.transform;
        }
        if (collision.gameObject.tag == "StageHole")
        {
            Killing_Player();
        }

    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bloc" || collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
            transform.parent = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hitbox")
        {
            parentGameObject = collision.transform.parent.gameObject;
            rb.velocity = new Vector2(rb.velocity.x, 10);
            Destroy(parentGameObject);
        }
    }
}
