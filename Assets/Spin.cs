using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spin : MonoBehaviour
{
    public Sprite[] sprites;
    [HideInInspector]
    public int SpriteID;
    public Image I;
    public bool ShouldSpin;
    // Start is called before the first frame update
    private void Awake()
    {
        
    SpriteID = 0;
    }

    void Start()
    {
//        self = GetComponent<Image>();
        ShouldSpin = false;
        
        StartCoroutine(Spinning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator Spinning()
    {
        while (true)
        {
            if (ShouldSpin == true)
            {
                SpriteID = Random.Range(0, 5);
                I.sprite = sprites[SpriteID];
                yield return new WaitForSeconds(0.3f);

            }
            yield return null;
        }

    }

    public void SetImage(int id)
    {
        SpriteID = id;
        Debug.Log(id);
        ShouldSpin = false;
     
        I.sprite = sprites[SpriteID];


//        yield return null;
    }
}
