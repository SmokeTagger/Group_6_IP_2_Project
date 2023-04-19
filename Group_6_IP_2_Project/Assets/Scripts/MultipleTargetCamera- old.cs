using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCameraold : MonoBehaviour
{
    public List<Transform> target;

    public Vector3 offset;

    private void LateUpdate()
    {
        if (target.Count == 0)
            return;

        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = centerPoint;
    }

    Vector3 GetCenterPoint()
    {
        if(target.Count == 1)
        {
            return target[0].position;
        }

        var bounds = new Bounds(target[0].position, Vector3.zero);
        for(int i = 0; i < target.Count; i++)
        {
            bounds.Encapsulate(target[i].position);
        }

        return bounds.center;
    }
}
