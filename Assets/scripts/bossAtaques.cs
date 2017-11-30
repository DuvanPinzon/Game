using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAtaques : MonoBehaviour {

    Boss bossScript;
    Spawner spawnerScript;

    public GameObject aoePrefab;
    GameObject aoeObject;
    float aoeTempActive;
    GameObject throwObject;
    GameObject bossSlot;
    public GameObject throwPrefab;
    GameObject bossObject;

    AudioSource audioGeneral;
    Audios audioScript;

    // Use this for initialization
    void Start () {
        bossScript = GameObject.Find("boss").GetComponent<Boss>();
        spawnerScript = GameObject.Find("Spawner").GetComponent<Spawner>();
        bossObject = GameObject.Find("boss");
        bossSlot = GameObject.Find("bossSlot");
        StartCoroutine("power");
        audioScript = GameObject.Find("Audios").GetComponent<Audios>();
        audioGeneral = GameObject.Find("Audios").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        pushMove();
        

    }
    IEnumerator enrage()
    {
        bossScript.GetComponent<Animator>().SetBool("bossIsEnraging", true);
        bossScript.velocidad = 7;
        yield return new WaitForSeconds(1.5f);
        bossScript.velocidad = 5;
        bossScript.GetComponent<Animator>().SetBool("bossIsEnraging", false);
    }
    IEnumerator push()
    {

        bossScript.velocidad = 10;
        bossScript.isPushing = true;
        bossScript.GetComponent<Animator>().SetBool("bossIsPushing", true);
        yield return new WaitForSeconds(1f);
        bossScript.isPushing = false;
        bossScript.GetComponent<Animator>().SetBool("bossIsPushing", false);
        bossScript.velocidad = 5;
    }
    void pushMove()
    {
        if (bossScript.isPushing)
        {
            bossObject.transform.Translate(new Vector3(0, bossScript.velocidad * Time.deltaTime, 0));
        }

    }
    IEnumerator aoe()
    {
        Instantiate(aoePrefab, new Vector3(Random.Range(-9f, 9f), Random.Range(-5f, 5f), 0), new Quaternion(0, 0, 0, 0));
        Instantiate(aoePrefab, new Vector3(Random.Range(-9f, 9f), Random.Range(-5f, 5f), 0), new Quaternion(0, 0, 0, 0));
        bossScript.GetComponent<Animator>().SetBool("bossIsAoeing", true);
        yield return new WaitForSeconds(0.2f);
        bossScript.GetComponent<Animator>().SetBool("bossIsAoeing", false);
    }

    IEnumerator throwB()
    {
        bossScript.GetComponent<Animator>().SetBool("bossIsThrowing", true);
        Instantiate(throwPrefab, bossSlot.transform.position, bossSlot.transform.rotation, bossSlot.transform);
        yield return new WaitForSeconds(0.2f);
        bossScript.GetComponent<Animator>().SetBool("bossIsThrowing", false);
    }
    IEnumerator spawnEnemy()
    {
        bossScript.GetComponent<Animator>().SetBool("bossIsAoeing", true);
        spawnerScript.SpawnEnemy();
        yield return new WaitForSeconds(0.2f);
        bossScript.GetComponent<Animator>().SetBool("bossIsAoeing", false);
    }

    IEnumerator power()
    {
        yield return new WaitForSeconds(Random.Range(2.5f, 4f));
        while (bossScript.alive)
        {
            switch (Random.Range(1, 6))
            {
                case 1:
                    StartCoroutine("enrage");
                    audioGeneral.clip = audioScript.bossEnrageClip;
                    audioGeneral.Play();
                    break;

                case 2:
                    StartCoroutine("aoe");
                    audioGeneral.clip = audioScript.bossSpawnClip;
                    audioGeneral.Play();
                    break;

                case 3:
                    StartCoroutine("push");
                    audioGeneral.clip = audioScript.bossPushClip;
                    audioGeneral.Play();
                    break;

                case 4:
                    StartCoroutine("throwB");
                    audioGeneral.clip = audioScript.bossSpawnClip;
                    audioGeneral.Play();
                    break;

                case 5:
                    StartCoroutine("spawnEnemy");
                    audioGeneral.clip = audioScript.bossSpawnClip;
                    audioGeneral.Play();
                    break;

            }
            yield return new WaitForSeconds(Random.Range(2.5f, 4f));
        }
        
    }
}
