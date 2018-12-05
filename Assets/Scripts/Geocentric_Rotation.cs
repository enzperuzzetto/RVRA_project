using UnityEngine;
using System.Collections;

public class Geocentric_Rotation : MonoBehaviour
{
    public float speed = 1.0f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime * speed);
    }
}