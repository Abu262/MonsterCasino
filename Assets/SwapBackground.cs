using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapBackground : MonoBehaviour
{
    public Image Background;
    //public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        Background.sprite = FindObjectOfType<ArtManager>().menus[FindObjectOfType<ArtManager>().ArtId];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapArt()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        if (FindObjectOfType<ArtManager>().ArtId + 1 > 3)
        {
            FindObjectOfType<ArtManager>().ArtId = 0;
        }
        else
        {
            FindObjectOfType<ArtManager>().ArtId += 1;
        }
        Background.sprite = FindObjectOfType<ArtManager>().menus[FindObjectOfType<ArtManager>().ArtId];
    }
}
