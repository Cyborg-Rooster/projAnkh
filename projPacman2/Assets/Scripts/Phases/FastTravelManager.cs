using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FastTravelManager : MonoBehaviour
{
    [SerializeField] Vector3 positionToSpawn;

    public void ChangePositionOfObject(GameObject obj)
    {
        positionToSpawn.z = obj.transform.position.z;
        obj.transform.position = positionToSpawn;
    }
}
