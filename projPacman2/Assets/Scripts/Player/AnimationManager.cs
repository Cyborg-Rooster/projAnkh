using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class AnimationManager
{
    public Animator Animator;
    public SpriteRenderer Renderer;

    public void ChangeAnimation(string animation)
    {
        Animator.Play(animation);
        Renderer.flipX = false;
    }

    public void ChangeAnimation(string animation, bool _flipX)
    {
        Animator.Play(animation);
        Renderer.flipX = _flipX;
    }

    public void ChangeBoolean(string boolean, bool value)
    {
        Animator.SetBool(boolean, value);
    }

    public void ChangeAnimatorController(RuntimeAnimatorController controller)
    {
        Animator.runtimeAnimatorController = controller;
    }

}
