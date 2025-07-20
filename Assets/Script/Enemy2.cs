using System;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    Rigidbody2D rb;
    private bool isInsideCamera = false;
    [SerializeField] GameObject heart1;
    [SerializeField] GameObject heart2;
    [SerializeField] GameObject parentGameObject;
    Transform Parent_Transform;
    public float HP = 2;
    // Start is called before the first frame update
    void Start()
    {
        heart1.SetActive(true);
        heart2.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        parentGameObject = transform.parent.gameObject;
        Parent_Transform = transform.parent.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MoveFloor")
        {
            parentGameObject.transform.SetParent(collision.transform);
        }
        if (collision.gameObject.tag == "StageHole")
        {
            Destroy(parentGameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && collision.gameObject.tag != "bloc")
        {
            if (isInsideCamera == true)
            {
                rb.velocity = new Vector2(-3, 8);
            }
        }
        if (collision.gameObject.tag == "bloc")
        {
            Parent_Transform.position -= Parent_Transform.right * 1 * Time.deltaTime;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MoveFloor")
        {
            try
            {
                parentGameObject.transform.parent = null;
            }
            catch
            {
                Debug.Log("parent delete error");
            }
        }
    }
}
