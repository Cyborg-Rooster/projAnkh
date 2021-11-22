using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PopulateGridController : MonoBehaviour
{
    [SerializeField] Grid grid;
    [SerializeField] Tilemap tilemapLayer;
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Transform parent;

    //28, 32
    public int x, y;
    
    // Start is called before the first frame update
    void Awake()
    {
        PopulateGridWithObjects populate = new PopulateGridWithObjects()
        {
            Grid = grid,
            Tilemap = tilemapLayer,
            ObjectToSpawn = objectToSpawn,
            Transform = transform,
            Parent = parent,
            StartX = transform.position.x,
            X = x,
            Y = y
        };
        populate.Populate();
    }
}
