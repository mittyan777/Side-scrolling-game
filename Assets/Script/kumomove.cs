using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kumomove : MonoBehaviour
{
    //[SerializeField] float DesSpawn;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("des", DesSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
    void des()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CloudEnd")
        {
            des();
        }
        if(collision.gameObject.tag == "StageHole")
        {
            des();
        }
    }
}
