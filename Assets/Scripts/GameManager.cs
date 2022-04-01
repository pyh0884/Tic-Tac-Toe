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
    public enum WinningValue
    {
        AIWin,
        PlayerWin,
        PlayerAdvantage,
        AIAdvantage,
        DeadCell,
        Empty,
    }

    public enum CellStatus
    {
        Empty,
        AI,
        Player,
    }

    public GameObject winSprite, loseSprite, drawSprite, AIChess, PlayerChess;
    private bool[] playerChessPos = new bool[9];
    private bool[] AIChessPos = new bool[9];
    private CellStatus[] TotalChessPos = new CellStatus[9];
    private WinningValue[] WinningValues = new WinningValue[9];
    public Vector3[] ChessPositions;
    private bool isAIFirst = false;
    private bool AIReadyToAction = false;
    [HideInInspector] public bool isGameOver = false;
    public int AIDifficultyLevel = 0;

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
                if (TotalChessPos[0] != CellStatus.Empty) return;
                Debug.Log("Player placed chess at 0,0");
                Instantiate(PlayerChess, new Vector3(-3.2f, -3.2f, 0.0f), Quaternion.identity, null);
                playerChessPos[0] = true;
                TotalChessPos[0] = CellStatus.Player;
            }
            if (mousePos.x > -1.3f && mousePos.x < 1.3f && mousePos.y > -4.5f && mousePos.y < -1.9f)
            {
                if (TotalChessPos[1] != CellStatus.Empty) return;
                Debug.Log("Player placed chess at 1,0");
                Instantiate(PlayerChess, new Vector3(0.0f, -3.2f, 0.0f), Quaternion.identity, null);
                playerChessPos[1] = true;
                TotalChessPos[1] = CellStatus.Player;
            }
            if (mousePos.x > 1.9f && mousePos.x < 4.5f && mousePos.y > -4.5f && mousePos.y < -1.9f)
            {
                if (TotalChessPos[2] != CellStatus.Empty) return;
                Debug.Log("Player placed chess at 2,0");
                Instantiate(PlayerChess, new Vector3(3.2f, -3.2f, 0.0f), Quaternion.identity, null);
                playerChessPos[2] = true;
                TotalChessPos[2] = CellStatus.Player;
            }
            if (mousePos.x > -4.5f && mousePos.x < -1.9f && mousePos.y > -1.3f && mousePos.y < 1.3f)
            {
                if (TotalChessPos[3] != CellStatus.Empty) return;
                Debug.Log("Player placed chess at 0,1");
                Instantiate(PlayerChess, new Vector3(-3.2f, 0.0f, 0.0f), Quaternion.identity, null);
                playerChessPos[3] = true;
                TotalChessPos[3] = CellStatus.Player;
            }
            if (mousePos.x > -1.3f && mousePos.x < 1.3f && mousePos.y > -1.3f && mousePos.y < 1.3f)
            {
                if (TotalChessPos[4] != CellStatus.Empty) return;
                Debug.Log("Player placed chess at 1,1");
                Instantiate(PlayerChess, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, null);
                playerChessPos[4] = true;
                TotalChessPos[4] = CellStatus.Player;
            }
            if (mousePos.x > 1.9f && mousePos.x < 4.5f && mousePos.y > -1.3f && mousePos.y < 1.3f)
            {
                if (TotalChessPos[5] != CellStatus.Empty) return;
                Debug.Log("Player placed chess at 2,1");
                Instantiate(PlayerChess, new Vector3(3.2f, 0.0f, 0.0f), Quaternion.identity, null);
                playerChessPos[5] = true;
                TotalChessPos[5] = CellStatus.Player;
            }
            if (mousePos.x > -4.5f && mousePos.x < -1.9f && mousePos.y > 1.9f && mousePos.y < 4.5f)
            {
                if (TotalChessPos[6] != CellStatus.Empty) return;
                Debug.Log("Player placed chess at 0,2");
                Instantiate(PlayerChess, new Vector3(-3.2f, 3.2f, 0.0f), Quaternion.identity, null);
                playerChessPos[6] = true;
                TotalChessPos[6] = CellStatus.Player;
            }
            if (mousePos.x > -1.3f && mousePos.x < 1.3f && mousePos.y > 1.9f && mousePos.y < 4.5f)
            {
                if (TotalChessPos[7] != CellStatus.Empty) return;
                Debug.Log("Player placed chess at 1,2");
                Instantiate(PlayerChess, new Vector3(0.0f, 3.2f, 0.0f), Quaternion.identity, null);
                playerChessPos[7] = true;
                TotalChessPos[7] = CellStatus.Player;
            }
            if (mousePos.x > 1.9f && mousePos.x < 4.5f && mousePos.y > 1.9f && mousePos.y < 4.5f)
            {
                if (TotalChessPos[8] != CellStatus.Empty) return;
                Debug.Log("Player placed chess at 2,2");
                Instantiate(PlayerChess, new Vector3(3.2f, 3.2f, 0.0f), Quaternion.identity, null);
                playerChessPos[8] = true;
                TotalChessPos[8] = CellStatus.Player;
            }
            WinningConditionCheck();
            WinningValueCalculate();
            AIReadyToAction = true;
        }
        #endregion
        #region AI Actions
        if (isGameOver) return;
        if (AIReadyToAction)
        {
            if (AIDifficultyLevel == 1)
            {
                AITakeAction();
            }
            else
            {
                HardAITakeAction();
            }
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
            if (TotalChessPos[randomIndex] == CellStatus.Empty) break;
            if (TotalChessPos[0] != CellStatus.Empty && TotalChessPos[1] != CellStatus.Empty&&
                TotalChessPos[2] != CellStatus.Empty && TotalChessPos[3] != CellStatus.Empty &&
                TotalChessPos[4] != CellStatus.Empty && TotalChessPos[5] != CellStatus.Empty &&
                TotalChessPos[6] != CellStatus.Empty && TotalChessPos[7] != CellStatus.Empty &&
                TotalChessPos[8] != CellStatus.Empty) break;
        }
        if (TotalChessPos[randomIndex] != CellStatus.Empty) return;
        Debug.Log("AI placed chess at " + GetCoordsFromIndex(randomIndex).x + "," + GetCoordsFromIndex(randomIndex).y);
        Instantiate(AIChess, ChessPositions[randomIndex], Quaternion.identity, null);
        AIChessPos[randomIndex] = true;
        TotalChessPos[randomIndex] = CellStatus.AI;
        WinningConditionCheck();
    }

    public void HardAITakeAction()
    {
        int targetIndex = 0;
        WinningValue tempValue = WinningValue.Empty;
        for (int index = 0; index < WinningValues.Length; ++index)
        {
            if (TotalChessPos[index] != CellStatus.Empty) continue;
            if (WinningValues[index] < tempValue)
            {
                tempValue = WinningValues[index];
                targetIndex = index;
            }
        }
        Debug.Log("AI placed chess at " + GetCoordsFromIndex(targetIndex).x + "," + GetCoordsFromIndex(targetIndex).y);
        Instantiate(AIChess, ChessPositions[targetIndex], Quaternion.identity, null);
        AIChessPos[targetIndex] = true;
        TotalChessPos[targetIndex] = CellStatus.AI;
        WinningConditionCheck();
    }

    public void ToggleAIFirst()
    {
        if (isAIFirst) return;
        isAIFirst = true;
        AITakeAction();
    }

    public void ChangeAIDifficulty()
    {
        PlayerHUD hud = FindObjectOfType<PlayerHUD>();
        AIDifficultyLevel = hud.dropDownMenu.value;
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
        if (TotalChessPos[0] != CellStatus.Empty && TotalChessPos[1] != CellStatus.Empty &&
            TotalChessPos[2] != CellStatus.Empty && TotalChessPos[3] != CellStatus.Empty &&
            TotalChessPos[4] != CellStatus.Empty && TotalChessPos[5] != CellStatus.Empty &&
            TotalChessPos[6] != CellStatus.Empty && TotalChessPos[7] != CellStatus.Empty &&
            TotalChessPos[8] != CellStatus.Empty)
        {
            Draw();
            return;
        }
    }

    public void WinningValueCalculate()
    {
        Vector2Int coords;
        WinningValue horizontalValue, verticalValue, diagonalValue, tempValue;
        for (int index = 0; index < WinningValues.Length; ++index) 
        {
            WinningValues[index] = WinningValue.Empty;
            coords = GetCoordsFromIndex(index);
            horizontalValue = CheckHorizontalValue(coords);
            verticalValue = CheckVerticalValue(coords);
            diagonalValue = CheckDiagonalValue(coords);
            tempValue = horizontalValue < verticalValue ? horizontalValue : verticalValue;
            tempValue = tempValue < diagonalValue ? tempValue : diagonalValue;
            WinningValues[index] = tempValue;
        }
    }

    private WinningValue CheckHorizontalValue(Vector2Int coords)
    {
        int neighbourAIndex = 0;
        int neighbourBIndex = 0;
        int index = GetIndexFromCoords(coords);
        switch (coords.x)
        {
            case 0:
                neighbourAIndex = index + 1;
                neighbourBIndex = index + 2;
                break;
            case 1:
                neighbourAIndex = index + 1;
                neighbourBIndex = index - 1;
                break;
            case 2:
                neighbourAIndex = index - 1;
                neighbourBIndex = index - 2;
                break;
        }
        return CheckWinningValue(TotalChessPos[neighbourAIndex], TotalChessPos[neighbourBIndex]);
    }

    private WinningValue CheckVerticalValue(Vector2Int coords)
    {
        int neighbourAIndex = 0;
        int neighbourBIndex = 0;
        int index = GetIndexFromCoords(coords);
        switch (coords.y)
        {
            case 0:
                neighbourAIndex = index + 3;
                neighbourBIndex = index + 6;
                break;
            case 1:
                neighbourAIndex = index + 3;
                neighbourBIndex = index - 3;
                break;
            case 2:
                neighbourAIndex = index - 3;
                neighbourBIndex = index - 6;
                break;
        }
        return CheckWinningValue(TotalChessPos[neighbourAIndex], TotalChessPos[neighbourBIndex]);
    }

    private WinningValue CheckDiagonalValue(Vector2Int coords)
    {
        int index = GetIndexFromCoords(coords);
        switch (index)
        {
            case 0:
                return CheckWinningValue(TotalChessPos[4], TotalChessPos[8]);
            case 2:
                return CheckWinningValue(TotalChessPos[4], TotalChessPos[6]);
            case 4:
                WinningValue tempValue;
                WinningValue tempValue2;
                tempValue = CheckWinningValue(TotalChessPos[2], TotalChessPos[6]);
                tempValue2 = CheckWinningValue(TotalChessPos[0], TotalChessPos[8]);
                return tempValue < tempValue2 ? tempValue : tempValue2;
            case 6:
                return CheckWinningValue(TotalChessPos[2], TotalChessPos[4]);
            case 8:
                return CheckWinningValue(TotalChessPos[0], TotalChessPos[4]);
            default:
                return WinningValue.Empty;
        }
    }

    private WinningValue CheckWinningValue(CellStatus AStatus, CellStatus BStatus)
    {
        if (AStatus == CellStatus.AI && BStatus == CellStatus.AI)
        {
            return WinningValue.AIWin;
        }
        if (AStatus == CellStatus.Player && BStatus == CellStatus.Player)
        {
            return WinningValue.PlayerWin;
        }
        if ((AStatus == CellStatus.Empty && BStatus == CellStatus.Player) ||
            (AStatus == CellStatus.Player && BStatus == CellStatus.Empty))
        {
            return WinningValue.PlayerAdvantage;
        }
        if ((AStatus == CellStatus.Empty && BStatus == CellStatus.AI) ||
            (AStatus == CellStatus.AI && BStatus == CellStatus.Empty))
        {
            return WinningValue.AIAdvantage;
        }
        if ((AStatus == CellStatus.Player && BStatus == CellStatus.AI) ||
            (AStatus == CellStatus.AI && BStatus == CellStatus.Player))
        {
            return WinningValue.DeadCell;
        }
        return WinningValue.Empty;
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

    public void Draw()
    {
        isGameOver = true;
        drawSprite.SetActive(true);
    }
}
