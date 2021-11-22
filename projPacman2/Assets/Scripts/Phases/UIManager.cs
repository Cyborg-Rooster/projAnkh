using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

class UIManager
{
    public Text TxtPoints, TxtEnemies, TxtLifes;

    public int AddValue(int a, int b)
    {
        return a + b;
    }

    public int SubValue(int a, int b)
    {
        return a - b;
    }

    public void UpdateValues(int points, int enemyCount, int lifes)
    {
        TxtPoints.text = points.ToString();
        TxtEnemies.text = $"Inimigos: {enemyCount.ToString()}";
        TxtLifes.text = $"{lifes}x";
    }

    public void ChangeTextBoxValue(Text textbox, string value)
    {
        textbox.text = value;
    }
}
