using System.Collections;
using UnityEngine;

class Shake
{
    public static IEnumerator ShakeObject(float duration, float magnetude, float minimumX, float maximumX, Transform transform)
    {
        Debug.Log("teste");

        float elapse = 0.0f;

        while (elapse < duration)
        {
            float x = Random.Range(minimumX, maximumX) * magnetude;

            transform.position = new Vector3
            (
                transform.position.x + x, 
                transform.position.y, 
                transform.position.z
            );

            elapse += Time.deltaTime;

            yield return null;
        }
    }
}