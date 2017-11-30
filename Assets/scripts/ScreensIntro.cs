using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreensIntro : MonoBehaviour {

    int estado=0;
    GameObject imagen;
    public Sprite screen0;
    public Sprite screen1;
    public Sprite screen2;
	// Use this for initialization
	void Start () {
        imagen = GameObject.Find("image");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            estado++;
        }
        if (estado == 1)
        {
            imagen.GetComponent<SpriteRenderer>().sprite = screen1;
        }
        if (estado == 2)
        {
            imagen.GetComponent<SpriteRenderer>().sprite = screen2;
        }
        if (estado == 3)
        {
            SceneManager.LoadScene("Lvl1");
        }
    }
}
