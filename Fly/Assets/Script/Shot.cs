using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shot : MonoBehaviour
{
    public Rigidbody animal;
    public Transform cannon;
    public Slider powerSlider;
    public GameManager gm;

    public GameObject EndPanel;

    public float power;

    private float rotateY;
    private float sliderValue;
    private int valueUpDown;

    private Vector3 startPos;

    private bool isSave;

    void Start()
    {
        rotateY = 17;
        sliderValue = 0.3f;
        valueUpDown = 1;

        isSave = false;
    }

    void Update()
    {
        if(!gm.isShot)
        {
            sliderValue += 0.005f * valueUpDown;
            if (sliderValue >= 1 || sliderValue <= 0.3f)
            {
                valueUpDown *= -1;
            }

            powerSlider.value = sliderValue;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                rotateY += 0.1f;

                cannon.transform.localRotation = Quaternion.Euler(0, rotateY, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rotateY -= 0.1f;

                cannon.transform.localRotation = Quaternion.Euler(0, rotateY, 0);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startPos = animal.gameObject.transform.position;
                powerSlider.gameObject.SetActive(false);

                gm.isShot = true;
                float angle = (1 / 90) * rotateY;
                animal.AddForce(new Vector3((1 - angle), angle, 0) * power * sliderValue, ForceMode.Force);
            }
        }
        else
        {
            gm.score = (int)(animal.gameObject.transform.position.x - startPos.x);

            if (animal.velocity.x <= 0.003 && animal.velocity.x >= -0.003)
            {
                Debug.Log("∏ÿ√Ë¥Á");

                User user = FireBaseDatabase.Instance.loginUser;

                if(!isSave)
                {
                    FireBaseDatabase.Instance.writeNewRank(user.ID, user.username, gm.score);
                    isSave = true;
                }

                EndPanel.gameObject.SetActive(true);
                gm.EndScoreText.text = gm.score.ToString();
            }
        }
    }
}
