using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movingfloor : MonoBehaviour
{
    [SerializeField] bool x = false;
    [SerializeField] bool y = false;
    [SerializeField] bool xy = false;
    [SerializeField] bool startmove = false;
    Vector3 startPos;//��`
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;//�������W�̑��

    }

    // Update is called once per frame
    void Update()
    {

        
            if (x == true && y == false && xy == false)
            {
                float posX = startPos.x + Mathf.Sin(Time.time) * 2;//�������W�{�����ړ���posY�ɑ��

                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
            }
            else if (x == false && y == true && xy == false)
            {
                float posY = startPos.y + Mathf.Sin(Time.time) * 2;//�������W�{�����ړ���posY�ɑ��

                transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            }
            else if (x == false && y == false && xy == true)
            {
                float posY = startPos.y + Mathf.Sin(Time.time) * 2;//�������W�{�����ړ���posY�ɑ��
                float posX = startPos.x + Mathf.Sin(Time.time) * 2;//�������W�{�����ړ���posY�ɑ��
                transform.position = new Vector3(posX, posY, transform.position.z);
            }
        

    }
 
}
