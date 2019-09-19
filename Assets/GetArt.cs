using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetArt : MonoBehaviour
{
    public Image self;
    //public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HI");
        self.sprite = FindObjectOfType<ArtManager>().backdrops[FindObjectOfType<ArtManager>().ArtId];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
