using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeColorController : MonoBehaviour
{
    [SerializeField] Tilemap[] tilemaps;
    [SerializeField] Color[] colors = new Color[4]
    {
        new Color(0, 255, 0, 255),
        new Color(255, 0, 0, 255),
        new Color(255, 0, 255, 255),
        new Color(0, 0, 255, 255)
    };
    static Color color = new Color(0, 0, 255, 255);
    // Start is called before the first frame update
    private void Start()
    {
        SetColor();
    }

    public void ChangeColor()
    {
        color = ReturnRandomColor();
    }

    Color ReturnRandomColor()
    {
        Color tempColor = colors[Random.Range(0, colors.Length)];
        while(tempColor == color)
        {
            tempColor = colors[Random.Range(0, colors.Length)];
        }
        return tempColor;
    }

    void SetColor()
    {
        foreach (Tilemap tilemap in tilemaps)
            tilemap.color = color;
    }
}
