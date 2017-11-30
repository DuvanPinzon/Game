using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss : MonoBehaviour
{
    Ataques ataqueScript;
    Mr MR;

    int vida = 30;
    public float velocidad = 5;
    public bool alive = true;
    bool trapped = false;
    public bool isPushing=false;
    public int daño = 1;
    float masa = 2;
    float activePush;

    GameObject puerta;
    GameObject flechas;

    bool balazo = false;
    Image vidaScroll;


    AudioSource audioSource;
    



    // Use this for initialization
    void Start()
    {
        flechas = GameObject.Find("flechas");
        puerta = GameObject.Find("puerta");
        audioSource = gameObject.GetComponent<AudioSource>();
        vidaScroll = GameObject.Find("vidafront").GetComponent<Image>();
        ataqueScript = GameObject.Find("ataqueScript").GetComponent<Ataques>();
        MR = GameObject.Find("MR").GetComponent<Mr>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        move();
        
        balazo = false;
        



    }

    IEnumerator isAlive()
    {
        //miro si el zombie está vivo
        if (vida <= 0)
        {
            //cambio sprite a muerto
            alive = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Animator>().SetBool("bossIsDead", true);
            yield return new WaitForSeconds(5f);
            //desaparezco
            Destroy(gameObject);
            flechas.SetActive(true);
            puerta.SetActive(true);


        }

    }

    IEnumerator isAttacking()
    {

        gameObject.GetComponent<Animator>().SetBool("bossIsAttacking", true);
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Animator>().SetBool("bossIsAttacking", false);


    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //miro que ataque hicieron
        if (coll.gameObject.name == "ataquenormal")
        {
            transform.Translate(0, ataqueScript.ataqueNormalPush / masa, 0);
            recibirDaño(ataqueScript.ataqueNormalDaño);

        }
        if (coll.gameObject.name == "ataquedoble")
        {
            transform.Translate(0, ataqueScript.ataqueDoblePush / masa, 0);
            recibirDaño(ataqueScript.ataqueDobleDaño);

        }
        if (coll.gameObject.name == "explosion")
        {
            transform.Translate(0, ataqueScript.explosionPush / masa, 0);
            recibirDaño(ataqueScript.explosionDaño);

        }
        if (coll.gameObject.name == "bala(Clone)")
        {
            if (balazo)
            {
                return;
            }
            balazo = true;
            transform.Translate(0, ataqueScript.balaPush / masa, 0);
            recibirDaño(ataqueScript.balaDaño);


        }
        if (coll.gameObject.name == "ataquepolvora")
        {
            transform.Translate(0, ataqueScript.ataquePolvoraPush / masa, 0);
            recibirDaño(ataqueScript.ataquePolvoraDaño);

        }
        if (coll.gameObject.name == "trampa(Clone)")
        {
            if (ataqueScript.trampaObject != null)
            {
                trapped = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "MR")
        {
            StartCoroutine("isAttacking");

        }

    }

    void move()
    {
        if (alive && !isPushing)
        {
            //roto dependiendo de la posición de Mr.
            Vector3 dir = Camera.main.WorldToScreenPoint(MR.transform.position) - Camera.main.WorldToScreenPoint(transform.position);
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (trapped == false && Vector3.Distance(transform.position, MR.transform.position) > 0.5f) //miro si la distancia es mayor a 1
            {
                transform.Translate(new Vector3(0, velocidad * Time.deltaTime, 0));

            }


        }
        if (ataqueScript.trampaObject == null)
        {
            trapped = false;
        }

    }
    void recibirDaño(int daño)
    {
        vida = vida - daño;
        vidaScroll.fillAmount = vida / 30f;
        StartCoroutine("parpadearSprite");
        StartCoroutine("isAlive");
        
    }
    IEnumerator parpadearSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }
    
}
