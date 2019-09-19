using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerScr : Character
{

  /*  public int Atk; //attack damage
    public int Def; //defense
    public int luck;
    public int lvl; //level*/
    [HideInInspector]
    public int MoveId1; //id for first move
    [HideInInspector]
    public int MoveId2; //id for second move
    [HideInInspector]
    public int MoveId3; //id for third move
    [HideInInspector]
    public int MoveId4; //id for fourth move
    public TextMeshProUGUI AtkText;
    public TextMeshProUGUI DefText;
    public TextMeshProUGUI LvlText;
    public TextMeshProUGUI ChipsText;
    public TextMeshProUGUI LuckText;
    
    /* public int chips;

     public int stuntime;

     public bool frozen = false;*/

    // public GameObject Enemy; //i might not need this
    public EnemyScr es;
    public MoveManager mm;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetMoves());
        //MoveId1 = Random.Range(1, 6);
        //MoveId2 = Random.Range(6, 11);
        //MoveId3 = Random.Range(11, 16);
        //MoveId4 = Random.Range(16, 21);
        name = "The Player";
        //    es = Enemy.GetComponent<EnemyScr>();
        //we are going to want to start the player off with two random moves, one to attack, one to defend
        Stuntime = -1;
        Muck = -1;
        IsCharacterTurn = true;
        StartAtk = 10;
        StartDef = 10;
        StartLuck = 10;
        AtkText.text = "Atk X " + Atk.ToString();
        DefText.text = "Def X " + Def.ToString();
        LuckText.text = "Luck X " + Luck.ToString();
        LvlText.text = "Lvl X " + Lvl.ToString();
        ChipsText.text = "Chips X " + Chips.ToString();
        //   MoveId1 = 0; //id for first move
        // MoveId2 = 0; //id for second move
        // MoveId3 = 0; //id for third move
        // MoveId4 = 0; //id for fourth move
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator CheckDead()
    {
        // i do this alot because i really dont want the player hitting buttons when they shouldn't be
        mm.b1.interactable = false;
        mm.b2.interactable = false;
        mm.b3.interactable = false;
        mm.b4.interactable = false;
        mm.MoveBtn.interactable = false;


        //they died
        if (Chips <= 0)
        {
            FindObjectOfType<AudioManager>().Play("Lose");
            yield return StartCoroutine(mm.DisplayText(name + " is out of chips!"));
            yield return StartCoroutine(mm.DisplayText("Game Over!"));
            mm.PlayerID = 0;
            SceneManager.LoadScene(0);
            
        }

        yield return null;
    }

    private IEnumerator SetMoves()
    {
        //when the game first starts this is called

        //get move id's
        MoveId1 = Random.Range(1, 6);
        MoveId2 = Random.Range(16, 21);
        MoveId3 = Random.Range(11, 16);
        MoveId4 = Random.Range(6, 11);

        //set the buttons to the id's
        mm.b1.GetComponent<Atk1Scr>().CreateButton();
        mm.b2.GetComponent<Atk2Scr>().CreateButton();
        mm.b3.GetComponent<Atk3Scr>().CreateButton();
        mm.b4.GetComponent<Atk4Scr>().CreateButton();

        yield return null;
    }
}
