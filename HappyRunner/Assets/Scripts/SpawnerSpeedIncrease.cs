using UnityEngine;
using System.Collections;

public class SpawnerSpeedIncrease : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<PlatformGenerator>().OnSpawn += IncreaseSpeed;
	}

	private void IncreaseSpeed(InstantVelocity spawnedVelocity) {
		spawnedVelocity.velocity = new Vector2(
			spawnedVelocity.velocity.x * (1f + (BackgroundSpeedIncrease.MaxSpeedMultiplier * (BackgroundSpeedIncrease.speedTimer / BackgroundSpeedIncrease.TimeForMaxSpeed))),
			spawnedVelocity.velocity.y);

	}
}
