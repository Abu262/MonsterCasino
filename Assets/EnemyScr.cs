using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyScr : Character
{
    private int LastMoveID;

    public int points;
    [HideInInspector]
    public bool Dead;

    public Button AtkUp;
    public Button DefUp;
    public Button LuckUp;

    public Spin TraitScr;
    public Spin ElementScr;
    public Spin MonsterScr;

    [HideInInspector]
    public int MoveId1; //id for first move
    [HideInInspector]
    public int MoveId2; //id for second move
    [HideInInspector]
    public int MoveId3; //id for third move
    [HideInInspector]
    public int MoveId4; //id for fourth move
    
    int ElementId;
    int TraitId;
    int MonsterId;
    
    public Button RepBtn1;
    public Button RepBtn2;
    public Button RepBtn3;
    public Button RepBtn4;

    public Button CashOut;
    public Button KeepGoing;




    [HideInInspector]
    public bool choosing;
    [HideInInspector]
    public int NewMoveID;



    public TextMeshProUGUI NameText;
    public TextMeshProUGUI AtkText;
    public TextMeshProUGUI DefText;
    public TextMeshProUGUI ChipsText;
    public TextMeshProUGUI LuckText;


    public PlayerScr ps; //i might not need this
    public EnemyScr es;
    public MoveManager mm;

    Character pc;
    Character ec;

    public Image Background;
    public TextMeshProUGUI Description;
    // Start is called before the first frame update
    void Start()
    {
        LastMoveID = 0;
        Dead = false;
        RepBtn1.gameObject.SetActive(false);
        RepBtn2.gameObject.SetActive(false);
        RepBtn3.gameObject.SetActive(false);
        RepBtn4.gameObject.SetActive(false);

        AtkUp.gameObject.SetActive(false);
        DefUp.gameObject.SetActive(false);
        LuckUp.gameObject.SetActive(false);

        CashOut.gameObject.SetActive(false);
        KeepGoing.gameObject.SetActive(false);
      
        StartCoroutine(CreateEnemy());

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(canAttack());
   
    }

    void CheckStun()
    {
        if (Stuntime > 0)

 
        {
            NameText.text = "STUN";
        }
    }


    //set elements
    void setElement(int id)
    {//different element ids give the monster different moves and stats, same goes for trait and monster id

        if (id == 1)
        {
            Atk += 15;
            Def += 0;
            Luck += 0;
            NameText.text += " Fire ";
            MoveId1 = 1;
        }
        else if (id == 2)
        {
            Atk += 10;
            Def += 0;
            Luck += 5;
            NameText.text += " Thunder ";
            MoveId1 = 2;
        }
        else  if (id == 3)
        {
            Atk += 0;
            Def += 15;
            Luck += 0;
            NameText.text += " Ice ";
            MoveId1 = 3;
        }
        else if (id == 4)
        {
            Atk += 5;
            Def += 5;
            Luck += 5;
            NameText.text += " Grass ";
            MoveId1 = 4;
        }
        else if (id == 5)
        {
            Atk += 0;
            Def += 5;
            Luck += 10;
            NameText.text += " Psychic ";
            MoveId1 = 5;
        }
    }

    //traits
    void setTrait(int id)
    {
        if (id == 6)
        {
            Atk += 5;
            Def += 15;
            Luck += 15;
            NameText.text = "High-Rolling ";
            MoveId2 = 6;
        }
        else if (id == 7)
        {
            Atk += 10;
            Def += 15;
            Luck += 10;
            NameText.text = "Thieving ";
            MoveId2 = 7;
        }
        else if (id == 8)
        {
            Atk += 25;
            Def += 0;
            Luck += 10;
            NameText.text = "Hot-Headed ";
            MoveId2 = 8;
        }
        else if (id == 9)
        {
            Atk += 15;
            Def += 5;
            Luck += 15;
            NameText.text = "Drunken ";
            MoveId2 = 9;
        }
        else if (id == 10)
        {
            Atk += 15;
            Def += 20;
            Luck += 0;
            NameText.text = "Card-Counting ";
            MoveId2 = 10;
        }
    }

    //set monsters
    void setMonster(int id)
    {
        if (id == 11)
        {
            Atk += 20;
            Def += 20;
            Luck += 10;
            NameText.text += " Dragon";
            MoveId3 = 11;
            MoveId4 = 16;
        }
        else if (id == 12)
        {
            Atk += 40;
            Def += 10;
            Luck += 0;
            NameText.text += " Orc";
            MoveId3 = 12;
            MoveId4 = 17;
        }
        else if (id == 13)
        {
            Atk += 10;
            Def += 10;
            Luck += 30;
            NameText.text += " Goblin";
            MoveId3 = 13;
            MoveId4 = 18;
        }
        else if (id == 14)
        {
            Atk += 0;
            Def += 30;
            Luck += 20;
            NameText.text += " Mimic";
            MoveId3 = 14;
            MoveId4 = 19;
        }
        else if (id == 15)
        {
            Atk += 5;
            Def += 5;
            Luck += 40;
            NameText.text += " Witch";
            MoveId3 = 15;
            MoveId4 = 20;
        }
    }

    IEnumerator canAttack()
    {
        //if it's the character's turn
        if (IsCharacterTurn == true)
        {
            mm.b1.interactable = false;
            mm.b2.interactable = false;
            mm.b3.interactable = false;
            mm.b4.interactable = false;
            mm.MoveBtn.interactable = false;
            IsCharacterTurn = false;

            //check the stun
            if (es.Stuntime > 0)
            {
                yield return StartCoroutine(mm.Stunned(ec, pc));
            }

            //else they can attack
            else
            {
                //declare if the stun timer is over
                if (es.Stuntime == 0)
                {
                    yield return StartCoroutine(mm.DisplayText(es.name + " is no longer stunned!"));
                    es.Stuntime = -1;
                }


                //attack with a random move
                int move = Random.Range(1, 5);
                while (move == LastMoveID)
                {
                    move = Random.Range(1, 5);
                }


                if (move == 1)
                {
                    yield return StartCoroutine(mm.UseMove(MoveId1, ec, pc));
                }
                else if (move == 2)
                {
                    yield return StartCoroutine(mm.UseMove(MoveId2, ec, pc));
                }
                else if (move == 3)
                {
                    yield return StartCoroutine(mm.UseMove(MoveId3, ec, pc));

                }
                else if (move == 4)
                {
                    yield return StartCoroutine(mm.UseMove(MoveId4, ec, pc));

                }
                LastMoveID = move; //we save this so they dont do the same move twice in a row
                //            Player.IsCharacterTurn = true;
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
                Background.enabled = false;
               // Description.enabled = false;
                yield return null;
            }
            Description.text += "\nIt is your turn!";
            mm.b1.interactable = true;
            mm.b2.interactable = true;
            mm.b3.interactable = true;
            mm.b4.interactable = true;
            mm.MoveBtn.interactable = true;

            if (mm.PlayerID == 1)
            {
                mm.b1.interactable = false;
            }
            if (mm.PlayerID == 2)
            {
                mm.b2.interactable = false;

            }
            if (mm.PlayerID == 3)
            {
                mm.b3.interactable = false;

            }
            if (mm.PlayerID == 4)
            {
                mm.b4.interactable = false;
            }
            yield return null;
        }

    }

    public override IEnumerator CheckDead()
    {
        mm.b1.interactable = false;
        mm.b2.interactable = false;
        mm.b3.interactable = false;
        mm.b4.interactable = false;
        mm.MoveBtn.interactable = false;

        if (Chips <= 0)
        {
            FindObjectOfType<AudioManager>().Play("Win");
            yield return StartCoroutine(mm.DisplayText(name + " is out of chips!"));
            NameText.text = " ";

            yield return StartCoroutine(LeaveOrStay());
            //start spinning here
            TraitScr.ShouldSpin = true;
            ElementScr.ShouldSpin = true;
            MonsterScr.ShouldSpin = true;
            ps.Lvl += 1;
            es.Lvl += 1;

      

            yield return StartCoroutine(Upgrade(pc));
            ps.Atk = ps.StartAtk;
            ps.Def = ps.StartDef;
            ps.Luck = ps.StartLuck;
            ps.Chips +=  (ps.Lvl * 10);
            ps.Frozen = false;
            ps.Stuntime = -1;
            ps.Muck = -1;

            yield return StartCoroutine(NewSkill());

           //yield return StartCoroutine(WatchAd());

            yield return StartCoroutine(CreateEnemy());
            Dead = true;
            yield return null;
            //end spin and set image
        }

      
        yield return null;
    }

    public IEnumerator CreateEnemy()
    {
        TraitScr.ShouldSpin = false;
        ElementScr.ShouldSpin = false;
        MonsterScr.ShouldSpin = false;
        IsCharacterTurn = false;
        ps.IsCharacterTurn = true;
        wait = false;
        pc = ps.GetComponent<Character>();
        ec = es.GetComponent<Character>();

        //new ids and stats
        ElementId =  Random.Range(1, 6);
        TraitId = Random.Range(6, 11);
        MonsterId =  Random.Range(11, 16);
        Muck = -1;
        Chips = 100 + (ps.Lvl * 10);
        Atk = 0;
        Def = 0;
        Luck = 0;
        Frozen = false;
        setTrait(TraitId);
        setElement(ElementId);
        setMonster(MonsterId);
        float a = (float)Atk * (((float)ps.Lvl * 0.1f) + 0.2f);
        float d = (float)Def * (((float)ps.Lvl * 0.1f) + 0.2f);
        float l = (float)Luck * (((float)ps.Lvl * 0.1f) + 0.2f);
        Atk = (int)a;
        Def = (int)d;
        Luck = (int)l;
        Stuntime = -1;


        AtkText.text = "Atk X " + Atk.ToString();
        DefText.text = "Def X " + Def.ToString();
        LuckText.text = "Luck X " + Luck.ToString();
        ChipsText.text = "Chips X " + Chips.ToString();
        name = NameText.text;

        //declare the monster
        Description.text = "A " + name + " appears!";
        mm.b1.interactable = true;
        mm.b2.interactable = true;
        mm.b3.interactable = true;
        mm.b4.interactable = true;
        Description.text += "\nIt is your turn!";
        mm.MoveBtn.interactable = true;

        if (mm.PlayerID == 1)
        {
            mm.b1.interactable = false;
        }
        if (mm.PlayerID == 2)
        {
            mm.b2.interactable = false;

        }
        if (mm.PlayerID == 3)
        {
            mm.b3.interactable = false;

        }
        if (mm.PlayerID == 4)
        {
            mm.b4.interactable = false;
        }

        //        yield return new WaitForSeconds(1f);
        TraitScr.SetImage(TraitId - 6);

//        yield return new WaitForSeconds(1f);
        ElementScr.SetImage(ElementId - 1);

//        yield return new WaitForSeconds(1f);
        MonsterScr.SetImage(MonsterId - 11);
        yield return null;
    }

    private IEnumerator Upgrade(Character Player)
    {
        AtkUp.gameObject.SetActive(true);
        DefUp.gameObject.SetActive(true);
        LuckUp.gameObject.SetActive(true);
        Description.enabled = true;

        points = 10;

        while (points > 0)
        {
            Description.text = "Choose a stat to upgrade!\nYou have " + points.ToString() + " points left!";
            ps.AtkText.text = "Atk X " + ps.StartAtk.ToString();
            ps.DefText.text = "Def X " + ps.StartDef.ToString();
            ps.LuckText.text = "Luck X " + ps.StartLuck.ToString();
            yield return null;
        }
        ps.AtkText.text = "Atk X " + ps.StartAtk.ToString();
        ps.DefText.text = "Def X " + ps.StartDef.ToString();
        ps.LuckText.text = "Luck X " + ps.StartLuck.ToString();
        AtkUp.gameObject.SetActive(false);
        DefUp.gameObject.SetActive(false);
        LuckUp.gameObject.SetActive(false);
        //Description.enabled = false;
        yield return null;
    }

    private IEnumerator NewSkill()
    {


        NewMoveID = Random.Range(1,21);
        //Debug.Log(NewMoveID);
        while (NewMoveID == ps.MoveId1
            || NewMoveID == ps.MoveId2
            || NewMoveID == ps.MoveId3
            || NewMoveID == ps.MoveId4)
        {

            NewMoveID = Random.Range(1, 21);
          //  Debug.Log(NewMoveID);
            // yield return null;
        }
//        Debug.Log(NewMoveID);
        string IDtext = " ";
        switch (NewMoveID)
        {
            case 1:
                IDtext = "Hot Streak: Hit's multiple times. (most of the time).";
                break;
            case 2:
                IDtext = "Flash: Chance to stun the opponent, preventing them from taking action for 2 moves.";
                break;
            case 3:
                  IDtext = "Freeze the Accounts: The next source of damage is ignored. (Weak against multi-hit moves).";
                break;
            case 4:
                  IDtext = "Buy the Pot: Gain a moderate ammount of chips.";
                break;
            case 5:
                  IDtext = "Mucker: For 3 attacks, there is a chance that you ignore the damage.";
                break;
            case 6:
                  IDtext = "All In: Chance to either deal a heavy ammount of damage, or no damage.";
                break;
            case 7:
                  IDtext = "Chip Swipe: Steal some chips from the opponent!";
                break;
            case 8:
                  IDtext = "Reckless Raise: Deal Heavy damage to the opponent and a small ammount to yourself.";
                break;
            case 9:
                  IDtext = "Dumb Luck: Randomly chooses another random attack. RANDOM!!!";
                break;
            case 10:
                  IDtext = "Implied Odds: Increase your luck.";
                break;
            case 16:
                  IDtext = "Jackpot: Deal a moderate ammount of damage to the opponent and heal yourself a small ammount.";
                break;
            case 17:
                  IDtext = "Bust: Whack the opponent twice.";
                break;
            case 13:
                  IDtext = "Sweeten the Pot: Increase your attack.";
                break;
            case 14:
                  IDtext = "Bluff: Increase your defense;";
                break;
            case 15:
                  IDtext = "Unfavorable Deck: Lower all of your opponents stats (not below 1).";
                break;
            case 11:
                  IDtext = "Full House: Increase all of your stats.";
                break;
            case 12:
                  IDtext = "Bad Beat: Greatly increase attack and lower defense and luck (not below 1).";
                break;
            case 18:
                  IDtext = "Suck-Out: chance to heal or hurt both you and your opponent. Feeling Lucky?";
                break;
            case 19:
                  IDtext = "Trap: Use your defense instead of your attack to determine damage.";
                break;
            case 20:
                  IDtext = "Hole Card: Use youe luck instead of your attack to determine damage.";
                break;

        }

        if (ps.MoveId1 == -1 || ps.MoveId2 == -1 || ps.MoveId3 == -1 || ps.MoveId4 == -1)
        {
            yield return StartCoroutine(mm.DisplayText(IDtext));
            //You learned a new move display text
            if (ps.MoveId1 == -1)
            {
            
                mm.b1.GetComponent<Atk1Scr>().SwapId(NewMoveID);
            }
            else if (ps.MoveId2 == -1)
            {
                mm.b2.GetComponent<Atk2Scr>().SwapId(NewMoveID);
            }
            else if (ps.MoveId3 == -1)
            {
                mm.b3.GetComponent<Atk3Scr>().SwapId(NewMoveID);
            }
            else if (ps.MoveId4 == -1)
            {
                mm.b4.GetComponent<Atk4Scr>().SwapId(NewMoveID);
            }
        }
        else
        {
            choosing = true;
            RepBtn1.gameObject.SetActive(true);
            RepBtn2.gameObject.SetActive(true);
            RepBtn3.gameObject.SetActive(true);
            RepBtn4.gameObject.SetActive(true);
            yield return StartCoroutine(mm.SetBtnText(RepBtn1, mm.b1.GetComponent<Atk1Scr>().moveID));
            yield return StartCoroutine(mm.SetBtnText(RepBtn2, mm.b2.GetComponent<Atk2Scr>().moveID));
            yield return StartCoroutine(mm.SetBtnText(RepBtn3, mm.b3.GetComponent<Atk3Scr>().moveID));
            yield return StartCoroutine(mm.SetBtnText(RepBtn4, mm.b4.GetComponent<Atk4Scr>().moveID));
            IDtext += "\nChoose a move to replace";
            Description.enabled = true;
            Description.text = IDtext;

            while (choosing)
            {

                yield return null;
            }

           // Description.enabled = false ;
            RepBtn1.gameObject.SetActive(false);
            RepBtn2.gameObject.SetActive(false);
            RepBtn3.gameObject.SetActive(false);
            RepBtn4.gameObject.SetActive(false);

            //display text to describe move
            //choose a button to replace
        }


        yield return null;
    }

    private IEnumerator LeaveOrStay()
    {
        choosing = true;
        CashOut.gameObject.SetActive(true);
        KeepGoing.gameObject.SetActive(true);

        Description.text = "Would you like to cash out?";

        while (choosing)
        {

            yield return null;
        }

        CashOut.gameObject.SetActive(false);
        KeepGoing.gameObject.SetActive(false);

        yield return null;
    }

}


