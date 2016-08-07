using UnityEngine;
using System.Collections;

public class HappinessManager : MonoBehaviour {
	[Header("Happiness Decay Values")]
	[Tooltip("The starting happiness decay value.")]
	[SerializeField]
	private float happinessBaseDecay = 5f;
	[Tooltip("The maximum value of the happiness decay.")]
	[SerializeField]
	private float happinessMaxDecay = 20f;
	[Tooltip("Time for maximum happiness decay to happen, in seconds.")]
	[SerializeField]
	private float timeToMaxDecay = 30f;

	[Header("Happiness Recharge Values")]
	[SerializeField]
	private float happinessRecharge = 5f;

	private static float totalHappiness = 100f;
	private float timer = 0f;
	static public bool isHard = false;
    void Start()
    {
        ResetHappiness();
    }

	// Update is called once per frame
	void Update () {
		if (!isHard) {
			AddToHappiness(-1 * Time.deltaTime * (happinessBaseDecay + (happinessMaxDecay - happinessBaseDecay) * Mathf.Clamp(timer / timeToMaxDecay, 0f, 1f)));
			timer += Time.deltaTime;
			if(totalHappiness <= 0f) {
				isHard = true;
				timer = 0f;
			}
		} else {
			AddToHappiness(happinessRecharge * Time.deltaTime);
			if(totalHappiness >= 100f) {
				isHard = false;
			}
		}
		MeterManager.HappinessMeterValue(totalHappiness * .01f);
	}

	static public void AddToHappiness(float value) {
		totalHappiness += value;
		totalHappiness = Mathf.Clamp(totalHappiness, 0, 100f);
	}

    static public void ResetHappiness()
    {
        totalHappiness = 100f;
    }

	
}
