using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStat : MonoBehaviour
{
    public EnemyScr Enemy;
    public PlayerScr Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseAtk()
    {

        FindObjectOfType<AudioManager>().Play("Boost");
        Enemy.points -= 1;
        Player.StartAtk += 1;
    }
    public void IncreaseDef()
    {

        FindObjectOfType<AudioManager>().Play("Boost");
        Enemy.points -= 1;
        Player.StartDef += 1;
    }
    public void IncreaseLuck()
    {

        FindObjectOfType<AudioManager>().Play("Boost");
        Enemy.points -= 1;
        Player.StartLuck += 1;
    }
}
