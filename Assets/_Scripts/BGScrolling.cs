using System;
using UnityEngine;

[Serializable]
public struct Boundary { public float min, max; }

public class BGScrolling : MonoBehaviour
{
    [SerializeField] private Boundary verticalBoundary;
    [SerializeField] private float scrollSpeed;

    private void Update()
    {
        transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
        if (transform.position.y < verticalBoundary.min)
        {
            transform.position = new Vector3
            (transform.position.x, verticalBoundary.max, transform.position.z);
        }
    }
}