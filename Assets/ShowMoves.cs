using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShowMoves : MonoBehaviour
{
    //this script lists alll the mvoes the player and opponent has

    public Image MovePic;
    
    public TextMeshProUGUI MovesTxt1;
    public TextMeshProUGUI MovesTxt2;
    public Button BackBtn;
    public Button NextBtn;
    public Button PrevBtn;

    public MoveManager mm;
   // public Image Wall;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMoves()
    {
//        Wall.enabled = true;
        MovePic.enabled = true;
        MovesTxt1.enabled = true;
        BackBtn.interactable = true;
        BackBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        NextBtn.interactable = true;
        NextBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        MovesTxt1.text = "Player moves\n\n" + CreateList(mm.ps.MoveId1) + "\n\n" + CreateList(mm.ps.MoveId2) + "\n\n" + CreateList(mm.ps.MoveId3) + "\n\n" + CreateList(mm.ps.MoveId4);
        MovesTxt2.text = "Enemy moves\n\n" + CreateList(mm.es.MoveId1) + "\n\n" + CreateList(mm.es.MoveId2) + "\n\n" + CreateList(mm.es.MoveId3) + "\n\n" + CreateList(mm.es.MoveId4);



    }

    private string CreateList(int ID)
    {
        string IDtext = " ";
        switch (ID)
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
        return IDtext;
    }

    public void NextMoves()
    {
        MovesTxt1.enabled = false;
        MovesTxt2.enabled = true;
        NextBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        NextBtn.interactable = false;
        PrevBtn.interactable = true;
        PrevBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }
    public void PrevMoves()
    {
        MovesTxt1.enabled = true;
        MovesTxt2.enabled = false;
        NextBtn.interactable = true;
        NextBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        PrevBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        PrevBtn.interactable = false;

    }

    public void CloseMoves()
    {
        //   Wall.enabled = false;
        MovePic.enabled = false;
        MovesTxt1.enabled = false;
        MovesTxt2.enabled = false;
        BackBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        BackBtn.interactable = false;
        NextBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        NextBtn.interactable = false;
        PrevBtn.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        PrevBtn.interactable = false;

    }
}
