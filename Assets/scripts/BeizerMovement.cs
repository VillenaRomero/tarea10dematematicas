using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeizerMovement : MonoBehaviour
{
    [SerializeField] private BeizerCubic beizerCubic;
    [SerializeField] private float speed;
    [SerializeField] private float sampleTime = 0f;

    private void Update() {
        sampleTime += Time.deltaTime * speed;
        transform.position = beizerCubic.Position(sampleTime);
        transform.forward = beizerCubic.Position(sampleTime + 0.001f) - transform.position;

        if(sampleTime >= 1f){
            sampleTime = 0;
        }
    }
}
