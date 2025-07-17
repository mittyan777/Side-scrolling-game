using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    GameObject parentGameObject;
    // Start is called before the first frame update
    void Start()
    {
        parentGameObject = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(parentGameObject.name);
    }
}
