using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private bool Direction = false;
    private bool isInsideCamera = false;
    SpriteRenderer spriteRenderer;
    [SerializeField] float speed = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isInsideCamera == true)
        {
            speed = 3;

            Debug.Log("ADAWDAD");
        }
        else
        {
            speed = 0;
        }


        if (Direction == false) { transform.position -= transform.right * speed * Time.deltaTime; }
        else { transform.position += transform.right * speed * Time.deltaTime; }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "fire")
        {
         
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "bloc")
        {
            if (Direction == false) { Direction = true; }
            else { Direction = false; }

            if (spriteRenderer.flipX == false) { spriteRenderer.flipX = true; }
            else { spriteRenderer.flipX = false; }
        }
        if (collision.gameObject.tag == "MoveFloor")
        {
            transform.SetParent(collision.transform);
        }
        if (collision.gameObject.tag == "StageHole")
        {
            Destroy(gameObject);
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
