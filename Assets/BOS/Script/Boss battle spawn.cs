using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossbattlespawn : MonoBehaviour
{
    [SerializeField] GameObject oya;
    [SerializeField] GameObject tamago;
    [SerializeField]float hindo;
    [SerializeField] bool ball;
    [SerializeField] bool ball2;
    [SerializeField] bool shoot = false;
    [SerializeField] float distance;
    [SerializeField] int randomhindo;
    public Transform Target_Object;
    // Start is called before the first frame update
    void Start()
    {
        if (ball == false && ball2 == false)
        {
            InvokeRepeating("spawn", 1, hindo);
        }
        else if(ball == true && ball2 == false )
        {
            InvokeRepeating("spawn2", 5, hindo);
        }
        else if(ball == false && ball2 == true)
        {
            InvokeRepeating("spawn3", 1, 2);
            InvokeRepeating("ranndamu", 1, randomhindo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindWithTag("Player").GetComponent<Transform>() != null) 
        {
            Target_Object = GameObject.FindWithTag("Player").GetComponent<Transform>();
            distance = Vector3.Distance(this.transform.position, Target_Object.position);
        }
       
        
       
        if (distance < 30)
        {
            shoot = true;
        }
    }
    void ranndamu()
    {
        randomhindo = Random.Range(1, 3);
    }
    void spawn()
    {
        if (tamago != null && oya != null)
        {
            Instantiate(tamago, transform.position, Quaternion.identity);
        }
    }
    void spawn2()
    {
        if (tamago != null && oya != null)
        {
            Instantiate(tamago, new Vector3(Random.Range(-12, 8), 12, 0), Quaternion.identity);
        }
    }
    void spawn3()
    {
        if (shoot == true && tamago != null && oya != null)
        {
            Instantiate(tamago, oya.transform.position, Quaternion.identity);
        }
    }
}
