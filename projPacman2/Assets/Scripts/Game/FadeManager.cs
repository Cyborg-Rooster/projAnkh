using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FadeManager
{
    public SpriteRenderer Renderer;

    public void DecreaseTransparency()
    {
        Color temp = Renderer.color;

        temp.a -= 0.25f;

        Renderer.color = temp;
    }

    public void IncreaseTransparency()
    {
        Color temp = Renderer.color;

        temp.a += 0.25f;

        Renderer.color = temp;
    }
}
