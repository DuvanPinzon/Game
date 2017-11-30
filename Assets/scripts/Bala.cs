using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

    float balaVelocidad = 40;
    float balaActiveTime = 0.5f;

    // Use this for initialization
    void Start () {
        StartCoroutine("destroyBala");
    }
	
	// Update is called once per frame
	void Update () {
        
        if (gameObject.activeSelf)
        {

            transform.Translate(0, balaVelocidad * Time.deltaTime, 0);

        }
    }
    IEnumerator destroyBala()
    {
        yield return new WaitForSeconds(balaActiveTime);
        Destroy(gameObject);
    }


}
