using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tamago : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        rb.velocity = new Vector2(Random.Range(-5,-15), Random.Range(5, 15));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
