using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

    float ThrowVelocidad = 15;
    float ThrowActiveTime = 2f;
    GameObject spriteRotate;
    public int daño = 1;

    // Use this for initialization
    void Start () {
        StartCoroutine("destroyThrow");
        spriteRotate = GameObject.Find("spriteRotate");
    }
	
	// Update is called once per frame
	void Update () {
        
        if (gameObject.activeSelf)
        {

            transform.Translate(0, ThrowVelocidad * Time.deltaTime, 0);
            spriteRotate.transform.Rotate(0, 0, 10);
            transform.SetParent(null);
        }
    }
    IEnumerator destroyThrow()
    {
        yield return new WaitForSeconds(ThrowActiveTime);
        Destroy(gameObject);
    }


}
