using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kumomove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("des", 30);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * 3 * Time.deltaTime;
    }
    void des()
    {
        Destroy(gameObject);
    }
}
