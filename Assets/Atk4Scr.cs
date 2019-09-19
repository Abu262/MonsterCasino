using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Atk4Scr : MonoBehaviour
{
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
     //   CreateButton();
        pc = ps.GetComponent<Character>();
        ec = es.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void attack4()
    {
        if (moveID != -1)
        {
            StartCoroutine(Attack());
        }
        

    }

    IEnumerator Attack()
    {
        btn.interactable = false;
        mm.PlayerID = 4;
        FindObjectOfType<AudioManager>().Play("Click");
        ps.IsCharacterTurn = false;
        if (ps.Stuntime > 0)
        {

            yield return StartCoroutine(mm.Stunned(pc, ec));

        }


        else
        {
            mm.b1.interactable = false;
            mm.b2.interactable = false;
            mm.b3.interactable = false;
            mm.b4.interactable = false;
            mm.MoveBtn.interactable = false;
            Debug.Log("testing");
            if (ps.Stuntime == 0)
            {
                yield return StartCoroutine(mm.DisplayText(ps.name + " is no longer stunned!"));
                ps.Stuntime = -1;
            }
            yield return StartCoroutine(mm.UseMove(moveID, pc, ec));
        }



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
        yield return null;
    }

    public void CreateButton()
    {
        moveID = ps.MoveId4;
        StartCoroutine(mm.SetBtnText(btn, moveID));

    }
    public void SwapId(int ID)
    {
        ps.MoveId4 = ID;
        CreateButton();
    }
}
