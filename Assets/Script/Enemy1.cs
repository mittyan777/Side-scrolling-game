using System.Collections;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private bool Direction = false;
    private bool isInsideCamera = false;
    bool IsOn_MoveFloor;
    SpriteRenderer spriteRenderer;
    [SerializeField] float speed = 0;
    [SerializeField] GameObject heart1;
    [SerializeField] GameObject heart2;
    [SerializeField] GameObject parentGameObject;
    Transform Parent_Transform;
    public float HP = 2;

    const float Wait_ChangeDirection = 0.2f;
    float Direction_Timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        heart1.SetActive(true);
        heart2.SetActive(true);
        parentGameObject = transform.parent.gameObject;
        Parent_Transform = transform.parent.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInsideCamera == true)
        {
            speed = 3;
        }
        else
        {
            speed = 0;
        }
        if (HP == 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            Destroy(parentGameObject);

        }
        else if (HP == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
        }
        else if (HP == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
        }

        if (!IsOn_MoveFloor)
        {
            parentGameObject.transform.parent = null;
        }

        if (Direction_Timer > 0) { Direction_Timer -= Time.deltaTime; }

        if (Direction == false) { Parent_Transform.position -= Parent_Transform.right * speed * Time.deltaTime; }
        else { Parent_Transform.position += Parent_Transform.right * speed * Time.deltaTime; }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "fire")
        {
            Destroy(parentGameObject);
        }
        if (collision.gameObject.tag == "bloc" || collision.gameObject.tag == "Enemy")
        {
            if (Direction_Timer <= 0)
            {
                Direction_Timer = Wait_ChangeDirection;
                Direction = !Direction;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
        if (collision.gameObject.tag == "MoveFloor")
        {
            IsOn_MoveFloor = true;
            parentGameObject.transform.SetParent(collision.transform);
        }
        if (collision.gameObject.tag == "StageHole")
        {
            Destroy(parentGameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MoveFloor")
        {
            IsOn_MoveFloor = false;
        }
    }

    //�@�J��������O�ꂽ
    private void OnBecameInvisible()
    {
        isInsideCamera = false;

    }
    //�@�J�������ɓ�����
    private void OnBecameVisible()
    {
        isInsideCamera = true;

    }
}
