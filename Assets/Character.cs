using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "Character", menuName = "Character")]
public abstract class Character : MonoBehaviour
{
    public int Atk; //attack damage
    public int Def; //defense
    public int Luck;
    public int Lvl; //level
    public bool IsCharacterTurn = false;
    public string name;
    public int Chips;
    public bool wait;
    public int Stuntime = -1;
    public int StartAtk;
    public int StartDef;
    public int StartLuck;
    
    public bool Frozen = false;
    //public int curse = 0;
    public int Muck = -1;
    //public int cardcount = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual IEnumerator CheckDead()
    {
        yield return null;
    }
}
