using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform Player_Pos;
    [SerializeField] float Move_Offset = 1.0f;
    [SerializeField] float Move_Speed = 1.0f;
    [SerializeField] float Camera_Left_EndPoint;
    [SerializeField] float Camera_Right_EndPoint;

    Transform Player_Object;
    Camera cameraObject;

    // Start is called before the first frame update
    void Start()
    {
        Player_Pos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        this.transform.position = new Vector3(Player_Pos.position.x, 0, this.transform.position.z);

        cameraObject = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Player_Pos == null) return;

        Vector3 Position = this.transform.position;

        Position.x = Mathf.Clamp(Player_Pos.position.x, Camera_Left_EndPoint, Camera_Right_EndPoint);

        this.transform.position =
        Vector3.Lerp(transform.position, Position, Move_Speed);
    }
}
