using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float Speed = 0;
    [SerializeField] float Speed2 = 0;
    [SerializeField] float jumpPower;
    SpriteRenderer spriteRenderer;
      
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.up * jumpPower;
        }
    }
    private void FixedUpdate()
    {
        if(Input.GetKey("d"))
        {
            if (Speed < 5)
            {
                spriteRenderer.flipX = false;
                Speed += 0.1f;
            }
        }
        else
        {
            if (Speed > 0)
            {
                Speed -= 0.1f;
            }
            else if (Speed < 0.01f)
            {
                Speed = 0;
            }
        }
        if (Input.GetKey("a"))
        {
            if (Speed2 > -5)
            {
                spriteRenderer.flipX = true;
                Speed2 -= 0.1f;
            }
        }
        else
        {
            if (Speed2 < 0)
            {
                Speed2 += 0.1f;
            }
            else if (Speed2 > 0.01f)
            {
                Speed2 = 0;
            }
        }

        transform.position += transform.right * Speed * Time.deltaTime;
        transform.position += transform.right * Speed2 * Time.deltaTime;
    }
}
