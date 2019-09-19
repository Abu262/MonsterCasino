using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    public TextMeshProUGUI scoretext;
    // Start is called before the first frame update
    void Start()
    {
        scoretext.text ="HIGHSCORE: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
