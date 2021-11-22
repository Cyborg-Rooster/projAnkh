using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PlayerAnimationController : AnimationManager
{
    public void ChangeAnimationByInput(KeyCode code)
    {
        if (code == KeyCode.W) ChangeAnimation("playerWalkUp");
        else if (code == KeyCode.A) ChangeAnimation("playerWalkHorizontal", true);
        else if (code == KeyCode.S) ChangeAnimation("playerWalkDown");
        else if (code == KeyCode.D) ChangeAnimation("playerWalkHorizontal");
        ChangeBoolean("walk", true);
    }

    public void StopWalkAnimation()
    {
        ChangeBoolean("walk", false);
    }

    public void Die()
    {
        ChangeAnimation("playerDeath");
    }

    public void TurnFoward()
    {
        ChangeAnimation("playerIdle");
    }
}
