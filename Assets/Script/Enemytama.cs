using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemytama : MonoBehaviour
{
    float Speed;
    // Start is called before the first frame update
    void Start()
    {
        Speed = Random.Range(2, 6);
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, -120);
        transform.position -= Vector3.right * Speed * Time.deltaTime;
    }
}
