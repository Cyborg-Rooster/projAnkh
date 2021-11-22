using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public float secondsApart;

    SpriteRenderer rend;
    FadeManager fade;

    // Start is called before the first frame update
    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        fade = new FadeManager()
        {
            Renderer = rend
        };
    }

    public IEnumerator StartFadeIn()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(secondsApart);
            fade.DecreaseTransparency();
        }
    }

    public IEnumerator StartFadeOut()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(secondsApart);
            fade.IncreaseTransparency();
        }
    }
}
