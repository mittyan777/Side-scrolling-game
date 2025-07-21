using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float max_MoveSpeed;
    [SerializeField] float Add_MoveSpeed;
    public float Moving_Speed;
    SpriteRenderer spriteRenderer;
    GameObject parentGameObject;
    public ParticleSystem effect;
    public ParticleSystem deseffect;

    //効果音
    AudioSource audioSource;
    [SerializeField] AudioClip JumpSound;
    [SerializeField] AudioClip HitSound;
    [SerializeField] AudioClip DeadSound;

    BoxCollider2D PlayerBoxCollider;
    [SerializeField] GameObject fire;
    float cooltime = 0;
    float ballcount = 0;
    [SerializeField] Text ballcountText;
    //ジャンプ関係
    [SerializeField] float Add_JumpPower;
    const float min_JumpPower = 5f;
    const int max_JumpHold = 30;
    private int holdJumpFrame = 0;
    private bool isJumpCharging = false;
    private bool isGrounded = true;

    private bool IsDead = false;
    public bool IsGoaled = false;
    public bool MOVEcontrol = false;
    public Animator animator;
    GameManager gameManager;

    public float input;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerBoxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameManager.Get_Is_TimeUP() && !IsDead) { Killing_Player(); }
        if (IsDead || IsGoaled) return;
        //移動
        if (MOVEcontrol == false) {Move(); }
        
        
        //ジャンプ
        Jump();
        
        cooltime -= 1 * Time.deltaTime;
        if (Input.GetKeyDown("f") && cooltime < 0 && ballcount > 0)
        {
            if (spriteRenderer.flipX == false)
            {
                Instantiate(fire, new Vector3(transform.position.x + 0.5f, transform.position.y + 1), Quaternion.identity);
            }
            else if(spriteRenderer.flipX == true) 
            {
                Instantiate(fire, new Vector3(transform.position.x - 0.5f, transform.position.y + 1), Quaternion.identity);
            }
            ballcount -= 1;
            cooltime = 1;
        }
        ballcountText.text = $"×{ballcount}";

    }

    private void Jump()
    {
        //ジャンプ開始
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumpCharging)
        {
            audioSource.PlayOneShot(JumpSound);
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
         input = 0f;

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
        transform.position += transform.right * Moving_Speed * Time.deltaTime;
    }

    //死亡判定
    public bool Get_Player_IsDead() { return IsDead; }
    void Killing_Player()
    {
        deseffect.Play();
        IsDead = true;
        PlayerBoxCollider.enabled = false;
        rb.velocity = new Vector2(rb.velocity.x, 10);
        audioSource.PlayOneShot(DeadSound);
    }
    void FallKill_Player()
    {
        IsDead = true;
        PlayerBoxCollider.enabled = false;
        audioSource.PlayOneShot(DeadSound);
    }

    //ゴール判定
    public void Set_Player_Goal(bool a)
    {
        IsGoaled = a;
    }

    //地面判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        //死亡処理
        if (collision.gameObject.tag == "Enemy")
        {
            Killing_Player();
        }
        if (collision.gameObject.tag == "StageHole")
        {
            FallKill_Player();
        }

        //動く床
        if (collision.gameObject.tag == "MoveFloor")
        {
            isGrounded = true;
            transform.SetParent(collision.transform);
            animator.SetBool("jump", false);
        }

        //ゴール処理
        if (collision.gameObject.tag == "Finish")
        {
            IsGoaled = true;
        }

        //ボール
        if (collision.gameObject.tag == "ball")
        {
            ballcount += 1;
            Destroy(collision.gameObject);
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
        if (collision.gameObject.tag == "Enemy")
        {
            Killing_Player();
        }
        if (collision.gameObject.tag == "Hitbox")
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            audioSource.PlayOneShot(HitSound);
            effect.Play();
            parentGameObject = collision.gameObject;
            parentGameObject.GetComponent<test>().hit();
        }
        if (collision.gameObject.tag == "Hitbox2")
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            audioSource.PlayOneShot(HitSound);
            effect.Play();
            parentGameObject = collision.gameObject;
            parentGameObject.GetComponent<test1>().hit();
        }
    }
}
