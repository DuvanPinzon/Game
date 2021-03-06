﻿using UnityEngine;
using System.Collections;

public class Crisp : MonoBehaviour {
    Ataques ataqueScript;
    Mr MR;
    Spawner spawnerScript;

    int vida=1;
    float velocidad = 6;
    bool alive=true;
    bool trapped = false;
    public int daño=2;
    float masa = 0.5f;

    bool balazo=false;


    AudioSource audioSource;



    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        ataqueScript = GameObject.Find("ataqueScript").GetComponent<Ataques>();
        MR = GameObject.Find("MR").GetComponent<Mr>();
        spawnerScript = GameObject.Find("Spawner").GetComponent<Spawner>();
    }
	
	// Update is called once per frame
	void Update () {

        StartCoroutine("isAlive");
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
            
            gameObject.GetComponent<Animator>().SetBool("crispIsDead", true);
            yield return new WaitForSeconds(1f);

            spawnerScript.StartCoroutine("SpawnBonus",transform.position);
            //desaparezco
            Destroy(gameObject);

            
            

        }
        
    }

    IEnumerator isAttacking()
    {

        gameObject.GetComponent<Animator>().SetBool("crispIsAttacking", true);
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        vida = 0;


    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //miro que ataque hicieron
        if (coll.gameObject.name == "ataquenormal") 
        {
            transform.Translate(0,ataqueScript.ataqueNormalPush/masa, 0);
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
        if(coll.gameObject.name == "MR")
        {
            StartCoroutine("isAttacking");

        }

    }

    void move()
    {
       if(alive)
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
        StartCoroutine("parpadearSprite");
    }
    IEnumerator parpadearSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }
    
}
