using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject uI;
    public Client client;

    public enum GameStatues
    {
        Idle,
        Waiting,
        Joined,
    }

    public GameStatues currentStatue;
    public Board board;
    public string player;
    public string turn;

    public void ChangeGameStatus(GameStatues newStatue)
    {
        currentStatue = newStatue;
        switch (currentStatue)
        {
            case GameStatues.Idle:
                // camera animation
                break;
            case GameStatues.Waiting:
                // button go brrr
                // waiting text opeen
                break;
            case GameStatues.Joined:
                uI.SetActive(false);
                break;
        }
    }

    public void SetTurn(string turn)
    {
        this.turn = turn;
    }

    public void Click(int index)
    {
        if (currentStatue != GameStatues.Joined)
        {
            return;
        }

        client.Click(index);
    }

    public void CreateAction(ActionType type, string player, int gridIndex = 0, int[] targets = null, int count = 0)
    {
        var action = new BoardAction(type, player, gridIndex, count, targets);
        lock (board.lockObject)
        {
            board.actions.Add(action);
        }
    }
}