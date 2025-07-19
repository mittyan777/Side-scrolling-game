using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private bool Direction = false;
    private bool isInsideCamera = false;
    SpriteRenderer spriteRenderer;
    [SerializeField] float speed = 0;
    [SerializeField] GameObject heart1;
    [SerializeField] GameObject heart2;
    public float HP = 2;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       heart1.SetActive(true);
        heart2.SetActive(true);
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
        if(HP == 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            Destroy(gameObject);
        }
        else if(HP == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
        }
        else if(HP == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
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
