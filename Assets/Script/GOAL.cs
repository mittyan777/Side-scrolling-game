using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAL : MonoBehaviour
{
    [SerializeField] GameObject GOALText;
    BoxCollider2D boxCollider;
    Rigidbody body;
    [SerializeField] AudioSource BGMSound;
    bool Soundcontrol = false;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        // if (Soundcontrol == true)
        // {
        //     BGMSound.volume -= 1 * Time.deltaTime;
        //     
        // }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GOALText.SetActive(true);
            boxCollider = null;
            body = null;
            Soundcontrol = true;

            gameManager.Set_GoalState(true);
        }
    }
}
