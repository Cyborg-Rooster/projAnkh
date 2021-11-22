using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

class PopulateGridWithObjects
{
    public Grid Grid;
    public Tilemap Tilemap;
    public GameObject ObjectToSpawn;
    public Transform Transform, Parent;

    public int X, Y;
    public float StartX;

    /// <summary>
    /// <para>Popula todas as células de um grid que estiverem com tile.</para>
    /// </summary>
    public void Populate()
    {
        for (int i = 0; i < Y; i++)
        {
            for (int f = 0; f < X; f++)
            {
                float localX = Transform.localPosition.x;
                if (CheckIfCanPopulate(Transform.position)) SpawnObject();

                Transform.localPosition = new Vector3(localX += 0.16f, Transform.position.y, Transform.position.z);
            }

            float localY = Transform.localPosition.y;
            Transform.localPosition = new Vector3(StartX, localY - 0.16f, Transform.position.z);
        }
    }
    /// <summary>
    /// <para>Popula uma célula aleatória de um grid que estiver com tile.</para>
    /// </summary>
    public void PopulateRandomCell()
    {
        List<Vector2> possiblePositions = new List<Vector2>();

        for (int i = 0; i < Y; i++)
        {
            for (int f = 0; f < X; f++)
            {
                float localX = Transform.localPosition.x;
                if (CheckIfCanPopulate(Transform.position)) possiblePositions.Add(Transform.position);

                Transform.localPosition = new Vector3(localX += 0.16f, Transform.position.y, Transform.position.z);
            }

            float localY = Transform.localPosition.y;
            Transform.localPosition = new Vector3(StartX, localY - 0.16f, Transform.position.z);
        }
        System.Random r = new System.Random();
        Transform.localPosition = possiblePositions[r.Next(0, possiblePositions.Count)];
        SpawnObject();
    }

    /// <summary>
    /// <para>Instancia o objeto.</para>
    /// </summary>
    private void SpawnObject()
    {
        var _obj = GameObject.Instantiate(ObjectToSpawn, Parent);
        _obj.transform.localPosition = Transform.localPosition;
    }

    /// <summary>
    /// <para>Retorne se há um tile em determinada célula do Grid.</para>
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    bool CheckIfCanPopulate(Vector3 pos)
    {
        Vector3Int cell = Grid.WorldToCell(pos);
        Collider2D obj = Physics2D.OverlapPoint(Transform.position);
        return Tilemap.HasTile(cell) && obj == null;
    }
}
