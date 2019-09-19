using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMusic : MonoBehaviour
{
    int ID;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewSong()
    {
        if (FindObjectOfType<AudioManager>().ID + 1 > 9)
        {
            FindObjectOfType<AudioManager>().ID = 7;
        }
        else
        {
            FindObjectOfType<AudioManager>().ID += 1;
        }
        FindObjectOfType<AudioManager>().sounds[7].source.Stop();
        FindObjectOfType<AudioManager>().sounds[8].source.Stop();
        FindObjectOfType<AudioManager>().sounds[9].source.Stop();
        if (FindObjectOfType<AudioManager>().ID == 7)
        {
            FindObjectOfType<AudioManager>().Play("Anime");
        }
        else if (FindObjectOfType<AudioManager>().ID == 8)
        {
            FindObjectOfType<AudioManager>().Play("Retro");
        }
        else if (FindObjectOfType<AudioManager>().ID == 9)
        {
            FindObjectOfType<AudioManager>().Play("Trance");
        }
    }
}
