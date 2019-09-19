using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Atk1Scr : MonoBehaviour
{
    //This script is the same as the other three Atc#Scr scripts, 
    //in hindsight i could've made them all one script, 
    //but this game is already published so i'll leave it be
    //just know that i will only comment this one script

    public MoveManager mm;
    public PlayerScr ps;
    public EnemyScr es;

    public int moveID;
    public Button btn;

    Character pc;
    Character ec;
    
    // Start is called before the first frame update
    void Start()
    {
      //  CreateButton();
        pc = ps.GetComponent<Character>();
        ec = es.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attack1()
    {
        //as long as the ID exists
        if (moveID != -1)
        {
            StartCoroutine(Attack());
        }

    }
    
    IEnumerator Attack()
    {
        //disable this button
        btn.interactable = false;
        mm.PlayerID = 1;

        FindObjectOfType<AudioManager>().Play("Click");

        //no longer player's turn
        ps.IsCharacterTurn = false;

        //if they're stunned
        if (ps.Stuntime > 0)
        {
            yield return StartCoroutine(mm.Stunned(pc, ec));

        }

        else
        {
            //declare the stun is worn off
            if (ps.Stuntime == 0)
            {
                mm.b1.interactable = false;
                mm.b2.interactable = false;
                mm.b3.interactable = false;
                mm.b4.interactable = false;
                mm.MoveBtn.interactable = false;
                yield return StartCoroutine(mm.DisplayText(ps.name + " is no longer stunned!"));
                ps.Stuntime = -1;
            }

            //use the move
            yield return StartCoroutine(mm.UseMove(moveID, pc, ec));
        }


        //update all the stats
        es.ChipsText.text = "Chips X " + es.Chips.ToString();
        es.AtkText.text = "Atk X " + es.Atk.ToString();
        es.DefText.text = "Def X " + es.Def.ToString();
        // es.LvlText.text = "Lvl X " + es.Lvl.ToString();
        es.LuckText.text = "Luck X " + es.Luck.ToString();

        ps.ChipsText.text = "Chips X " + ps.Chips.ToString();
        ps.AtkText.text = "Atk X " + ps.Atk.ToString();
        ps.DefText.text = "Def X " + ps.Def.ToString();
        ps.LvlText.text = "Lvl X " + ps.Lvl.ToString();
        ps.LuckText.text = "Luck X " + ps.Luck.ToString();
        //////////

        yield return null;
    }

    public void CreateButton()
    {
        //set the move id to the given player move id
        moveID = ps.MoveId1;
        StartCoroutine(mm.SetBtnText(btn, moveID));
    }

    public void SwapId(int ID)
    {
        //get the new id
        ps.MoveId1 = ID;
        CreateButton();
    }
}
