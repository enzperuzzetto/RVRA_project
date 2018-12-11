using UnityEngine;

public class displayScene : MonoBehaviour {

    public GameObject marker0;
	public GameObject marker1;
	private GameObject background;

    private float passedTime;

    private const float UPDATE_TIME = 0.5f;

    // Use this for initialization
    void Start ()
    {
        background = GameObject.Find("background");
        passedTime = 0;
         
	}
	
	// Update is called once per frame
	void Update ()
	{

        if (marker0.activeSelf && marker1.activeSelf){
            background.SetActive(true);
        }
        else{
            background.SetActive(false);
        }

		float dist = distance(marker0, marker1);
        
        Vector3 relative_pos = marker0.transform.position - marker1.transform.position;
        //background.transform.rotation = Quaternion.LookRotation(relative_pos, Vector3.left);
        //background.transform.rotation = Quaternion.LookRotation(Vector3.up, relative_pos);
        background.transform.rotation = marker0.transform.rotation;


        passedTime += Time.deltaTime;
        if (passedTime > UPDATE_TIME){
		    setPosition(background, marker0, marker1);
            background.transform.localScale = new Vector3(dist * 0.1f, dist * 0.1f, dist * 0.1f);
            passedTime = 0.0f;
        }
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

}
