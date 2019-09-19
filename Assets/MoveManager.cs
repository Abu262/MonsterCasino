using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//FUTURE PLANS

//set level up system Still need to add in a way to get a new move
//enemy scaling system  DONE
//set death system
//set player death
//main menu
//art

  


public class MoveManager : MonoBehaviour
{
    public Button MoveBtn;
    public Image MovePic;
    public int PlayerID;
    public TextMeshProUGUI MovesTxt1;
    public TextMeshProUGUI MovesTxt2;
    public Button BackBtn;
    public Button NextBtn;
    public Button PrevBtn;

    public TextMeshProUGUI CONTtxt;

    public PlayerScr ps; //reference to player for direct changes to player stats
    public EnemyScr es;  //reference to enemy for direct changes to enemy stats

    //characters of player and enemy
    Character pc;
    Character ec;

    public bool wait; //a bool to hold the game while things run, may not need this in the end

    public Image Background;   //text box background
    public TextMeshProUGUI Description; //text

    //Player buttons
    public Button b1;
    public Button b2;
    public Button b3;
    public Button b4;

    public Spin trait;
    public Spin element;
    public Spin monster;

    private int backupTraitID;
    private int backupElementID;
    private int backupMonsterID;

    bool waiting;

    //MOVE  REFERENCE GUIDE
    //atk = attack
    //ult = utility
    /*
Hot Streak         1    Flaming       atk rapid attack                        
Flash              2    Electric      ult stun
Freeze the Account 3    Ice           ult block next damage
Buy the Pot        4    Grass         ult heal
Mucker             5    Psychic       ult Chance to dodge damage

All In             6    High Rolling  atk Either high damage or no damage
Chip Swipe         7    Thieving      atk damage and heal self
Reckless Raise     8    Tempermental  atk high damage and hurt self
Duumb Luck         9    Drunken       ??? random heal and hurt
Implied Odds       10   card counting atk change luck

Full House         11   Dragon        ult Boost all stats 
Bad Beat           12   Orc           ult Increase Attack but drop defense and luck
Sweeten the Pot    13   Goblin        ult change attack stat
Bluff              14   Mimic         ult change defense stat
Unfavorable Deck   15   Witch         ult change eveything stat


Jackpot            16   Dragon        atk damage and small heal
Bust               17   Orc           atk Heavy Attack, 
Suck-Out           18   Goblin        atk random chance to do different attacks 
Trap               19   Mimic         atk Use your defense as the attack
Hole Card          20   Witch         atk Use the difference of all stats as your attack
*/


