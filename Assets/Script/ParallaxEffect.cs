using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    Vector2 startingPosition;
    Vector2 canMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float startingZ;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    void Update()
    {
        if (followTarget != null)
        {
            Vector2 newPosition = startingPosition * canMoveSinceStart * parallaxFactor;
            transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
        }
    }
}
