using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private bool Direction = false;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Direction == false) { transform.position -= transform.right * 3 * Time.deltaTime; }
        else { transform.position += transform.right * 3 * Time.deltaTime; }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "bloc")
        {
           if(Direction == false) { Direction = true; }
            else {  Direction = false; }

           if(spriteRenderer.flipX == false) { spriteRenderer.flipX = true; }
            else {  spriteRenderer.flipX = false; }
            
        }
    }
}
