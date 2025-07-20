using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    [SerializeField]GameObject parentGameObject;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(parentGameObject.name);
    }
    public void hit()
    {
        Debug.Log("OK");
        parentGameObject.GetComponent<Enemy2>().HP -= 1;
    }
}
