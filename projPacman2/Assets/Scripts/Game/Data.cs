using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

static class Data
{
    public static int EnemyCount = 6;

    public static float EnemySpeed = 0.8f;

    public static KeyCode[] Characters = new KeyCode[26]
    {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
        KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
        KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z
    };

    public static int Coin { get => 10; }

    public static int Treasure
    {
        get => GetRandomTreasureValue(new System.Random().Next(5));
    }

    private static int GetRandomTreasureValue(int randomValue)
    {
        int[] treasureValues = new int[5]
        {
            100, 200, 300, 400, 500
        };

        Debug.Log("O número de pontos pego foi de: " + treasureValues[randomValue]);
        return treasureValues[randomValue];
    }

    public static float Speed { get => 0.8f; }

    public static float VulnerableEnemySpeed { get => 0.4f; }

    public static float AnkhTime { get => 10f; }

}
