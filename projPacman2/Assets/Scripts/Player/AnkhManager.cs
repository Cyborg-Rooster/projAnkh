using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class AnkhManager
{
    public static bool AnkhCatched = false;
    public static List<EnemyController> Enemies;
    public static AnkhListeningController Controller;

    public static void GetAnkh(PlayerController player)
    {
        AnkhCatched = true;
        Debug.Log("Akhn está ativo");

        foreach (var enemy in Enemies) enemy.SetVulnerability(true);
        player.StartCoroutine(WaitAnkhTime());
    }

    public static void DeleteEnemy(EnemyController enemy)
    {
        Enemies.Remove(enemy);
    }

    static IEnumerator WaitAnkhTime()
    {
        yield return new WaitForSeconds(Data.AnkhTime);
        LeftAnkh();
    }

    static void LeftAnkh()
    {
        Debug.Log("Akhn está desativo");
        foreach (var enemy in Enemies) enemy.SetVulnerability(false);
        AnkhCatched = false;
        Controller.SpawnAnkh();
    }

}
