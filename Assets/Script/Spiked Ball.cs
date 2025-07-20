using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    [SerializeField] GameObject Spiked_Ball;
    // Start is called before the first framÅQe update
    void Start()
    {
        InvokeRepeating("Spawn", 1, 3);

    }

    // Update is called once per frame
    void Update()
    {
       

    }
    void Spawn()
    {
         Instantiate(Spiked_Ball, transform.position, Quaternion.identity);
    }
}
