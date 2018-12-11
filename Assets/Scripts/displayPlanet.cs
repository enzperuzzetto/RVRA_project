using UnityEngine;
using ArucoUnity.Objects.Trackers;
using System.Collections.Generic;

public class displayPlanet : MonoBehaviour
{
	public List<GameObject> markers;
	public List<GameObject> planets;

    private float transferCooldown;
	private const float COOLDOWN_VALUE = 2.0f;
	private const float TRANSFER_DIST_MIN = 0.1f;
	private List<float> passedTime = new List<float>();


    // Use this for initialization
    void Start()
	{
		for (int i = 0; i < markers.Count; i++) {
			passedTime.Add(.0f);
			if (planets[i] != null) {
				markers[i].transform.position = new Vector3(3.0f * i, 3.0f * i, 3.0f * i);
				planets[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			}
		}
		transferCooldown = COOLDOWN_VALUE;
    }

    // Update is called once per frame
    void Update()
	{
		transferCooldown = Mathf.Max(0.0f, transferCooldown - Time.deltaTime);

		for (int i = 0; i < markers.Count; i++) {
			if (planets[i] != null) {
				planets [i].SetActive (true);
				passedTime[i] = updatePosition(passedTime[i], planets[i], markers[i]);			
			}
			else {
				for (int j = 0; j < markers.Count; j++) {
					if (planets[j] != null && transferCooldown == .0f && distance(markers[i], markers[j]) < TRANSFER_DIST_MIN) {
						planets[i] = planets[j];
						planets[j] = null;
						setPosition(planets[i], markers[i]);
						transferCooldown = COOLDOWN_VALUE;
						break;
					}
				}
			}
		}
	}


	void setPosition(GameObject planet, GameObject marker)
	{
		planet.transform.parent = marker.transform;
		planet.transform.localPosition = new Vector3 (.0f, 0.05f, .0f);
	}

    /*
     * Compute distance between two gameobject
     */
	float distance(GameObject obj1, GameObject obj2)
    {
		float x = obj1.transform.position.x - obj2.transform.position.x;
		float y = obj1.transform.position.y - obj2.transform.position.y;
		float z = obj1.transform.position.z - obj2.transform.position.z;
		return Mathf.Sqrt ((x * x) + (y * y) + (z * z));
	}
    
	/*
	 *	Update Position of obj according to obj2 if it has not been update 
	 *  in the last half second 
	 */
    float updatePosition(float passedTime, GameObject obj, GameObject obj2)
    {
        passedTime += Time.deltaTime;
        if (passedTime > 0.5){
        	setPosition(obj, obj2);   
		}
        return passedTime;
    }

}
