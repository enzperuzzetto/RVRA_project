using UnityEngine;
using ArucoUnity.Objects.Trackers;

public class displayScene : MonoBehaviour {

    public ArucoObjectsTracker tracker;
    public GameObject marker0;
	public GameObject marker1;
	private GameObject background;

    private Vector3 startingScale;

    // Use this for initialization
    void Start ()
    {
        background = GameObject.Find("background");
        startingScale = background.transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!isVisible(marker0) || !isVisible(marker1))
            background.SetActive(false);
        else
            background.SetActive(true);

		float dist = distance(marker0, marker1);
		setPosition(background, marker0, marker1);
        background.transform.localScale = new Vector3(dist * 0.1f, dist * 0.1f, dist * 0.1f);
		
	}

	void setPosition( GameObject toset, GameObject obj1, GameObject obj2)
    {
		float x = (obj1.transform.position.x + obj2.transform.position.x)/2.0f;
		float y = (obj1.transform.position.y + obj2.transform.position.y)/2.0f;
		float z = (obj1.transform.position.z + obj2.transform.position.z)/2.0f;
		toset.transform.position = new Vector3(x, y, z);
	}

    /*
     * Compute distance between two gameobject
     */
	float distance( GameObject obj1, GameObject obj2)
    {
		float x = obj1.transform.position.x - obj2.transform.position.x;
		float y = obj1.transform.position.y - obj2.transform.position.y;
		float z = obj1.transform.position.z - obj2.transform.position.z;
		return Mathf.Sqrt ((x * x) + (y * y) + (z * z));
	}

    /*
     * Check if gameObject is in camera fustrum
     */
    bool isVisible(GameObject obj)
    {
        return true;
    }
}
