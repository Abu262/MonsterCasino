using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtManager : MonoBehaviour
{
    public int ArtId;
    public static ArtManager instance;
    public Sprite[] sprites;
    public Sprite[] menus;
    public Sprite[] backdrops;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        ArtId = 0;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetArtID()
    {
        return ArtId;
    }

}
