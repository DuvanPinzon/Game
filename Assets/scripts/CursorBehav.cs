using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehav : MonoBehaviour {

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = new Vector3(0,0,-0.2f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        var delta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if ((transform.position.x + delta.x) < 9 && (transform.position.x + delta.x) > -9){
            if ((transform.position.y + delta.y) < 5 && (transform.position.y + delta.y) > -5)
            {
                delta.Scale(new Vector3(0.5f, 0.5f, 0));
                transform.position += delta;
            }
        }
    }


    


}
