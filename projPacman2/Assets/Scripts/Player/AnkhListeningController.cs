using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
class AnkhListeningController : MonoBehaviour
{
    [SerializeField] EnemyController[] enemyControllers;
    [SerializeField] GameObject ankh;
    [SerializeField] Grid grid;
    [SerializeField] Tilemap tilemapLayer;

    PopulateGridWithObjects populate;

    public int x, y;

    private void Start()
    {
        AnkhManager.Enemies = enemyControllers.ToList();
        AnkhManager.Controller = this;

        populate = new PopulateGridWithObjects()
        {
            Grid = grid,
            Tilemap = tilemapLayer,
            ObjectToSpawn = ankh,
            Transform = transform,
            Parent = grid.transform,
            StartX = transform.position.x,
            X = x,
            Y = y
        };
    }

    public void SpawnAnkh()
    {
        populate.PopulateRandomCell();
    }
}
