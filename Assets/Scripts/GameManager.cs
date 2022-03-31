using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject winSprite, loseSprite;
    void Update()
    {
        
    }

    public void Win()
    {
        winSprite.SetActive(true);
    }

    public void Lose()
    {
        loseSprite.SetActive(true);
    }
}
