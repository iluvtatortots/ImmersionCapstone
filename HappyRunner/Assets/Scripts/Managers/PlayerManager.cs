﻿using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	[Header("Player Health Values")]
	[Range(0, 2)]
	private int playerHealth = 2;

	[Tooltip("The time it takes for the player to regenerate back to full health after taking damage, in seconds.")]
	[SerializeField]
	private float healthRegenTime = 10f;
	private float timer = 0f;

	[Header("Invulnerability Values")]
	[SerializeField]
	private float invulnerabilityTime = 1f;
	[SerializeField]
	private float invulnerabilityFlickerTime = .05f;
	private float flickerTimer = 0f;
	private bool isInvulnerable = false;
	private float invulnerableTimer = 0f;

	private SpriteRenderer playerSprite = null;

	[Header("Control Lockout Values")]
	private float controlLockoutTime = .5f;
	private float lockoutTimer = 0f;
	private bool isLockedOut = false;

	private Characterfinal controls = null;

	[Header("Collectible Values")]
	[SerializeField]
	private float happinessGain = 5f;

	[Header("Monster Values")]
	[SerializeField]
	private MonsterManager monster = null;
	
    void Awake()
    {
        controls = GetComponent<Characterfinal>();
        playerSprite = GetComponent<SpriteRenderer>();
    }
	private void Update() {
		if(playerHealth < 2 && playerHealth > 0) {
			timer += Time.deltaTime;
			monster.MonsterMovement(timer, healthRegenTime);
			if(timer >= healthRegenTime) {
				playerHealth++;
				playerHealth = Mathf.Clamp(playerHealth, 0, 2);
				timer = 0f;
			}
		}
		if (isInvulnerable) {
			invulnerableTimer += Time.deltaTime;
			flickerTimer += Time.deltaTime;
			if(flickerTimer >= invulnerabilityFlickerTime) {
				playerSprite.color = (playerSprite.color.a > 0) ? new Vector4(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0) : new Vector4(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1);
                flickerTimer = 0f;
			}
			if(invulnerableTimer >= invulnerabilityTime) {
				EndInvulnerability();
			}
		}
		if (isLockedOut) {
			lockoutTimer += Time.deltaTime;
			if(lockoutTimer >= controlLockoutTime) {
				EndControlLockout();
			}
		}
		if (Input.GetButtonDown("Fire1")) {
			Test();
		}
	}

	private void Test() {
		playerHealth--;
		playerHealth = Mathf.Clamp(playerHealth, 0, 2);
		if (playerHealth <= 0) {
			Death();
			return;
		}
		StartControlLockout();
		StartInvulnerabilityTime();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Obstacle" && !isInvulnerable) {
			playerHealth--;
			playerHealth = Mathf.Clamp(playerHealth, 0, 2);
			if(playerHealth <= 0) {
				Death();
				return;
			}
			StartControlLockout();
			StartInvulnerabilityTime();
		} else if(other.tag == "Collectible") {
			Destroy(other.gameObject);
			HappinessManager.AddToHappiness(happinessGain);
		}
	}

	private void StartInvulnerabilityTime() {
		isInvulnerable = true;
	}

	public void EndInvulnerability() {
		isInvulnerable = false;
		playerSprite.color = new Vector4(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1);
        invulnerableTimer = 0f;
		flickerTimer = 0f;
	}

	private void StartControlLockout() {
		isLockedOut = true;
		controls.enabled = false;
	}

	private void EndControlLockout() {
		isLockedOut = false;
		controls.enabled = true;
	}

	private void Death() {
		monster.DeathMonsterMovement();
	}

}
