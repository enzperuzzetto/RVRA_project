using UnityEngine;
using ArucoUnity.Objects.Trackers;
using System.Collections.Generic;

public class displayPlanet : MonoBehaviour
{

    public ArucoObjectsTracker tracker;
	public List<GameObject> markers;
	public List<GameObject> planets;


    // Use this for initialization
    void Start()
	{
		for (int i = 0; i < markers.Count; i++) {
			planets[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		}

    }

    // Update is called once per frame
    void Update()
	{
		for (int i = 0; i < markers.Count; i++) {
			planets [i].SetActive (true);
			setPosition (planets [i], markers [i]);
		}
	}


	void setPosition(GameObject planet, GameObject marker)
	{
		planet.transform.parent = marker.transform;
		planet.transform.localPosition = new Vector3 (.0f, 0.05f, .0f);
	}

    
    /*
     * Check if gameObject is in camera fustrum
     */
    bool isVisible(GameObject obj)
    {
        return true;
    }
}
