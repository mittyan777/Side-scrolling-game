using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    Vector3 dir2;
    private GameObject Player;
    Rigidbody2D rb;
    [SerializeField] ParticleSystem effect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        dir2 = Player.transform.position - transform.position;
        rb.velocity = new Vector3(dir2.x * -16, 8);
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {

            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            effect.Play();
            Invoke("des", 2);
            
        }
    }
    void des()
    {
        Destroy(gameObject);
    }
}
