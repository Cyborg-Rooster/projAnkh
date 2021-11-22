using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    [SerializeField] MainController main;
    public int index = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) UpSelector();
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) DownSelector();
        else if (Input.GetKeyDown(KeyCode.Return)) main.SelectOption(index);
    }

    void DownSelector()
    {
        index++;
        if (index > 2)
        {
            index = 0;
            transform.position = new Vector3(transform.position.x, -0.45f, 0);
        }
        else transform.position = new Vector3(transform.position.x, transform.position.y - 0.11f, 0);
    }

    void UpSelector()
    {
        index--;
        if (index < 0)
        {
            index = 2;
            transform.position = new Vector3(transform.position.x, -0.67f, 0);
        }
        else transform.position = new Vector3(transform.position.x, transform.position.y + 0.11f, 0);
    }
}
