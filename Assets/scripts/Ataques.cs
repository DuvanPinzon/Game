using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Ataques : MonoBehaviour {

    public GameObject bombaPrefab;
    public GameObject trampaPrefab;
    public GameObject balaPrefab;
    public GameObject slot;
    GameObject GameScreen;

    Mr MR;
    Bomba bombaScript;
    Audios audioScript;

    AudioSource audioSource;


    public int collCount;

    bool isBoostCD;
    float boostTempCD;
    float boostCD = 2f;
    float boostTempSpeed;
    float boostSpeed = 24;
    float boostTimeSpeed = 0.1f;

    GameObject balaObject;

    Image boostCDScroll;
    Image bombaCDScroll;
    Image trampaCDScroll;
    Image attackCDScroll;

    GameObject bombaObject;
    GameObject ataqueNormal;
    GameObject ataqueDoble;
    GameObject ataquePolvora;
    public GameObject trampaObject;

    float attackTempCD;
    float attackTempActive;
    float attackCD = 0.85f;
    float attackActiveTime = 0.05f;
    bool isAttackCD=false;
    public bool hasExplode = false;
    bool isTrampaActivaded = false;

    float trampaTempCD;
    float trampaTempActive;
    float trampaCD = 5f;
    float trampaActiveTime = 3f;
    bool isTrampaCD = false;

    float bombaTempCD;
    public float explosionTempActive;
    float bombaCD = 6f;
    bool isBombaCD = false;

    public float ataqueNormalPush = -1f;
    public float ataqueDoblePush = -2f;
    public float ataquePolvoraPush = -1f;
    public float balaPush = -1f;
    public float explosionPush = -4f;

    float recoil= -0.3f;

    public int ataqueNormalDaño = 1;
    public int ataqueDobleDaño = 1;
    public int ataquePolvoraDaño = 2    ;
    public int explosionDaño = 2;
    public int balaDaño = 1;

    // Use this for initialization
    void Start () {
        GameScreen = GameObject.Find("Game");
        slot = GameObject.Find("slot");
        //Instantiate(bombaPrefab, slot.transform.position,slot.transform.rotation,slot.transform);
        boostCDScroll = GameObject.Find("boostCD").GetComponent<Image>();
        attackCDScroll = GameObject.Find("ataqueCD").GetComponent<Image>();
        bombaCDScroll = GameObject.Find("bombaCD").GetComponent<Image>();
        trampaCDScroll = GameObject.Find("trampaCD").GetComponent<Image>();

        audioSource = gameObject.GetComponent<AudioSource>();

        MR = GameObject.Find("MR").GetComponent<Mr>();
        bombaScript = GameObject.Find("Spawner").GetComponent<Bomba>();
        audioScript = GameObject.Find("Audios").GetComponent<Audios>();

        ataqueNormal = GameObject.Find("ataquenormal");
        ataqueNormal.SetActive(false);
        ataqueDoble = GameObject.Find("ataquedoble");
        ataqueDoble.SetActive(false);
        ataquePolvora = GameObject.Find("ataquepolvora");
        ataquePolvora.SetActive(false);
    }
    
    // Update is called once per frame
    void Update () {
        if (MR.vida > 0)
        {
            atacar();
            boost();
            bomba();
            explosion();
            trampa();
        }
        

    }
    void boost()
    {
        //verifico el cooldown del Boost
        if (boostTempCD <= Time.time)
        {
            isBoostCD = false;
            boostCDScroll.fillAmount = 0;
        }
        else
        {
            isBoostCD = true;
            boostCDScroll.fillAmount = (-(((boostTempCD - Time.time)) - boostCD)) / boostCD;
        }


        // vuelvo a la velocidad normal después del tiempo del boost
        if (boostTempSpeed <= Time.time)
        {
            MR.velocidad = MR.velocidadInicial; 
        }


        //al hace el boost se modifica la velocidad
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (isBoostCD == false)
            {
                MR.velocidad = boostSpeed;
                boostTempSpeed = Time.time + boostTimeSpeed;
                boostTempCD = Time.time + boostCD;
            }

        }
    }
    void atacar()
    {
        if (attackTempCD <= Time.time)
        {
            isAttackCD = false;
            attackCDScroll.fillAmount = 0;
        }
        else
        {
            isAttackCD = true;
            attackCDScroll.fillAmount = (-(((attackTempCD - Time.time)) - attackCD)) / attackCD;
        }


        
        if (attackTempActive <= Time.time)
        {
            ataqueNormal.SetActive(false);
            ataqueDoble.SetActive(false);
            ataquePolvora.SetActive(false);
            

        }
        else
        {
            if (MR.bonus== 0 )
            {
                ataqueNormal.SetActive(true);
                audioSource.clip = audioScript.disparoClip;
                
            }
            else if (MR.bonus == 3)
            {
                ataqueNormal.SetActive(true);
                audioSource.clip = audioScript.disparoRifleClip;

            }
            else if(MR.bonus == 1)
            {
                ataqueDoble.SetActive(true);
                audioSource.clip = audioScript.disparoDobleClip;
            }
            else if (MR.bonus == 2)
            {
                ataquePolvora.SetActive(true);
                audioSource.clip = audioScript.disparoPolvoraClip;

            }
            audioSource.Play();
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isAttackCD == false)
            {
                MR.transform.Translate(0, recoil, 0);
                attackTempActive = Time.time + attackActiveTime;
                attackTempCD = Time.time + attackCD;
                if (MR.bonus == 3)
                {
                    bala();

                }
                
            }
            


        }
    }

    void trampa()
    {
        if (trampaTempCD <= Time.time)
        {
            isTrampaCD = false;
            trampaCDScroll.fillAmount = 0;
        }
        else
        {
            isTrampaCD = true;
            trampaCDScroll.fillAmount = (-(((trampaTempCD - Time.time)) - trampaCD)) / trampaCD;
        }


        if (isTrampaActivaded)
        {
            if (trampaTempActive <= Time.time)
            {
                Destroy(trampaObject);
                isTrampaActivaded = false;
            }
            else
            {
                trampaObject.SetActive(true);

            }
        }
        


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTrampaCD == false)
            {
                Vector3 dir = MR.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
                Instantiate(trampaPrefab, (MR.mouse.transform.position), Quaternion.AngleAxis(angle, Vector3.forward), GameScreen.transform);
                trampaObject = GameObject.Find("trampa(Clone)");
                trampaTempActive = Time.time + trampaActiveTime;
                trampaTempCD = Time.time + trampaCD;
                isTrampaActivaded = true;
                audioSource.clip = audioScript.vallaClip;
                audioSource.Play();

            }



        }
    }
    void bomba()
    {
        if (bombaTempCD <= Time.time)
        {
            isBombaCD = false;
            bombaCDScroll.fillAmount = 0;
        }
        else
        {
            isBombaCD = true;
            bombaCDScroll.fillAmount = (-(((bombaTempCD - Time.time)) - bombaCD)) / bombaCD;
        }

        

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            
            if (isBombaCD == false)
            {
                Instantiate(bombaPrefab, slot.transform.position, slot.transform.rotation, slot.transform);
                bombaObject = GameObject.Find("bomba(Clone)");
                bombaScript.explosion = GameObject.Find("explosion");
                MR.transform.Translate(0, recoil, 0);
                bombaObject.transform.SetParent(null);
                bombaObject.SetActive(true);
                bombaTempCD = Time.time + bombaCD;
                audioSource.clip = audioScript.granadaClip;
                audioSource.Play();
            }



        }
    }
    void bala()
    {
        Instantiate(balaPrefab, slot.transform.position, slot.transform.rotation, slot.transform);
        balaObject = GameObject.Find("bala(Clone)");
        balaObject.transform.SetParent(null);
    }
    public void explosion()
    {
        if (hasExplode)
        {
            if (explosionTempActive <= Time.time)
            {
                
                Destroy(bombaScript.explosion);
                Destroy(bombaObject);
                hasExplode = false;
            }
            else
            {
                bombaScript.explosion.SetActive(true);
                audioSource.clip = audioScript.explosionClip;
                audioSource.Play();

            }

            
        }
        

    }
   


}
