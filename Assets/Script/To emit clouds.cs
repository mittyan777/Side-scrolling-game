using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toemitclouds : MonoBehaviour
{
    [SerializeField] GameObject kumo;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Toemit", 1, 2);

    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void Toemit()
    {
        Instantiate(kumo, new Vector3(-10,Random.Range(3,14),0), Quaternion.identity);
    }
}
