using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BOS : MonoBehaviour
{
    Vector3 startPos;//’è‹`
    float HP = 5;
    public ParticleSystem effect;
    public ParticleSystem deseffect;
    GameManager gameManager;
    [SerializeField] GameObject GOALText;
    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        startPos = transform.position;//‰ŠúÀ•W‚Ì‘ã“ü
    }

    // Update is called once per frame
    void Update()
    {
        float posY = startPos.y + Mathf.Sin(Time.time) * 3;//‰ŠúÀ•W{‰•œˆÚ“®‚ğposY‚É‘ã“ü

        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        if(HP == 0 )
        {
            GOALText.SetActive(true);
            //gameManager.Set_GoalState(true);
            effect.Play();
            Invoke("des", 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "fire")
        {
            HP -= 1;
            deseffect.Play();
            Destroy(collision.gameObject);
        }
    }
    void des()
    {
        //GameObject.FindWithTag("Player").GetComponent<Player>().IsGoaled = true;
        SceneManager.LoadScene("Title");
        deseffect.Play();
        Destroy(gameObject);    
    }
}
