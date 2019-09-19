using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load(int id)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene(id);
    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://sites.google.com/view/monstercasinoprivacypolicy/home");
    }
}
