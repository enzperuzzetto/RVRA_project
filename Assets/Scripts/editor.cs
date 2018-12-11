using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class editor : MonoBehaviour {

	public GameObject max;
	public GameObject min;
	public GameObject middle;
	
	private List<string> planets;

	private float passedTime;

    private const float UPDATE_TIME = 1.0f;

    // Use this for initialization
    void Start () {
		planets = new List<string>(9){"Earth","Sun","Mercury","Venus","Mars","Jupiter","Saturn","Uranus","Neptune"};
		float passedTime = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if( min.activeSelf && max.activeSelf)
		{
			passedTime += Time.deltaTime;
			if (passedTime > UPDATE_TIME){
				passedTime = 0;
				Scale();
			}
		}
		
	}

	private void Scale()
	{
		Transform child = null;
		for(int i=0; i<planets.Count; i++){
			child = middle.transform.Find(planets[i]);
			if( child != null )
				break;
		}
		if(child != null)
		{
			Vector3 init = child.localScale;
			float dist = distance(min, max);
			float dist2 = distance(min, middle);
			float d = dist2/dist;
			Vector3 s = new Vector3(d*2.0f, d*2.0f, d*2.0f);
			child.localScale = Vector3.Scale(init, s);
		}
	}

	float distance( GameObject obj1, GameObject obj2)
    {
		float x = obj1.transform.position.x - obj2.transform.position.x;
		//float y = obj1.transform.position.y - obj2.transform.position.y;
		//float z = obj1.transform.position.z - obj2.transform.position.z;
		return Mathf.Sqrt ((x * x));// + (y * y) + (z * z));
	}

}
