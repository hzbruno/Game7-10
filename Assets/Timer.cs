using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Image img;
    public float timeRemaining = 180;
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
               
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(2);
        }
        txt.text = (int)timeRemaining+" Segundos Restantes";
    }
}
