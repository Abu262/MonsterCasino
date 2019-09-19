using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CashOut : MonoBehaviour
{
    public EnemyScr es;
    public PlayerScr ps;
    public MoveManager mm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeepGoing()
    {

        es.choosing = false;

    }

    //buttons can't be coroutines so i did this in two parts
    public void Cash()
    {
        //they clicked the button
        StartCoroutine(Out()); //so we call the coroutine
    }

    private IEnumerator Out()
    {

        yield return StartCoroutine(mm.DisplayText("Thank you for playing, your score is: " + ps.Chips + "!"));

        //set the highschore in the main menu
        if (ps.Chips > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", ps.Chips);

        }
        SceneManager.LoadScene(0);
        yield return null;
    }
}
