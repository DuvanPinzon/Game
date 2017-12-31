using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour {
     
    public GameObject zombiePrefab;
    public GameObject bigPrefab;
    public GameObject crispPrefab;

    public GameObject bonusDoblePrefab;
    public GameObject bonusPolvoraPrefab;
    public GameObject bonusRiflePrefab;

    GameObject puerta;
    GameObject flechas;

    GameObject GameScreen;
    bool lvlEnd = true;
    bool bossIsAlive = true;

    float timeLeft = 60.0f;
    
    int randomPosition;
    int randomBonus;
    int randomBonusLuck;
    int randomEnemy;
    float enemyFrecuency=0.5f;

    Text contador;

    

    GameObject[] bonusArray;

    Vector3[] positionSpawnArray = new[] {
        new Vector3(-10,7,0), new Vector3(-6, 7, 0), new Vector3(-2, 7, 0),
        new Vector3(2, 7, 0), new Vector3(6, 7, 0), new Vector3(10, 7, 0),
        new Vector3(11, 3, 0), new Vector3(11, 0, 0), new Vector3(11, -3, 0),
        new Vector3(10,-7,0), new Vector3(6, -7, 0), new Vector3(2, -7, 0),
        new Vector3(-2, -7, 0), new Vector3(-6, -7, 0), new Vector3(-10, -7, 0),
        new Vector3(-11, -3, 0), new Vector3(-11, 0, 0), new Vector3(11, 3, 0),};
    // Use this for initialization
    void awake()
    {

    }

    void Start () {

        if (SceneManager.GetActiveScene().name == "Lvl1")
        {
            StartCoroutine("SpawnEnemiesRoutine");
            contador = GameObject.Find("contador").GetComponent<Text>();
        }
        else if(SceneManager.GetActiveScene().name == "boss")
        {
            StartCoroutine("SpawnBonusAuto");
        }
        GameScreen = GameObject.Find("Game");
        flechas = GameObject.Find("flechas");
        puerta = GameObject.Find("puerta");
        bonusArray =new [] { null,bonusDoblePrefab,bonusPolvoraPrefab,bonusRiflePrefab};
        flechas.SetActive(false);
        puerta.SetActive(false);


    }
	
	// Update is called once per frame
	void Update () {
        
        if (SceneManager.GetActiveScene().name == "Lvl1")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                contador.text = "GO!";
                lvlEnd = false;
                flechas.SetActive(true);
                puerta.SetActive(true);
            }

            timeLeft -= Time.deltaTime;
            contador.text = ((int)timeLeft).ToString();
            if (timeLeft < 0)
            {
                contador.text = "GO!";
                lvlEnd = false;
                flechas.SetActive(true);
                puerta.SetActive(true);
            }
        }
        
    }
    IEnumerator SpawnEnemiesRoutine()
    {
        while (lvlEnd)
        {
            if (enemyFrecuency < 0.7)
            {
                enemyFrecuency = enemyFrecuency * 1.02f; //1.05 maybe
            }

            SpawnEnemy();
            
            yield return new WaitForSeconds(1 / enemyFrecuency);

        }

    }

    public void SpawnEnemy()
    {
        randomPosition = Random.Range(0, 18);
        randomEnemy = Random.Range(0, 10);
        if (randomEnemy < 5)
        {
            Instantiate(zombiePrefab, positionSpawnArray[randomPosition], transform.rotation);
        }
        else if (randomEnemy < 8)
        {
            Instantiate(bigPrefab, positionSpawnArray[randomPosition], transform.rotation);
        }
        else if (randomEnemy < 10)
        {
            Instantiate(crispPrefab, positionSpawnArray[randomPosition], transform.rotation);
        }
    }

    public IEnumerator SpawnBonus(Vector3 posicion)
    {
        GameObject bonusInstance;
        //falta hacer que aparezca en el centro al coger bonus (animacion)

        randomBonusLuck = Random.Range(0, 10);
        if (randomBonusLuck <= 1)
        {
            randomBonus = Random.Range(1, 4);
            bonusInstance = Instantiate(bonusArray[randomBonus], posicion, transform.rotation, GameScreen.transform);
            yield return new WaitForSeconds(5);
            Destroy(bonusInstance);
        }

    }

    public IEnumerator SpawnBonusAuto()
    {
        GameObject bonusInstance;
        Vector3 posicion;
        //falta hacer que aparezca en el centro al coger bonus (animacion)
        while (bossIsAlive)
        {
            posicion = new Vector3(Random.Range(-9f, 9f), Random.Range(-5f, 5f), 0);
            yield return new WaitForSeconds(5);
            randomBonus = Random.Range(1, 4);
            bonusInstance = Instantiate(bonusArray[randomBonus], posicion, transform.rotation, GameScreen.transform);
            yield return new WaitForSeconds(5);
            Destroy(bonusInstance);
        }



    }
}
