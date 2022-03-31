using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //X Regions:(-4.8,-1.6),(-1.6,1.6),(1.6,4.8)
    //Y Regions:(-4.8,-1.6),(-1.6,1.6),(1.6,4.8)

    //Chess Pos:
    //(-3.2,3.2),(0.0,3.2),(3.2,3.2)
    //(-3.2,0.0),(0.0,0.0),(3.2,0.0)
    //(-3.2,-3.2),(0.0,-3.2),(3.2,-3.2)
    public GameObject winSprite, loseSprite, AIChess, PlayerChess;
    private bool[] playerChessPos = new bool[9];
    private bool[] AIChessPos = new bool[9];
    private bool[] TotalChessPos = new bool[9];
    public Vector3[] ChessPositions;
    private bool isAIFirst = false;
    private bool AIReadyToAction = false;
    [HideInInspector] public bool isGameOver = false;

    //Works only for 3X3 board game
    public int GetIndexFromCoords(Vector2Int coords)
    {
        return coords.y * 3 + coords.x;
    }
    
    //Works only for 3X3 board game
    public Vector2Int GetCoordsFromIndex(int index)
    {
        return new Vector2Int(index % 3, index / 3);
    }

    void Update()
    {
        if (isGameOver) return;
        #region Player Actions
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos.x < -4.5f || mousePos.x > 4.5f || mousePos.y > 4.5f || mousePos.y < -4.5f) return;
            if (mousePos.x > -4.5f && mousePos.x < -1.9f && mousePos.y > -4.5f && mousePos.y < -1.9f)
            {
                if (TotalChessPos[0]) return;
                //Debug.Log("Player placed chess at 0,0");
                Instantiate(PlayerChess, new Vector3(-3.2f, -3.2f, 0.0f), Quaternion.identity, null);
                playerChessPos[0] = true;
                TotalChessPos[0] = true;
            }
            if (mousePos.x > -1.3f && mousePos.x < 1.3f && mousePos.y > -4.5f && mousePos.y < -1.9f)
            {
                if (TotalChessPos[1]) return;
                //Debug.Log("Player placed chess at 1,0");
                Instantiate(PlayerChess, new Vector3(0.0f, -3.2f, 0.0f), Quaternion.identity, null);
                playerChessPos[1] = true;
                TotalChessPos[1] = true;
            }
            if (mousePos.x > 1.9f && mousePos.x < 4.5f && mousePos.y > -4.5f && mousePos.y < -1.9f)
            {
                if (TotalChessPos[2]) return;
                //Debug.Log("Player placed chess at 2,0");
                Instantiate(PlayerChess, new Vector3(3.2f, -3.2f, 0.0f), Quaternion.identity, null);
                playerChessPos[2] = true;
                TotalChessPos[2] = true;
            }
            if (mousePos.x > -4.5f && mousePos.x < -1.9f && mousePos.y > -1.3f && mousePos.y < 1.3f)
            {
                if (TotalChessPos[3]) return;
                //Debug.Log("Player placed chess at 0,1");
                Instantiate(PlayerChess, new Vector3(-3.2f, 0.0f, 0.0f), Quaternion.identity, null);
                playerChessPos[3] = true;
                TotalChessPos[3] = true;
            }
            if (mousePos.x > -1.3f && mousePos.x < 1.3f && mousePos.y > -1.3f && mousePos.y < 1.3f)
            {
                if (TotalChessPos[4]) return;
                //Debug.Log("Player placed chess at 1,1");
                Instantiate(PlayerChess, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, null);
                playerChessPos[4] = true;
                TotalChessPos[4] = true;
            }
            if (mousePos.x > 1.9f && mousePos.x < 4.5f && mousePos.y > -1.3f && mousePos.y < 1.3f)
            {
                if (TotalChessPos[5]) return;
                //Debug.Log("Player placed chess at 2,1");
                Instantiate(PlayerChess, new Vector3(3.2f, 0.0f, 0.0f), Quaternion.identity, null);
                playerChessPos[5] = true;
                TotalChessPos[5] = true;
            }
            if (mousePos.x > -4.5f && mousePos.x < -1.9f && mousePos.y > 1.9f && mousePos.y < 4.5f)
            {
                if (TotalChessPos[6]) return;
                //Debug.Log("Player placed chess at 0,2");
                Instantiate(PlayerChess, new Vector3(-3.2f, 3.2f, 0.0f), Quaternion.identity, null);
                playerChessPos[6] = true;
                TotalChessPos[6] = true;
            }
            if (mousePos.x > -1.3f && mousePos.x < 1.3f && mousePos.y > 1.9f && mousePos.y < 4.5f)
            {
                if (TotalChessPos[7]) return;
                //Debug.Log("Player placed chess at 1,2");
                Instantiate(PlayerChess, new Vector3(0.0f, 3.2f, 0.0f), Quaternion.identity, null);
                playerChessPos[7] = true;
                TotalChessPos[7] = true;
            }
            if (mousePos.x > 1.9f && mousePos.x < 4.5f && mousePos.y > 1.9f && mousePos.y < 4.5f)
            {
                if (TotalChessPos[8]) return;
                //Debug.Log("Player placed chess at 2,2");
                Instantiate(PlayerChess, new Vector3(3.2f, 3.2f, 0.0f), Quaternion.identity, null);
                playerChessPos[8] = true;
                TotalChessPos[8] = true;
            }
            WinningConditionCheck();
            AIReadyToAction = true;
        }
        #endregion
        #region AI Actions
        if (isGameOver) return;
        if (AIReadyToAction)
        {
            AITakeAction();
            AIReadyToAction = false;
        }
        #endregion
    }

    public void AITakeAction()
    {
        int randomIndex;
        while (true)
        {
            randomIndex = Random.Range(0, 8);
            if (!TotalChessPos[randomIndex]) break;
            if (TotalChessPos[0] && TotalChessPos[1] && TotalChessPos[2] &&
                TotalChessPos[3] && TotalChessPos[4] && TotalChessPos[5] &&
                TotalChessPos[6] && TotalChessPos[7] && TotalChessPos[8]) break;
        }
        if (TotalChessPos[randomIndex]) return;
        Debug.Log("AI placed chess at " + GetCoordsFromIndex(randomIndex).x + "," + GetCoordsFromIndex(randomIndex).y);
        Instantiate(AIChess, ChessPositions[randomIndex], Quaternion.identity, null);
        AIChessPos[randomIndex] = true;
        TotalChessPos[randomIndex] = true;
        WinningConditionCheck();
    }

    public void ToggleAIFirst()
    {
        if (isAIFirst) return;
        isAIFirst = true;
        AITakeAction();
    }

    public void WinningConditionCheck()
    {
        //Horizontal Player Winning Condition
        if ((playerChessPos[0] && playerChessPos[1] && playerChessPos[2]) ||
            (playerChessPos[3] && playerChessPos[4] && playerChessPos[5]) ||
            (playerChessPos[6] && playerChessPos[7] && playerChessPos[8]))
        {
            Win();
            return;
        }
        //Horizontal AI Winning Condition
        if ((AIChessPos[0] && AIChessPos[1] && AIChessPos[2]) ||
            (AIChessPos[3] && AIChessPos[4] && AIChessPos[5]) ||
            (AIChessPos[6] && AIChessPos[7] && AIChessPos[8]))
        {
            Lose();
            return;
        }

        //Vertical Player Winning Condition
        if ((playerChessPos[0] && playerChessPos[3] && playerChessPos[6]) ||
            (playerChessPos[1] && playerChessPos[4] && playerChessPos[7]) ||
            (playerChessPos[2] && playerChessPos[5] && playerChessPos[8]))
        {
            Win();
            return;
        }
        //Vertical AI Winning Condition
        if ((AIChessPos[0] && AIChessPos[3] && AIChessPos[6]) ||
            (AIChessPos[1] && AIChessPos[4] && AIChessPos[7]) ||
            (AIChessPos[2] && AIChessPos[5] && AIChessPos[8]))
        {
            Lose();
            return;
        }

        //Diagonal Player Winning Condition
        if ((playerChessPos[0] && playerChessPos[4] && playerChessPos[8]) ||
            (playerChessPos[2] && playerChessPos[4] && playerChessPos[6]))
        {
            Win();
            return;
        }
        //Diagonal AI Winning Condition
        if ((AIChessPos[0] && AIChessPos[4] && AIChessPos[8]) ||
            (AIChessPos[2] && AIChessPos[4] && AIChessPos[6]))
        {
            Lose();
            return;
        }
        if (TotalChessPos[0] && TotalChessPos[1] && TotalChessPos[2] &&
            TotalChessPos[3] && TotalChessPos[4] && TotalChessPos[5] &&
                TotalChessPos[6] && TotalChessPos[7] && TotalChessPos[8])
        {
            Lose();
            return;
        }
    }

    public void Win()
    {
        isGameOver = true;
        winSprite.SetActive(true);
    }

    public void Lose()
    {
        isGameOver = true;
        loseSprite.SetActive(true);
    }
}
