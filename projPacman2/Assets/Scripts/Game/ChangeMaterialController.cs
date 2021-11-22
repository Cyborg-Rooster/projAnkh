using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ChangeMaterialController : MonoBehaviour
{
    public float time;
    public bool isBlinking;
    bool isMaterialChanged;
    float timeRange;

    [SerializeField]
    Material material;

    Material startMaterial;
    SpriteRenderer spr;

    public void StartBlinkForAWhile(float time)
    {
        StartCoroutine(ChangeForTime(time));
    }

    public void StartBlink()
    {
        isBlinking = true;
    }

    public void StopBlink()
    {
        isBlinking = false;

        if (isMaterialChanged) ChangeToMaterial();
    }

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        startMaterial = spr.material;
    }

    void Update()
    {
        if (isBlinking && Time.time > timeRange)
        {
            timeRange = Time.time + time;
            ChangeToMaterial();
        }
    }

    void ChangeToMaterial()
    {
        if (isMaterialChanged)
        {
            spr.material = startMaterial;
            isMaterialChanged = false;
        }
        else
        {
            spr.material = material;
            isMaterialChanged = true;
        }
    }

    IEnumerator ChangeForTime(float _time)
    {
        float range = time;
        float halfTime = _time / 2;

        StartBlink();
        yield return new WaitForSeconds(halfTime);

        time = time / 2;
        yield return new WaitForSeconds(halfTime/2);

        time = time / 2;
        yield return new WaitForSeconds(halfTime / 2);

        StopBlink();
        time = range;
    }
}
