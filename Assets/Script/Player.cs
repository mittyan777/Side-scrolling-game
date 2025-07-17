using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float MoveSpeed_Left;
    [SerializeField] float MoveSpeed_Right;
    SpriteRenderer spriteRenderer;

    //ジャンプ関係
    float min_JumpPower = 1f;
    float max_JumpPower = 15f;
    int max_HoldFrames = 30;

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
        Debug.Log(isGrounded);
        //ジャンプ
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
            }
        }

    }

    private void FixedUpdate()
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bloc" || collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bloc" || collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }
}
