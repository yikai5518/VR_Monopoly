﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MONOPOLY;
using PlayerInterface;

public class Game : MonoBehaviour
{
    private Board board;
    private GameObject[] dice;

    // Start is called before the first frame update
    void Start()
    {
        board = new Board();

        GameObject[] player_uis = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < Board.PLAYER_COUNT; ++i)
        {
            board.players[i].player_ui = player_uis[i].GetComponent<PlayerUI>();
            board.players[i].player_ui.player_index = i;
        }

        dice = GameObject.FindGameObjectsWithTag("Dice");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Step();
        }
    }

    void Step()
    {
        dice[0].transform.position = new Vector3(4, -9, 0);
        dice[1].transform.position = new Vector3(0, -9, 0);

        Dice[] dice_script = new Dice[2];
        dice_script[0] = dice[0].GetComponent<Dice>();
        dice_script[1] = dice[1].GetComponent<Dice>();

        int[] results = new int[2];
        results[0] = Random.Range(1, 7);
        results[1] = Random.Range(1, 7);

        dice_script[0].SetDiceFace(results[0]);
        dice_script[1].SetDiceFace(results[1]);

        board.Step(results[0], results[1]);
    }
}
