using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class floorRoteMoving : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject target2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target.transform.RotateAround(transform.position, Vector3.fwd, -15 * Time.deltaTime);
        target2.transform.RotateAround(transform.position, Vector3.fwd, -15 * Time.deltaTime);
        target.transform.Rotate(0,0,15 * Time.deltaTime);
        target2.transform.Rotate(0, 0, 15 * Time.deltaTime);
    }
}
