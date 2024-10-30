using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeizerCubic : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform midPoint;

    [SerializeField] private float middlePoints;
    [SerializeField] private float pointRadious;

    public Vector3 Position(float t){
        Vector3 firstSequence = Vector3.Lerp(startPoint.position, midPoint.position, t);
        Vector3 secondSequence = Vector3.Lerp(midPoint.position, endPoint.position,t);
        return Vector3.Lerp(firstSequence, secondSequence, t);
    }

    private void OnDrawGizmos() {
        if(startPoint == null || endPoint == null || midPoint == null) return;

        for (int i = 0; i < middlePoints; i++)
        {
            Gizmos.DrawWireSphere(Position(i/middlePoints), pointRadious);
        }
    }
}
