using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Grid : MonoBehaviour
{
    public int gridIndex;
    public int lvl;
    public GameObject[] firstPlayerBombs;
    public GameObject[] secondPlayerBombs;
    public GameObject currentBomb;

    private GameManager _gameManager;
    private Board _board;
    private string owner;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _board = FindObjectOfType<Board>();
    }

    private void OnMouseDown()
    {
        if ((owner == null || owner == _gameManager.player))
        {
            _gameManager.Click(gridIndex);
        }
    }

    public GameObject[] getPlayerBombs(string player)
    {
        return player == "p1" ? firstPlayerBombs : secondPlayerBombs;
    }

    public IEnumerator ChangeAnimation(int level, string player)
    {
        owner = player;

        switch (level)
        {
            case 0:
                currentBomb.transform.DOScale(new Vector3(0, 0, 0), .2f);
                yield return new WaitForSeconds(.2f);
                Destroy(currentBomb);
                yield break;
            case > 1:
                currentBomb.transform.DOScale(new Vector3(0, 0, 0), .2f);
                yield return new WaitForSeconds(.2f);
                Destroy(currentBomb);
                break;
        }

        currentBomb = Instantiate(getPlayerBombs(player)[level - 1], transform.position, transform.rotation);
        currentBomb.transform.localScale = Vector3.zero;
        currentBomb.transform.DOScale(new Vector3(1, 1, 1), .2f);
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator BoomAnimation(string player, int[] locations)
    {
        currentBomb.transform.DOScale(new Vector3(2, 2, 2), .2f);

        var createdObjects = new List<GameObject>();

        for (var i = 0; i < locations.Length; i++)
        {
            createdObjects.Add(Instantiate(getPlayerBombs(player)[0], transform.position, transform.rotation));
            createdObjects[i].transform.localScale = Vector3.zero;
            createdObjects[i].transform.DOJump(_board.FindGridPosition(locations[i]) + Vector3.up, 1f, 1, .5f);
            createdObjects[i].transform.DOScale(new Vector3(.3f, .3f, .3f), .4f);
        }

        yield return new WaitForSeconds(.5f);
        foreach (var obj in createdObjects)
        {
            Destroy(obj);
        }
    }
}