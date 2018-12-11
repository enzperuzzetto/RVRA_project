using UnityEngine;
using ArucoUnity.Objects.Trackers;

public class displayScene : MonoBehaviour {

    public ArucoObjectsTracker tracker;
    public GameObject marker0;
	public GameObject marker1;
	private GameObject background;

    private Vector3 startingScale;

    private Vector3 prevPos0;
    private Vector3 currPos0;
    private Vector3 prevPos1;
    private Vector3 currPos1;
    private float passedTime;

    // Use this for initialization
    void Start ()
    {
        background = GameObject.Find("background");
        startingScale = background.transform.localScale;

        prevPos0 = Vector3.one;
        currPos0 = Vector3.one;
        prevPos1 = Vector3.one;
        currPos1 = Vector3.one;
        passedTime = Time.deltaTime; 
	}
	
	// Update is called once per frame
	void Update ()
	{
        currPos0 = marker0.transform.position;
        currPos1 = marker1.transform.position;

        passedTime = isVisible(passedTime, background, currPos0, prevPos0, currPos1, prevPos1);
        
        prevPos0 = currPos0;
        prevPos1 = currPos1;

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
     * Check if gameObject is changing position
     */
    bool isMoving(Vector3 pos0, Vector3 pos1){
        return (pos0!=pos1);

    }

    /*
     * Display the GameObject if his position has changed in the last half second
     */
    float isVisible(float passedTime, GameObject obj, Vector3 pos0, Vector3 prev0, Vector3 pos1, Vector3 prev1)
    {
        if (!isMoving(prev0, pos0) || !isMoving(prev1, pos1)){
            passedTime += Time.deltaTime;
            if (passedTime > 0.5){
                obj.SetActive(false);    
            }
        }
        else{
            obj.SetActive(true);
            passedTime = 0;
        }
        return passedTime;
    }


}
