using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour
{
    public Dropdown dropDownMenu;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadScene(int number)
    {
        StartCoroutine(LoadSceneFunction(number));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneFunction(int number)
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(number);
        yield return new WaitForSeconds(0.7f);
    }

    public void QuickLoad(int number)
    {
        SceneManager.LoadScene(number);
    }

}
