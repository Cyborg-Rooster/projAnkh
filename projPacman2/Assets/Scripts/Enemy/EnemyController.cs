using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class EnemyController : MonoBehaviour
{
    public float speed = Data.EnemySpeed;
    public bool alive = true;

    Physics movement;
    EnemyAnimationController animationManager;
    EnemyMovementManager movementManager;
    Grid grid;

    [SerializeField] Transform player;
    [SerializeField] PhaseController phaseController;
    [SerializeField] RuntimeAnimatorController defaultController;
    [SerializeField] RuntimeAnimatorController vulnerableController;

    KeyCode code;

    private void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        movement = new Physics()
        {
            Transform = transform,
            Speed = speed,
            Grid = grid
        };

        animationManager = new EnemyAnimationController()
        {
            Animator = GetComponent<Animator>(),
            Renderer = GetComponent<SpriteRenderer>(),
            Default = defaultController,
            Vulnerable = vulnerableController
        };

        movementManager = new EnemyMovementManager()
        {
            GameObject = gameObject,
            Transform = transform,
            Movement = movement,
            Player = player
        };

        movement.HaveCollision(KeyCode.S, true);
    }

    private void Update()
    {
        if (phaseController.Started)
        {
            code = movementManager.ChooseNewWay();

            if (movement.GetObjectByCollision("Wall") != null && !movement.HaveCollision(code, true))
                animationManager.ChangeAnimationByInput(code);
        }
    }

    private void FixedUpdate()
    {
        movement.Move();
    }

    public IEnumerator Die()
    {
        if (alive)
        {
            alive = false;
            movement.SetCanMove(false);
            animationManager.Die();

            yield return new WaitForSeconds(0.5f);

            AnkhManager.DeleteEnemy(this);
            PhaseController.points += 500 * PhaseController.phaseCount;
            phaseController.SubEnemy(1);
            Destroy(gameObject);
        }
    }

    public void SetVulnerability(bool vulnerable)
    {
        if (alive)
        {
            animationManager.ChangeController(vulnerable);
            ChangeVulnerabilitySpeed(vulnerable);
        }
    }

    public void ChangeVulnerabilitySpeed(bool vulnerable)
    {
        if (vulnerable) movement.ChangeSpeed(Data.VulnerableEnemySpeed);
        else movement.ChangeSpeed(Data.EnemySpeed);
    }
}
