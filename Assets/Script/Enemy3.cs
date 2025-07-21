using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    private bool isInsideCamera = false;
    GameObject parentGameObject;

    enum Type
    {
        UP,
        DOWN
    }

    [SerializeField] Enemy3Trigger TriggerObject;
    [Space(10)]
    [Header("出現位置")]
    [SerializeField] Type Spawn_Type;
    [Space(10)]
    Transform Parent_Transform;
    public float HP = 1;
    float speed;
    Vector3 startPos;
    bool Trigger_Player;
    bool Stopping_Anim;
    float Last_Sin = 0;
    float AnimTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        parentGameObject = transform.parent.gameObject;
        Parent_Transform = transform.parent.gameObject.transform;
        startPos = Parent_Transform.position;
        if (Spawn_Type == Type.UP)
        {
            Parent_Transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float posY = 0;
        float Sin_Value;
        speed = 1;

        if (!Stopping_Anim && isInsideCamera)
        {
            AnimTime += Time.deltaTime;
            Sin_Value = Mathf.Sin(AnimTime);
            Last_Sin = Sin_Value;
        }
        else
        {
            Sin_Value = Last_Sin;
        }

        // 上から出る場合
        if (Spawn_Type == Type.UP)
        {
            posY = startPos.y - Sin_Value * speed;

            // ほぼ startPos.y に戻ってきたときにアニメーション停止
            if (Sin_Value <= 0f && !Stopping_Anim && Trigger_Player) // sin波の下端（＝startPos）に戻ったとき
            {
                Stopping_Anim = true;
            }
        }
        // 下から出る場合
        else if (Spawn_Type == Type.DOWN)
        {
            posY = startPos.y + Sin_Value * speed;

            if (Sin_Value <= 0f && !Stopping_Anim && Trigger_Player) // sin波の上端（＝startPos + 1）に戻ったとき
            {
                Stopping_Anim = true;
            }
        }
        Parent_Transform.position = new Vector3(
            Parent_Transform.position.x, posY, Parent_Transform.position.z);


        //体力
        if (HP == 0)
        {
            Destroy(parentGameObject);
        }

        Trigger_Player = TriggerObject.Get_IsNearPlayer();
        if (!Trigger_Player) { Stopping_Anim = false; }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "fire")
        {
            Destroy(parentGameObject);
        }
    }

    private void OnBecameInvisible()
    {
        isInsideCamera = false;

    }
    private void OnBecameVisible()
    {
        isInsideCamera = true;

    }
}
