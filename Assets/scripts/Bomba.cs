using UnityEngine;
using System.Collections;

public class Bomba : MonoBehaviour {

    public GameObject explosion;
    
    Ataques ataqueScript;

    float bombaVelocidad = 30;
    float explosionActiveTime = 0.05f;
    // Use this for initialization

    
    void Start () {
        ataqueScript = GameObject.Find("ataqueScript").GetComponent<Ataques>();
        explosion = GameObject.Find("explosion");
        explosion.GetComponent<Collider2D>().enabled = false;
        explosion.GetComponent<Collider2D>().enabled = true;
        explosion.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.activeSelf)
        { 
            
            transform.Translate(0, bombaVelocidad * Time.deltaTime, 0);
            
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        explosion.SetActive(true);
        explosion.transform.SetParent(null);
        gameObject.SetActive(false);
        ataqueScript.hasExplode = true;
        ataqueScript.explosionTempActive = Time.time + explosionActiveTime;

    }
}
