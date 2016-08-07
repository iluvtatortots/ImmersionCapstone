using UnityEngine;
using System.Collections;

public class PlayerSpeedIncrease : MonoBehaviour {

	private Animator playerAnimator;

	// Use this for initialization
	void Start () {
		playerAnimator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		playerAnimator.speed = (1f + (BackgroundSpeedIncrease.MaxSpeedMultiplier * (BackgroundSpeedIncrease.speedTimer / BackgroundSpeedIncrease.TimeForMaxSpeed)));
    }
}
