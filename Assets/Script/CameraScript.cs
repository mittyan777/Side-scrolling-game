using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Transform Player_Pos;
    [SerializeField] float Move_Speed = 1.0f;
    [SerializeField] Transform Camera_Left_EndPoint;
    [SerializeField] Transform Camera_Right_EndPoint;

    // Start is called before the first frame update
    void Start()
    {
        Player_Pos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Initialize_CameraPos();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Player_Pos == null) return;

        //カメラの現在位置
        Vector3 Position = this.transform.position;

        Position.x = Mathf.Clamp(
            Player_Pos.position.x, Camera_Left_EndPoint.position.x, Camera_Right_EndPoint.position.x
            );

        this.transform.position =
        Vector3.Lerp(transform.position, Position, Move_Speed);
    }

    void Initialize_CameraPos()
    {
        this.transform.position = new Vector3(Camera_Left_EndPoint.position.x, 0, this.transform.position.z);
    }
}
