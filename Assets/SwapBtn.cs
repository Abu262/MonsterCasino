using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SwapBtn : MonoBehaviour
{
    public Button btn;
    public EnemyScr es;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //cswaps the button move with the new id, each one relates to a different button
    public void SwapBtn1()
    {
        FindObjectOfType<AudioManager>().Play("Click");

        btn.GetComponent<Atk1Scr>().SwapId(es.NewMoveID);

        es.choosing = false;
    }
    public void SwapBtn2()
    {

        FindObjectOfType<AudioManager>().Play("Click");

        btn.GetComponent<Atk2Scr>().SwapId(es.NewMoveID);
        es.choosing = false;
    }
    public void SwapBtn3()
    {
        FindObjectOfType<AudioManager>().Play("Click");

        btn.GetComponent<Atk3Scr>().SwapId(es.NewMoveID);
        es.choosing = false;
    }
    public void SwapBtn4()
    {
        FindObjectOfType<AudioManager>().Play("Click");

        btn.GetComponent<Atk4Scr>().SwapId(es.NewMoveID);
        es.choosing = false;
    }
    


}
