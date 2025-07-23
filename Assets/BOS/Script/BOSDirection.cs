using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BOSDirection : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Animator animator2;
    public Transform Target_Object; // �����𑪂肽���I�u�W�F�N�gB
    [SerializeField] float distance;
    BoxCollider2D boxCollider2D;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        Target_Object = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, Target_Object.position);
        if (distance < 6)
        {
            gameManager.Set_GoalState(true);
            Invoke("direction", 3);
        }
        if (distance < 5)
        {
            Target_Object.GetComponent<Player>().MOVEcontrol = true;
            Target_Object.GetComponent<Player>().animator.SetBool("walk", false);
        }
        if (transform.position.y > 20)
        {
            Target_Object.GetComponent<Player>().animator.SetBool("walk", true);
            transform.position = new Vector2(transform.position.x, 30);
            Target_Object.transform.position += transform.right * 5 * Time.deltaTime;
        }

    }
    void action()
    {

        rb.velocity = new Vector3(0, 1, 0) * 20;
    }
    void direction()
    {

        animator.SetBool("jump", true);
        boxCollider2D.size = new Vector2(0.4f, 0.54f);
        Invoke("action", 1);
    }

}
