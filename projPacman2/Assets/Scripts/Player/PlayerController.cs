using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float speed;
    public bool alive = true;

    Physics movement;
    PlayerAnimationController animationController;
    PlayerCollisionsManager collisionsManager;
    ChangeMaterialController materialController;

    Animator animator;
    SpriteRenderer sprite;

    [SerializeField] Grid grid;
    [SerializeField] PhaseController manager;
    [SerializeField] CameraController cameraController;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        materialController = GetComponent<ChangeMaterialController>();

        movement = new Physics()
        {
            Transform = transform,
            Speed = Data.Speed,
            Grid = grid
        };

        animationController = new PlayerAnimationController()
        {
            Animator = animator,
            Renderer = sprite
        };

        collisionsManager = new PlayerCollisionsManager()
        {
            Camera = cameraController,
            Player = this,
            MaterialController = materialController,
            Movement = movement,
            Manager = manager
        };

        movement.SetNextPos(transform.localPosition);
    }

    void Update()
    {
        if (alive && manager.Started)
        {
            if (InputObserver.CheckIfMovementInputHasBeenPressed() && !movement.HaveCollision(InputObserver.MovementInput, true))
                animationController.ChangeAnimationByInput(InputObserver.MovementInput);
        }
    }

    private void FixedUpdate()
    {
        movement.Move();
        collisionsManager.CheckCollisions();
    }

    public void Die()
    {
        if (alive)
        {
            movement.Speed = 0;
            alive = false;
            animationController.Die();
            manager.Finish(true);
        }
    }

    public void OnPlayerStopped()
    {
        animationController.StopWalkAnimation();
    }

    public void StopAndTurnForward()
    {
        movement.SetCanMove(false);
        animationController.TurnFoward();
    }
}
