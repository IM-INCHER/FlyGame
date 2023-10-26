using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameManager gm;
    public GameObject animal;

    void Start()
    {
        
    }

    void Update()
    {
        if(gm.isShot)
        {
            this.transform.position = new Vector3(animal.transform.position.x, animal.transform.position.y + 15, animal.transform.position.z - 50);
            this.transform.rotation = Quaternion.Euler (20, 2, 0);
        }
    }
}
