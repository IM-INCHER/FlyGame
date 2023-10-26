using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    public Rigidbody animal;
    public Transform cannon;
    public Slider powerSlider;
    public GameManager gm;

    public float power;

    private float rotateY;
    private float sliderValue;
    private int valueUpDown;

    void Start()
    {
        rotateY = 17;
        sliderValue = 0.3f;
        valueUpDown = 1;
    }

    void Update()
    {
        if(!gm.isShot)
        {
            sliderValue += 0.008f * valueUpDown;
            if (sliderValue >= 1 || sliderValue <= 0.3f)
            {
                valueUpDown *= -1;
            }

            powerSlider.value = sliderValue;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                rotateY += 0.3f;

                cannon.transform.localRotation = Quaternion.Euler(0, rotateY, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rotateY -= 0.3f;

                cannon.transform.localRotation = Quaternion.Euler(0, rotateY, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gm.isShot = true;
                float angle = (1 / 90) * rotateY;
                animal.AddForce(new Vector3((1 - angle), angle, 0) * power * sliderValue, ForceMode.Force);
            }
        }
    }
}
