using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aoe : MonoBehaviour {
    
    float AoeActiveTime = 3.5f;
    public int daño = 1;
    // Use this for initialization


    void Start()
    {
        StartCoroutine("destroyAoe");
        transform.SetParent(null);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator destroyAoe()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Animator>().SetBool("AoeExplode", true);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        yield return new WaitForSeconds(AoeActiveTime);
        Destroy(gameObject);
    }
}
