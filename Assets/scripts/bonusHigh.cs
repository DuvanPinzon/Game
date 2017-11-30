using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusHigh : MonoBehaviour
{
    
    float bonusHighActiveTime = 0.4f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("destroyHigh");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator destroyHigh()
    {
        yield return new WaitForSeconds(bonusHighActiveTime);
        Destroy(gameObject);
    }


}
