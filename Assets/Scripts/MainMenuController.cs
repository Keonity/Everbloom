using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene("Merry Meadows");
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
