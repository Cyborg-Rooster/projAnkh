using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

static class InputObserver
{
    public static KeyCode MovementInput;
    public static bool CheckIfMovementInputHasBeenPressed()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) return SaveLastMovementInput(KeyCode.W);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) return SaveLastMovementInput(KeyCode.A);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) return SaveLastMovementInput(KeyCode.S);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) return SaveLastMovementInput(KeyCode.D);
        else return false;
    }

    private static bool SaveLastMovementInput(KeyCode value)
    {
        MovementInput = value;
        return true;
    }
}
