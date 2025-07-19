using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    Rigidbody2D rb;
    private bool isInsideCamera = false;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

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
            transform.SetParent(collision.transform);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor"  && collision.gameObject.tag != "bloc")
        {
            if (isInsideCamera == true)
            {
                rb.velocity = new Vector2(-3, 8);
            }
        }
        if(collision.gameObject.tag == "bloc")
        {
            transform.position -= transform.right * 1 * Time.deltaTime;
        }
        if (collision.gameObject.tag == "StageHole")
        {
             Destroy(gameObject);
        }

    }
}
