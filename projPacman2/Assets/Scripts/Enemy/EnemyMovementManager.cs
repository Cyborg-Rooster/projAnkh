using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class EnemyMovementManager
{
    public Physics Movement;
    public Transform Player;
    public GameObject GameObject;
    public Transform Transform;

    public KeyCode ChooseNewWay()
    {
        List<KeyCode> codes = ReturnKeyCodeList();
        KeyCode code;

        System.Random r = new System.Random();
        code = codes[r.Next(0, codes.Count())];

        //Debug.Log(string.Format("{0} decidiu apertar {1}", GameObject.name, code));
        return code;
    }

    List<KeyCode> ReturnKeyCodeList()
    {
        List<KeyCode> codes = new List<KeyCode>() { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

        if (AnkhManager.AnkhCatched)
        {
            codes = DeleteCodesCheckingPlayerTransform();
            if (codes.Count < 1) codes = DeleteCodes();
        }
        else codes = DeleteCodes();

        return codes;
    }

    List<KeyCode> DeleteCodes()
    {
        List<KeyCode> codes = new List<KeyCode>() { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

        if (Movement.HaveCollision(KeyCode.W, false)) codes.Remove(KeyCode.W);
        if (Movement.HaveCollision(KeyCode.A, false)) codes.Remove(KeyCode.A);
        if (Movement.HaveCollision(KeyCode.S, false)) codes.Remove(KeyCode.S);
        if (Movement.HaveCollision(KeyCode.D, false)) codes.Remove(KeyCode.D);

        return codes;
    }

    List<KeyCode> DeleteCodesCheckingPlayerTransform()
    {
        List<KeyCode> codes = new List<KeyCode>() { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

        if (Movement.HaveCollision(KeyCode.W, false) || Player.position.y > Transform.position.y) codes.Remove(KeyCode.W);
        if (Movement.HaveCollision(KeyCode.A, false) || Player.position.x < Transform.position.x) codes.Remove(KeyCode.A);
        if (Movement.HaveCollision(KeyCode.S, false) || Player.position.y < Transform.position.y) codes.Remove(KeyCode.S);
        if (Movement.HaveCollision(KeyCode.D, false) || Player.position.x > Transform.position.x) codes.Remove(KeyCode.D);

        return codes;
    }
}
