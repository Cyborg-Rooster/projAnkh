using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PlayerCollisionsManager
{
    public CameraController Camera;
    public PlayerController Player;
    public ChangeMaterialController MaterialController;
    public PhaseController Manager;
    public Physics Movement;

    public void CheckCollisions()
    {
        if (Movement.GetObjectByCollision("Wall") != null) Player.OnPlayerStopped();

        CheckEnemyCollision();
        CheckPointsCollision();
        CheckFastTravelCollision();
        CheckAnkhCollision();
    }

    private void CheckPointsCollision()
    {
        GameObject collision = Movement.GetObjectByCollision("Itens");
        if (collision != null)
        {
            if (collision.tag == "Coin") Manager.AddPoints(Data.Coin);
            else Manager.AddPoints(Data.Treasure);

            GameObject.Destroy(collision);
        }
    }

    private void CheckEnemyCollision()
    {
        GameObject collision = Movement.GetObjectByCollision("Enemy");

        if (collision != null)
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy.alive && Player.alive)
            {
                Camera.ShakeObject();
                if (AnkhManager.AnkhCatched) enemy.StartCoroutine("Die");
                else Player.Die();

                Debug.Log("Player colidiu com o(a) " + collision.name);
            }
        }
    }

    private void CheckFastTravelCollision()
    {
        GameObject collision = Movement.GetObjectByCollision("FastTravel");
        if (collision != null)
        {
            collision.GetComponent<FastTravelManager>().ChangePositionOfObject(Player.gameObject);
            Movement.UpdateCollider();
        }
    }

    private void CheckAnkhCollision()
    {
        GameObject collision = Movement.GetObjectByCollision("Ankh");
        if (collision != null)
        {
            MaterialController.StartBlinkForAWhile(Data.AnkhTime);
            AnkhManager.GetAnkh(Player);
            GameObject.Destroy(collision);
        }
    }
}