    // Start is called before the first frame update
    void Start()
    {

        MovePic.enabled = false;
        MovesTxt1.enabled = false;
        MovesTxt2.enabled = false;

        BackBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        BackBtn.interactable = false;
        NextBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        NextBtn.interactable = false;
        PrevBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        PrevBtn.interactable = false;


        //get character components
        pc = ps.GetComponent<Character>();
        ec = es.GetComponent<Character>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    public IEnumerator Stunned(Character Attacker, Character Defender)
    {
        //coroutine for when the character is stunned

        //make buttons unclickable
        b1.interactable = false;
        b2.interactable = false;
        b3.interactable = false;
        b4.interactable = false;
        MoveBtn.interactable = false;


        
        //announce the stun
        if (Attacker.Stuntime > 0)
        {
            yield return StartCoroutine(DisplayText(Attacker.name + " is stunned for " + Attacker.Stuntime.ToString() + " more turns!"));
            Attacker.Stuntime -= 1; //decrease the timer

        }

        //next character's turn
        Attacker.IsCharacterTurn = false;
        Defender.IsCharacterTurn = true;
        yield return null;
    }

    public IEnumerator EndStun(Character Attacker, Character Defender)
    {
        //this was giving me grief so i dont use it

        //make buttons unclickable
        b1.interactable = false;
        b2.interactable = false;
        b3.interactable = false;
        b4.interactable = false;
        MoveBtn.interactable = false;



        yield return StartCoroutine(DisplayText(Attacker.name + " is no longer stunned!"));
        Attacker.Stuntime = -1;


        yield return null;
    }

    public IEnumerator UseMove(int ID, Character Attacker, Character Defender)
    {

        //when a move is used by either character,hide and turn off buttons
        //NOTE: we dont actually turn off buttons because they sometimes call this coroutine and that causes issues

        //make buttons unclickable
        b1.interactable = false;
        b2.interactable = false;
        b3.interactable = false;
        b4.interactable = false;
        MoveBtn.interactable = false;



        //with the given id, call a move
        switch (ID)
        {


            case 1:
                yield return StartCoroutine(SetImage(0,0,0));
                yield return StartCoroutine(HotStreak(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 2:
                yield return StartCoroutine(SetImage(1, 1, 1));
                yield return StartCoroutine(Flash(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 3:
                yield return StartCoroutine(SetImage(2, 2, 2));
                yield return StartCoroutine(FreezeTheAccounts(Attacker));
                yield return StartCoroutine(RevertImage());
                break;
            case 4:
                yield return StartCoroutine(SetImage(3, 3, 3));
                yield return StartCoroutine(BuyThePot(Attacker,Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 5:
                yield return StartCoroutine(SetImage(4, 4, 4));
                yield return StartCoroutine(Mucker(Attacker));
                yield return StartCoroutine(RevertImage());
                break;
            case 6:
                yield return StartCoroutine(SetImage(5, 5, 5));
                yield return StartCoroutine(AllIn(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 7:
                yield return StartCoroutine(SetImage(6, 6, 6));
                yield return StartCoroutine(ChipSwipe(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 8:
                yield return StartCoroutine(SetImage(7, 7, 7));
                yield return StartCoroutine(RecklessRaise(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 9:
                yield return StartCoroutine(SetImage(8, 8, 8));
                yield return StartCoroutine(DumbLuck(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 10:
                yield return StartCoroutine(SetImage(9, 9, 9));
                yield return StartCoroutine(PerfectCard(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 16:
                yield return StartCoroutine(SetImage(10, 10, 10));
                yield return StartCoroutine(Jackpot(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 17:
                yield return StartCoroutine(SetImage(11, 11, 11));
                yield return StartCoroutine(Bust(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 13:
                yield return StartCoroutine(SetImage(12, 12, 12));
                yield return StartCoroutine(SweetenThePot(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 14:
                yield return StartCoroutine(SetImage(13, 13, 13));
                yield return StartCoroutine(Bluff(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 15:
                yield return StartCoroutine(SetImage(14, 14, 14));
                yield return StartCoroutine(UnfavorableDeck(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 11:
                yield return StartCoroutine(SetImage(10, 10, 10));
                yield return StartCoroutine(FullHouse(Attacker));
                yield return StartCoroutine(RevertImage());
                break;
            case 12:
                yield return StartCoroutine(SetImage(11, 11, 11));
                yield return StartCoroutine(BadBeat(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 18:
                yield return StartCoroutine(SetImage(12, 12, 12));
                yield return StartCoroutine(SuckOut(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 19:
                yield return StartCoroutine(SetImage(13, 13, 13));
                yield return StartCoroutine(Trap(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 20:
                yield return StartCoroutine(SetImage(14, 14, 14));
                yield return StartCoroutine(HoleCard(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            default:
                break;

      
        }
        yield return (Defender.CheckDead()); //whoever just got hit, check if they are dead
        

        //swap the turn order if not already done
        if  (es.Dead == true)
        {
            //if the enemy died then we set the player to be first
            es.IsCharacterTurn = false;
            ps.IsCharacterTurn = true;
            es.Dead = false;
        }
        else
        {
            //else we just swap the turn order
            Attacker.IsCharacterTurn = false;
            Defender.IsCharacterTurn = true;
        }


    }
    
    public IEnumerator SetBtnText(Button btn, int ID)
    {

        //this sets the text of a button to the ID
        switch (ID)
        {


            case 1:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Hot Streak \n(ATK)";
                break;
            case 2:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Flash \n(STUN)";
                break;
            case 3:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Freeze the Account \n(SHIELD)";
                break;
            case 4:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Buy the Pot \n(HEAL)";
                break;
            case 5:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Mucker \n(DODGE)";
                break;
            case 6:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "All In \n(ATK)";
                break;
            case 7:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Chip Swipe \n(ATK)";
                break;
            case 8:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Reckless Raise \n(ATK)";
                break;
            case 9:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Dumb Luck \n(???)";
                break;
            case 10:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Implied Odds \n(BUFF)";
                break;
            case 16:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Jackpot \n(ATK)";
                break;
            case 17:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Bust \n(ATK)";
                break;
            case 13:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Sweeten the Pot \n(BUFF)";
                break;
            case 14:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Bluff \n(BUFF)";
                break;
            case 15:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Unfavorable Deck \n(DEBUFF)";
                break;
            case 11:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Full House \n(BUFF)";
                break;
            case 12:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Bad Beat \n(BUFF)";
                break;
            case 18:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Suck-Out \n(ATK)";
                break;
            case 19:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Trap \n(ATK)";
                break;
            case 20:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "Hole Card \n(ATK)";
                break;
            default:
                btn.GetComponentInChildren<TextMeshProUGUI>().text = "---";
                break;
        }

        yield return null;
    }

    //here are ALL the attacks



    //flaming (random multi hit) hot streak
    public IEnumerator HotStreak(Character Attacker,Character Defender)
    {
        //calculate damage
        int damage =  CalculateDamage(Attacker.Atk,Defender.Def,100);

        //calculate luck modifier
        int luck = 2 * (Attacker.Luck - Defender.Luck); 

        //roll percent
        int chance = Random.Range(1, 101);

        yield return StartCoroutine(  DisplayText(Attacker.name + " used Hot Streak!" ));

        //first hit
        //after every hit we roll percent again to see if it hits again
        chance = Random.Range(1, 101);
        yield return StartCoroutine(DisplayText("Attack 1!"));
        yield return StartCoroutine(  DealDamage(  Defender,   Attacker,   damage, "Attack 1 hit for " + damage.ToString() + " chips!"));

            //second hit
            if (chance + luck >= 20)
            {
                yield return StartCoroutine(DisplayText("Attack 2!"));
                yield return StartCoroutine(  DealDamage(  Defender,   Attacker,   damage, "Attack 2 hit for " + damage.ToString() + " chips!"));
                chance = Random.Range(1, 101);

                //third hit
                if (chance + luck >= 40)
                {
                    yield return StartCoroutine(DisplayText("Attack 3!"));
                    yield return StartCoroutine(  DealDamage(  Defender,   Attacker,   damage , "Attack 3 hit for " + damage.ToString() + " chips!"));
                    chance = Random.Range(1, 101);

                    //fourth hit
                    if (chance + luck >= 60)
                    {
                        yield return StartCoroutine(DisplayText("Attack 4!"));
                        yield return StartCoroutine(  DealDamage(  Defender,   Attacker,   damage, "Attack 4 hit for " + damage.ToString() + " chips!"));
                        chance = Random.Range(1, 101);

                        //fifth hit
                        if (chance + luck >= 80)
                        {
                            yield return StartCoroutine(DisplayText("Attack 5!"));
                            yield return StartCoroutine(  DealDamage(  Defender,   Attacker,   damage , "Attack 5 hit for " + damage.ToString() + " chips!"));
                        }
                    }
                }
            
            yield return null;
        }

        



    }
    //lightning (stun chance) flash
    public IEnumerator Flash(  Character Attacker,   Character Defender)
    {
      
        //chance to stun
        int chance = Random.Range(1, 101);

        //luck modifier
        int luck = 2 * (Attacker.Luck - Defender.Luck);
        yield return StartCoroutine(  DisplayText(Attacker.name + " used Flash!" ));

        //we beat the roll
        if (chance + luck >= 70)
            {

            yield return StartCoroutine(  DisplayText("It succeeded, " + Defender.name + " is stunned!" ));

            //target stunned
            Defender.Stuntime = 2;
            }

        //we didnt beat the roll
        else
        {
            yield return StartCoroutine(  DisplayText("It failed!" ));
        }
        yield return null;

    }


    //ice (ignores next source of damage) Freeze the Account
    public IEnumerator FreezeTheAccounts(  Character Attacker)
    {

        Attacker.Frozen = true;
        yield return StartCoroutine(  DisplayText(Attacker.name + " used Freeze-the-Accounts" ));
        yield return StartCoroutine(  DisplayText("The next attack will be ignored!" ));

    }


    //grass (heal self) buy the pot
    public IEnumerator BuyThePot(  Character Attacker, Character Defender)
    {
        //calculate heal
        int heal = 15 + 2 * (Attacker.Luck - Defender.Luck); //CalculateDamage(Attacker.Luck, Defender.Luck, 50);
        if (heal < 1)
        {
            heal = 1;
        }
        yield return StartCoroutine(  DisplayText(Attacker.name + " used Buy-the-Pot!" ));

        //heal
        yield return StartCoroutine(Heal(Attacker, heal));

        yield return StartCoroutine(  DisplayText(Attacker.name + " gained " + heal.ToString() + " chips!" ));



    }

    //psychic (chance to dodge the next attack) Mucker 
    public IEnumerator Mucker(  Character Attacker)
    {
        Attacker.Muck = 3;
        yield return StartCoroutine(  DisplayText(Attacker.name + " used Muck!" ));
        yield return StartCoroutine(  DisplayText("For the next 3 attacks, " + Attacker.name + " has a chance to avoid losing chips!" ));

    }



    ///high rolling (strong attack or weak, nothing in middle) all in
    public IEnumerator AllIn(  Character Attacker,  Character Defender)
    {
        int chance = Random.Range(1, 101);
        int luck = 2 * ( Attacker.Luck - Defender.Luck);
        yield return StartCoroutine(  DisplayText(Attacker.name + " used All In!" ));

        if (chance + luck >= 60)
        {
            int damage = CalculateDamage(Attacker.Atk, Defender.Def, 750);
            yield return StartCoroutine(  DealDamage(  Defender,   Attacker,   damage, "It hit for " + damage.ToString() + " chips!"));

        }
        else
        {
            yield return StartCoroutine(  DisplayText("It missed!" ));
        }
        
    }

    //thieving (steal chips) chip swipe
    public IEnumerator ChipSwipe(  Character Attacker,   Character Defender)
    {

        int damage = CalculateDamage(Attacker.Atk, Defender.Def, 250);

        yield return StartCoroutine(  DisplayText(Attacker.name + " used Chip-Swipe!" ));
        yield return StartCoroutine(Heal(Attacker, damage));
        yield return StartCoroutine(DisplayText(Attacker.name + " gained " + damage.ToString() + " chips!"));
        yield return StartCoroutine(DealDamage(Defender, Attacker, damage, Defender.name + " lost " + damage.ToString() + " chips!"));



        yield break;

    }


    //tempermental (high damage, hurt yourself)  Reckless Raise   
    public IEnumerator RecklessRaise(  Character Attacker,   Character Defender)
    {
        int damage = CalculateDamage(Attacker.Atk, Defender.Def, 450);
        int selfdamage = CalculateDamage(Attacker.Atk, Attacker.Def, 450);
        yield return StartCoroutine(  DisplayText(Attacker.name + " used Reckless Raise!" ));
        int AttackerDmg = selfdamage / 2;
        if (Attacker.Chips - AttackerDmg < 1)
        {
            AttackerDmg = (Attacker.Chips - 1);
        }
        yield return StartCoroutine(DealDamage(Attacker, Defender, AttackerDmg, Attacker.name + " lost " + (AttackerDmg).ToString() + " chips!"));
        yield return StartCoroutine(  DealDamage(  Defender,   Attacker,   damage, Defender.name + " lost " + damage.ToString() + " chips!"));
        yield return null;

    }

    //drunken (random damage ammount, random heal) dumb luck
    public IEnumerator DumbLuck(  Character Attacker,   Character Defender)
    {
        yield return StartCoroutine(DisplayText(Attacker.name + " used Dumb Luck!"));

        //get a random move id
        int ID = Random.Range(1, 21);
        switch (ID)
        {


            case 1:
                yield return StartCoroutine(SetImage(0, 0, 0));
                yield return StartCoroutine(HotStreak(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 2:
                yield return StartCoroutine(SetImage(1, 1, 1));
                yield return StartCoroutine(Flash(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 3:
                yield return StartCoroutine(SetImage(2, 2, 2));
                yield return StartCoroutine(FreezeTheAccounts(Attacker));
                yield return StartCoroutine(RevertImage());
                break;
            case 4:
                yield return StartCoroutine(SetImage(3, 3, 3));
                yield return StartCoroutine(BuyThePot(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 5:
                yield return StartCoroutine(SetImage(4, 4, 4));
                yield return StartCoroutine(Mucker(Attacker));
                yield return StartCoroutine(RevertImage());
                break;
            case 6:
                yield return StartCoroutine(SetImage(5, 5, 5));
                yield return StartCoroutine(AllIn(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 7:
                yield return StartCoroutine(SetImage(6, 6, 6));
                yield return StartCoroutine(ChipSwipe(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 8:
                yield return StartCoroutine(SetImage(7, 7, 7));
                yield return StartCoroutine(RecklessRaise(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 9:
                yield return StartCoroutine(SetImage(8, 8, 8));
                yield return StartCoroutine(DumbLuck(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 10:
                yield return StartCoroutine(SetImage(9, 9, 9));
                yield return StartCoroutine(PerfectCard(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 16:
                yield return StartCoroutine(SetImage(10, 10, 10));
                yield return StartCoroutine(Jackpot(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 17:
                yield return StartCoroutine(SetImage(11, 11, 11));
                yield return StartCoroutine(Bust(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 13:
                yield return StartCoroutine(SetImage(12, 12, 12));
                yield return StartCoroutine(SweetenThePot(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 14:
                yield return StartCoroutine(SetImage(13, 13, 13));
                yield return StartCoroutine(Bluff(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 15:
                yield return StartCoroutine(SetImage(14, 14, 14));
                yield return StartCoroutine(UnfavorableDeck(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 11:
                yield return StartCoroutine(SetImage(10, 10, 10));
                yield return StartCoroutine(FullHouse(Attacker));
                yield return StartCoroutine(RevertImage());
                break;
            case 12:
                yield return StartCoroutine(SetImage(11, 11, 11));
                yield return StartCoroutine(BadBeat(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 18:
                yield return StartCoroutine(SetImage(12, 12, 12));
                yield return StartCoroutine(SuckOut(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 19:
                yield return StartCoroutine(SetImage(13, 13, 13));
                yield return StartCoroutine(Trap(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            case 20:
                yield return StartCoroutine(SetImage(14, 14, 14));
                yield return StartCoroutine(HoleCard(Attacker, Defender));
                yield return StartCoroutine(RevertImage());
                break;
            default:
                break;


        }





    }

    //card counting (increase luck, lower enemy luck) Implied Odds
    public IEnumerator PerfectCard(  Character Attacker,   Character Defender)
    {
        int boost = 3 + Attacker.Lvl;

        yield return StartCoroutine(  DisplayText(Attacker.name + " used Implied Odds!" ));

        Attacker.Luck += boost;
        FindObjectOfType<AudioManager>().Play("Boost");
        yield return StartCoroutine(UpdateStats());

        yield return StartCoroutine(  DisplayText(Attacker.name + " gained " + boost.ToString() + " luck!" ));

    }

    //Dragon (Nice hit and small heal) Jackpot 
    public IEnumerator Jackpot(  Character Attacker,   Character Defender)
    {
        yield return StartCoroutine(  DisplayText(Attacker.name + " used Jackpot!" ));

        int damage = CalculateDamage(Attacker.Atk, Defender.Def, 400);
        int heal = damage / 4;
        yield return StartCoroutine(Heal(Attacker, heal));
        yield return StartCoroutine(DisplayText(Attacker.name + " gained " + heal.ToString() + " chips!"));
        yield return StartCoroutine(DealDamage(  Defender,   Attacker,   damage, Defender.name + " lost " + damage.ToString() + " chips!"));       
    }

    //Orc (heavy hit) Bust
    public IEnumerator Bust(  Character Attacker,   Character Defender)
    {
        int damage = CalculateDamage(Attacker.Atk, Defender.Def, 250); ;
        yield return StartCoroutine(DisplayText(Attacker.name + " used Bust!"));
        yield return StartCoroutine(DisplayText("Attack 1!"));
        yield return StartCoroutine(  DealDamage(  Defender,   Attacker,   damage, Defender.name + " lost " + damage.ToString() + " chips!"));
        yield return StartCoroutine(DisplayText("Attack 2!"));
        yield return StartCoroutine(DealDamage(Defender, Attacker, damage, Defender.name + " lost " + damage.ToString() + " chips!"));

    }

    //Mimic (raise Attack) Sweeten the Pot
    public IEnumerator SweetenThePot(  Character Attacker,   Character Defender)
    {
        int boost = 3 + Attacker.Lvl;

        yield return StartCoroutine(  DisplayText(Attacker.name + " used Sweeten the Pot!" ));

        Attacker.Atk += boost;
        FindObjectOfType<AudioManager>().Play("Boost");
        yield return StartCoroutine(UpdateStats());
        yield return StartCoroutine(  DisplayText(Attacker.name + " gained " + boost.ToString() + " attack!" ));
        
    }

    //Goblin (raise defense, lower Enemy Defense) Bluff
    public IEnumerator Bluff(  Character Attacker,   Character Defender)
    {
        int boost = 3 + Attacker.Lvl;
        yield return StartCoroutine(  DisplayText(Attacker.name + " used Bluff!" ));
        Attacker.Def += boost;
        FindObjectOfType<AudioManager>().Play("Boost");
        yield return StartCoroutine(UpdateStats());
        yield return StartCoroutine(  DisplayText(Attacker.name + " gained " + boost.ToString() + " defense!" ));

    }

    //Witch (lower stats) Unfavorable Deck
    public IEnumerator UnfavorableDeck(  Character Attacker,   Character Defender)
    {
        yield return StartCoroutine(  DisplayText(Attacker.name + " used Unfavorable Deck!" ));

        int boost = 1+ Attacker.Lvl;
        if (boost < 1)
        {
            boost = 1;
        }


        if (Defender.Atk - boost > 1)
        {
            Defender.Atk -= boost;
        }
        else
        {
            Defender.Atk = 1;
        }
        FindObjectOfType<AudioManager>().Play("UnBoost");
        yield return StartCoroutine(UpdateStats());
        yield return StartCoroutine(DisplayText(Defender.name + " lost " + boost.ToString() + " Attack!"));

        if (Defender.Def - boost > 1)
        {
            Defender.Def -= boost;
        }
        else
        {
            Defender.Def = 1;
        }
        FindObjectOfType<AudioManager>().Play("UnBoost");
        yield return StartCoroutine(UpdateStats());
        yield return StartCoroutine(DisplayText(Defender.name + " lost " + boost.ToString() + " Defense!"));

        if (Defender.Luck - boost > 1)
        {
            Defender.Luck -= boost;
        }
        else
        {
            Defender.Luck = 1;
        }
        FindObjectOfType<AudioManager>().Play("UnBoost");
        yield return StartCoroutine(UpdateStats());
        yield return StartCoroutine(  DisplayText(Defender.name + " lost " + boost.ToString() + " Luck!" ));
  
    }

    // Full House Dragon Ult Boost all stats
    public IEnumerator FullHouse(  Character Attacker)
    {
        yield return StartCoroutine(  DisplayText(Attacker.name + " used Full House!" ));
        int boost = 1 + Attacker.Lvl;

        Attacker.Atk += boost;
        FindObjectOfType<AudioManager>().Play("Boost");
        yield return StartCoroutine(UpdateStats());
        yield return StartCoroutine(DisplayText(Attacker.name + " gained " + boost.ToString() + " Attack!"));
        Attacker.Def += boost;
        FindObjectOfType<AudioManager>().Play("Boost");
        yield return StartCoroutine(UpdateStats());
        yield return StartCoroutine(DisplayText(Attacker.name + " gained " + boost.ToString() + " Defense!"));
        Attacker.Luck += boost;
        FindObjectOfType<AudioManager>().Play("Boost");
        yield return StartCoroutine(UpdateStats());
        yield return StartCoroutine(  DisplayText(Attacker.name + " gained " + boost.ToString() + " Luck!" ));
    
    }


    //Bad Beat  Orc Ult Increase Attack but drop defense and luck
    public IEnumerator BadBeat(  Character Attacker,   Character Defender)
    {
        yield return StartCoroutine(  DisplayText(Attacker.name + " used Bad Beat!" ));
        int boost = 6 + Attacker.Lvl;
        Attacker.Atk += boost;
        FindObjectOfType<AudioManager>().Play("Boost");
        yield return StartCoroutine(UpdateStats());
        yield return StartCoroutine(  DisplayText(Attacker.name + " gained " + boost.ToString() + " attack!" ));

        if (Attacker.Def - boost/2 < 1)
        {
            Attacker.Def = 1;
        }
        else
        {
            Attacker.Def -= boost / 2;
        }
        FindObjectOfType<AudioManager>().Play("UnBoost");
        yield return StartCoroutine(UpdateStats());
        yield return StartCoroutine(  DisplayText(Attacker.name + " lost " + (boost/2).ToString() + " Defense!" ));
        if (Attacker.Luck - boost/2 < 1)
        {
            Attacker.Luck = 1;
        }
        else
        {
            Attacker.Luck -= boost / 2;
        }
        FindObjectOfType<AudioManager>().Play("UnBoost");
        yield return StartCoroutine(UpdateStats());
        yield return StartCoroutine(  DisplayText(Attacker.name + " lost " + (boost / 2).ToString() + " Luck!" ));

    }


    // Suck-Out Goblin Atk random chance to do different attacks
    public IEnumerator SuckOut(  Character Attacker,   Character Defender)
    {
        int luck =2 * ( Attacker.Luck - Defender.Luck); //CalculateDamage(Attacker.Luck, Defender.Luck, 100);
        int damage = CalculateDamage(Attacker.Atk, Defender.Def, 400);
        int selfdamage = CalculateDamage(Attacker.Atk, Attacker.Def, 400);
        int chance = Random.Range(1, 101);
        yield return  StartCoroutine(DisplayText(Attacker.name + " used Suck Out!" ));
        //1-10 hurt self
        if (chance + luck <= 10)
        {
            if (Attacker.Chips - selfdamage < 1)
            {
                selfdamage = (Attacker.Chips - 1);
            }
            yield return StartCoroutine(DealDamage(Attacker, Defender, selfdamage, Attacker.name + " lost " + selfdamage.ToString() + " chips!"));

        }

        //11-25 hurt self and enemy
        else if (chance + luck > 10 && chance + luck <= 25)
        {
            yield return StartCoroutine(  DealDamage(  Defender,   Attacker,   damage, Defender.name + " lost " + damage.ToString() + " chips!"));

            if (Attacker.Chips - selfdamage < 1)
            {
                selfdamage = (Attacker.Chips - 1);
            }
            yield return StartCoroutine(DealDamage(Attacker, Defender, selfdamage, Attacker.name + " lost " + selfdamage.ToString() + " chips!"));
        }

        //26-90 hurt enemy
        else if (chance + luck > 25 && chance + luck <= 75)
        {
            yield return StartCoroutine(  DealDamage(  Defender,   Attacker,   damage, Defender.name + " lost " + damage.ToString() + " chips!"));

        
        }
        //heal everyone
        else if (chance + luck > 75 && chance + luck <= 90)
        {
            yield return StartCoroutine(Heal(Attacker, damage));
            yield return StartCoroutine(DisplayText(Attacker.name + " gained " + damage.ToString() + " chips!"));
            yield return StartCoroutine(Heal(Defender, damage));
            yield return StartCoroutine(DisplayText(Defender.name + " gained " + damage.ToString() + " chips!"));
        }

        //91-100 hurt enemy and heal
        else if (chance + luck > 90)
        {
            yield return StartCoroutine(Heal(Attacker, damage));
            yield return StartCoroutine(  DisplayText(Attacker.name + " gained " + damage.ToString() + " chips!" ));

            yield return StartCoroutine(  DealDamage(  Defender,   Attacker,   damage, Defender.name + " lost " + damage.ToString() + " chips!"));
        }
    
    }


   // Trap Mimic Atk Use your defense as the attack
   public IEnumerator Trap(  Character Attacker,  Character Defender)
    {
        int damage = CalculateDamage(Attacker.Def, Defender.Def, 400);
        yield return StartCoroutine(DisplayText(Attacker.name + " used Trap!"));
        yield return StartCoroutine(  DealDamage(  Defender,   Attacker,   damage, Defender.name + " lost " + damage.ToString() + " chips!"));

    
    }

        //         Hole Card          20   Witch Atk Use the difference of all stats as your attack
    public IEnumerator HoleCard(  Character Attacker,   Character Defender)
    {
        int damage = CalculateDamage(Attacker.Luck, Defender.Luck, 400);
        yield return StartCoroutine(DisplayText(Attacker.name + " used Hole Card!"));
        yield return StartCoroutine(DealDamage(Defender, Attacker, damage, Defender.name + " lost " + damage.ToString() + " chips!"));
    }



    private IEnumerator Heal(Character Attacker, int heal)
    {
   
        yield return StartCoroutine(RaiseChips(Attacker, heal));
        yield return StartCoroutine(UpdateStats());
    }



    private IEnumerator DealDamage(  Character Defender,   Character Attacker,   int Damage, string s)
    {//deals damage and shows some text

        //if they used freeze the accounts earlier
        if (Defender.Frozen == true)
        {
            Defender.Frozen = false;
            yield return StartCoroutine(  DisplayText(Defender.name + " had their acccount frozen and lost no chips!" ));
            yield return StartCoroutine(  DisplayText(Defender.name + "'s account is no longer frozen!" ));
        }

        //if they mucked earlier
        else if (Defender.Muck > 0)
        {
            int chance = Random.Range(1, 101);
            int luck =2 * ( Attacker.Luck - Defender.Luck);
            if (chance + luck >= 60)
            {
                //no damage
                yield return StartCoroutine(  DisplayText(Defender.name + " mucked and lost no chips!" ));
                
            }
            else
            {
                //it hit normally
                yield return StartCoroutine(LowerChips(Defender, Damage));
                yield return StartCoroutine(UpdateStats());
                yield return StartCoroutine(DisplayText(s));
   
            }
            Defender.Muck -= 1;


        }
        else
        {
            //else we calculate damage normally
            yield return StartCoroutine(LowerChips(Defender, Damage));
            yield return StartCoroutine(UpdateStats());
            yield return StartCoroutine(DisplayText(s));
    
        }

        //announce if the muck worn off
        if (Defender.Muck == 0)
        {
            yield return StartCoroutine(DisplayText(Defender.name + "'s muck has worn off"));
            Defender.Muck = -1;
        }

        yield return StartCoroutine(UpdateStats());

    }

    public IEnumerator DisplayText(string s)
    {//displays text


        // display text box
        Background.enabled = true;
        Description.enabled = true;
        Description.text = s;

        //remove text box on key press
        yield return new WaitForSeconds(0.35f);
        yield return StartCoroutine(waitForKeyPress());

        yield return null;


    }

    private IEnumerator waitForKeyPress()
    {//waits for a button

        waiting = true;
        StartCoroutine(blink()); //blink a continue button
        StartCoroutine(WaitTimer(2f)); //a timer in case the player doesnt press anything
        
        while (waiting)
        {

            if (Input.GetMouseButtonDown(0))
            {

                waiting = false;


            }


            yield return null;
        }
        waiting = false;

        CONTtxt.text = " ";
    }

    private IEnumerator WaitTimer(float t)
    {//timer

        while (waiting && t > 0)
        {
            //when the timer runs out we continue
            t -= Time.deltaTime;

            yield return null;
        }
        waiting = false;
    }


    private IEnumerator blink()
    {//blinks a text box

        while (waiting)
        {
            //gradually blink the word NEXT
            CONTtxt.text = "NEXT";
            yield return new WaitForSeconds(0.5f);
            CONTtxt.text = " ";
            yield return new WaitForSeconds(0.5f);
            
        }
    }

    private IEnumerator UpdateStats()
    {//this updates all the stats

        es.ChipsText.text ="Chips X " + es.Chips.ToString();
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

    private IEnumerator LowerChips(Character Defender, int Damage)
    {
        int i = Damage;
        wait = true;
        StartCoroutine(skip());
        while (i > 0 && wait == true)
        {

            Defender.Chips -= 1;
            i -= 1;
            //play sounds
            FindObjectOfType<AudioManager>().Play("LoseChip");
            yield return StartCoroutine(UpdateStats());
            yield return new WaitForSeconds(0.02f);
            
            if (Defender.Chips <= 0)
            {
                i = 0;
            }
           
        }
        Defender.Chips -= i;
        if (Defender.Chips <= 0)
        {
            Defender.Chips = 0;
        }
        wait = false;
        yield return null;
    }
    private IEnumerator RaiseChips(Character Attacker, int Heal)
    {
        int i = Heal;
        wait = true;
        StartCoroutine(skip());
        while (i > 0 && wait == true)
        {


            Attacker.Chips += 1;
            i -= 1;
            //play sounds
            FindObjectOfType<AudioManager>().Play("HealChip");
            yield return StartCoroutine(UpdateStats());
            yield return new WaitForSeconds(0.02f);
            
       

        }
        Attacker.Chips += i;
        wait = false;
        yield return null;
    }

    private IEnumerator skip()
    {
        wait = true;
        while (wait == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                wait = false;
            }

            yield return null;
        }
        yield return false;



    }


    private IEnumerator SetImage(int elementID, int traitID, int monsterID)
    {
        //sets the enemy sprites
        trait.I.sprite = FindObjectOfType<ArtManager>().sprites[traitID];
        element.I.sprite = FindObjectOfType<ArtManager>().sprites[elementID];
        monster.I.sprite = FindObjectOfType<ArtManager>().sprites[monsterID];

        yield return null;
    }

    private IEnumerator RevertImage()
    {
        //reverts to the monsters orignal sprites
        trait.I.sprite = trait.sprites[trait.SpriteID];
        element.I.sprite = element.sprites[element.SpriteID];
        monster.I.sprite = monster.sprites[monster.SpriteID];

        yield return null;
    }

    private int CalculateDamage(int Atk, int Def, int Power)
    {
        


        float AtkFloat = (float)Atk;
        float DefFloat = (float)Def;
        float PowerFloat = (float)Power;

        // i half ripped this off from pokemon
        return (int)(((PowerFloat * ((AtkFloat + 10f) / (DefFloat + 10f))) / 100f) * 10f);

    }

}



