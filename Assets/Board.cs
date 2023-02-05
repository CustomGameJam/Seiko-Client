using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Grid[] grids;
    public object lockObject = new();
    public List<BoardAction> actions = new();
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        StartCoroutine(RunActions());
    }

    public Vector3 FindGridPosition(int index)
    {
        return grids[index].gameObject.transform.position;
    }

    public Grid FindGrid(int index)
    {
        return grids[index];
    }

    IEnumerator RunActions()
    {
        while (true)
        {
            yield return new WaitUntil(() => actions.Count > 0);
            lock (lockObject)
            {
                foreach (var action in actions)
                {
                    yield return PlayAction(action);
                }

                actions.Clear();
            }
        }
    }

    IEnumerator PlayAction(BoardAction action)
    {
        switch (action.type)
        {
            case ActionType.Turn:
            {
                var player = action.player == "nil" ? null : action.player;

                _gameManager.SetTurn(player);
                // todo change turn and wait for that
                break;
            }
            case ActionType.Change:
            {
                var player = action.player == "nil" ? null : action.player;
                var index = action.gridIndex;
                var count = action.count;
                
                Debug.Log("player:" + player + " index:" + index + " count:" + count);

                yield return grids[index].ChangeAnimation(count, player);
                // todo change
                break;
            }
            case ActionType.Boom:
            {
                var player = action.player == "nil" ? null : action.player;
                var index = action.gridIndex;
                var changes = action.targets;

                yield return grids[index].BoomAnimation(player, changes);
                // todo boom
                break;
            }
            case ActionType.GameStarted:
            {
                var player = action.player;
                _gameManager.player = player;
                _gameManager.ChangeGameStatus(GameManager.GameStatues.Joined);
                break;
            }
        }

        // todo
        yield break;
    }
}