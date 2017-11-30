using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Mr : MonoBehaviour
{

    // Use this for initialization
    public Sprite vida5;
    public Sprite vida4;
    public Sprite vida3;
    public Sprite vida2;
    public Sprite vida1;
    public Sprite vida0;

    public Sprite bonus0;
    public Sprite bonus1;
    public Sprite bonus2;
    public Sprite bonus3;

    Vector3 moveVector;
    public float velocidadInicial =8;
    public float velocidad;
    public int vida=5;

    float dañoTempCD;
    float dañoCD = 1;

    GameObject vidaSprite;
    GameObject bonusSprite;
    GameObject spawnerObject;


    public GameObject normalHighPrefab;
    public GameObject dobleHighPrefab;
    public GameObject polvoraHighPrefab;
    public GameObject rifleHighPrefab;

    AudioSource audioSource;
    AudioSource audioGeneral;

    Zombie zombieScript;
    Big bigScript;
    Crisp crispScript;
    Boss bossScript;
    Throw throwScript;
    Aoe aoeScript;
    Audios audioScript;
    public int bonus =0;
    int bonusAnt=0;
    float bonusCD=10;
    float bonusCDTemp;

    public GameObject mouse;


    void Start()
    {
        velocidad = velocidadInicial;
        spawnerObject = GameObject.Find("Spawner");
        zombieScript = spawnerObject.GetComponent<Zombie>();
        bigScript = spawnerObject.GetComponent<Big>();
        crispScript = spawnerObject.GetComponent<Crisp>();
        bossScript = spawnerObject.GetComponent<Boss>();
        throwScript = spawnerObject.GetComponent<Throw>();
        aoeScript = spawnerObject.GetComponent<Aoe>();

        mouse = GameObject.Find("cursor");

        audioSource = gameObject.GetComponent<AudioSource>();
        audioScript = GameObject.Find("Audios").GetComponent<Audios>();
        audioGeneral = GameObject.Find("Audios").GetComponent<AudioSource>();

        vidaSprite = GameObject.Find("vida");
        bonusSprite = GameObject.Find("bonus");

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        lookAtMouse();
        StartCoroutine("checkBonus");

      
    }
    void Move()
    {
        if (vida > 0)
        {
            //waliking ->IDLE
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
                transform.Translate(velocidad * moveVector.normalized * Time.deltaTime, Space.World);
                gameObject.GetComponent<Animator>().SetBool("mrIsWalking", true);
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }


            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                gameObject.GetComponent<Animator>().SetBool("mrIsWalking", false);

            }
        }
                

    }
    
    void lookAtMouse()
    {
        if (vida > 0)
        {
            //apunta a donde se encuentra el mouse
            Vector3 dir = Camera.main.WorldToScreenPoint(mouse.transform.position) - Camera.main.WorldToScreenPoint(transform.position);
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }
    void checkBonus()
    {
        if (bonusCDTemp <= Time.time && bonus!=bonusAnt)
        {
            bonus = 0;
            Instantiate(normalHighPrefab);
            bonusChange(bonus);
            bonusAnt = bonus;
        }
        
    }

    void vidaChange(int vida)
    {
        if (vida == 5)
        {
            vidaSprite.GetComponent<SpriteRenderer>().sprite = vida5;
        }
        if (vida == 4)
        {
            vidaSprite.GetComponent<SpriteRenderer>().sprite = vida4;
        }
        if (vida == 3)
        {
            vidaSprite.GetComponent<SpriteRenderer>().sprite = vida3;
        }
        if (vida == 2)
        {
            vidaSprite.GetComponent<SpriteRenderer>().sprite = vida2;
        }
        if (vida == 1)
        {
            vidaSprite.GetComponent<SpriteRenderer>().sprite = vida1;
        }
        if (vida <= 0)
        {
            vidaSprite.GetComponent<SpriteRenderer>().sprite = vida0;
            
        }
    }

    IEnumerator mrDead()
    {
        gameObject.GetComponent<Animator>().SetBool("mrIsDead", true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("gameover");

    }

    void bonusChange(int bonus)
    {
        if (bonus == 3)
        {
            bonusSprite.GetComponent<SpriteRenderer>().sprite = bonus3;
            
        }
        if (bonus == 2)
        {
            bonusSprite.GetComponent<SpriteRenderer>().sprite = bonus2;
            
        }
        if (bonus == 1)
        {
            bonusSprite.GetComponent<SpriteRenderer>().sprite = bonus1;
            
        }
        if (bonus == 0)
        {
            bonusSprite.GetComponent<SpriteRenderer>().sprite = bonus0;
            
        }
    }

    
    void OnCollisionEnter2D(Collision2D coll)
    {
        //miro que ataque hicieron
        if (coll.gameObject.name == "zombie(Clone)") 
        {
            if (dañoTempCD <= Time.time)
            {
                recibirDaño(zombieScript.daño);
            } 
        }
        if (coll.gameObject.name == "big(Clone)")
        {
            if (dañoTempCD <= Time.time)
            {
                recibirDaño(bigScript.daño);
            }
        }
        if (coll.gameObject.name == "crisp(Clone)")
        {
            if (dañoTempCD <= Time.time)
            {
                recibirDaño(crispScript.daño);
            }
        }
        if (coll.gameObject.name == "boss")
        {
            if (dañoTempCD <= Time.time)
            {
                recibirDaño(bossScript.daño);
            }
        }

    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.name == "bossaoe(Clone)")
        {
            if (dañoTempCD <= Time.time)
            {
                recibirDaño(aoeScript.daño);
                audioGeneral.clip = audioScript.bossAoeLavaClip;
                audioGeneral.Play();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "doble(Clone)")
        {
            bonus = 1;
            Destroy(coll.gameObject);
            bonusChange(bonus);
            bonusCDTemp = Time.time + bonusCD;
            Instantiate(dobleHighPrefab);
            audioGeneral.clip = audioScript.recogerClip;
            audioGeneral.Play();

        }
        if (coll.gameObject.name == "polvora(Clone)")
        {
            bonus = 2; 
            Destroy(coll.gameObject);
            bonusChange(bonus);
            bonusCDTemp = Time.time + bonusCD;
            Instantiate(polvoraHighPrefab);
            audioGeneral.clip = audioScript.recogerClip;
            audioGeneral.Play();
        }
        if (coll.gameObject.name == "rifle(Clone)")
        {
            bonus = 3;
            Destroy(coll.gameObject);
            bonusChange(bonus);
            bonusCDTemp = Time.time + bonusCD;
            Instantiate(rifleHighPrefab);
            audioGeneral.clip = audioScript.recogerClip;
            audioGeneral.Play();
        }
        if (coll.gameObject.name == "spriteRotate")
        {
            if (dañoTempCD <= Time.time)
            {
                recibirDaño(throwScript.daño);
                audioGeneral.clip = audioScript.bossThrowClip;
                audioGeneral.Play();
            }
        }
        if (coll.gameObject.name == "bossaoe(Clone)")
        {
            if (dañoTempCD <= Time.time)
            {
                recibirDaño(aoeScript.daño);
                audioGeneral.clip = audioScript.bossAoeLavaClip;
                audioGeneral.Play();

            }
        }
        if (coll.gameObject.name == "puerta")
        {
            if (SceneManager.GetActiveScene().name == "Lvl1")
            {
                SceneManager.LoadScene("bossPreview");
            }
            else if (SceneManager.GetActiveScene().name == "boss")
            {
                SceneManager.LoadScene("bossEnd");
            }
            
        }

    }
    void recibirDaño(int daño)
    {
        dañoTempCD = Time.time + dañoCD;
        vida = vida - daño;
        vidaChange(vida);
        if (vida > 0)
        {
            StartCoroutine("parpadearSprite");
        }
        else
        {
            StartCoroutine("mrDead");
        }
        
    }
    
    IEnumerator parpadearSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.5f);
        yield return new WaitForSeconds(dañoCD);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
    }

    

}
