using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class EnemyAnimationController : AnimationManager
{
    private KeyCode LastCode;
    public RuntimeAnimatorController Default, Vulnerable;
    public void ChangeAnimationByInput(KeyCode code)
    {
        LastCode = code;
        string Animation = CheckAnimation(code);

        if(LastCode ==  KeyCode.A) ChangeAnimation(Animation, true);
        else ChangeAnimation(Animation);

        ChangeBoolean("walk", true);
    }

    public void ChangeController(bool vulnerable)
    {
        if (vulnerable) ChangeAnimatorController(Vulnerable);
        else ChangeAnimatorController(Default);

        ChangeAnimationByInput(LastCode);
    }

    public void StopWalkAnimation()
    {
        ChangeBoolean("walk", false);
    }

    public void Die()
    {
        ChangeAnimation("EnemyDeath");
    }

    private string CheckAnimation(KeyCode code)
    {
        if (code == KeyCode.W) return "EnemyWalkUp";
        else if (code == KeyCode.A) return "EnemyWalkHorizontal";
        else if (code == KeyCode.S) return "EnemyWalkDown";
        else if (code == KeyCode.D) return "EnemyWalkHorizontal";

        return "";
    }
}
