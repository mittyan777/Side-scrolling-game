using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossbattlespawn : MonoBehaviour
{
    [SerializeField] GameObject tamago;
    [SerializeField]float hindo;
    [SerializeField] bool ball; 
    // Start is called before the first frame update
    void Start()
    {
        if (ball == false)
        {
            InvokeRepeating("spawn", 1, hindo);
        }
        else
        {
            InvokeRepeating("spawn2", 5, hindo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void spawn()
    {
        Instantiate(tamago, transform.position, Quaternion.identity);
    }
    void spawn2()
    {
        Instantiate(tamago, new Vector3(Random.Range(-12,8),12,0), Quaternion.identity);
    }
}
