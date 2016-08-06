using UnityEngine;
using System.Collections;

public class PlatformDestroyer : MonoBehaviour {

    public GameObject platformDestuctionPoint;

	// Use this for initialization
	void Start () {
        platformDestuctionPoint = GameObject.Find("PlatformDestructionPoint");
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(transform.position.x < platformDestuctionPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
	}
}
