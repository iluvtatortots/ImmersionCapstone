using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

    public GameObject platform;
    public Transform generationPoint;
    public float distinceBetween;
    public float distinceBetweenMin;
    public float distinceBetweenMax;

    private float platformWidth;

    public ObjectPooler objectPool;
    // Use this for initialization
    void Start () {
        platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
	}
	
	// Update is called once per frame
	void Update () {

        GeneratePlatform();
	}

    public void GeneratePlatform()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            distinceBetween = Random.Range(distinceBetweenMin, distinceBetweenMax);

            transform.position = new Vector3(transform.position.x + platformWidth + distinceBetween, transform.position.y, transform.position.z);

            //Instantiate(platform, transform.position, transform.rotation);
            GameObject newPlatform = objectPool.GetPooledOjbect();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

        }
    }
}
