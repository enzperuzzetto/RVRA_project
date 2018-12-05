using UnityEngine;
using System.Collections;

public class Heliocentric_Rotation : MonoBehaviour
{
    public GameObject sun;
    public float speed = 1.0f;

    void Update()
    {
        transform.RotateAround(sun.transform.position, Vector3.up, 20 * Time.deltaTime * speed);
    }
}