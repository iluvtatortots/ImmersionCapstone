using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeterManager : MonoBehaviour {

	public Image HappinessMeter;
	static private Image happinessMeter;

	private void Start() {
		happinessMeter = HappinessMeter;
	}

	private void Update() {
		HappinessMeterValue(happinessMeter.fillAmount - Input.GetAxisRaw("Vertical") * .01f);
	}

	static public void HappinessMeterValue(float value) {
		value = Mathf.Clamp(value, 0f, 1f);
		happinessMeter.fillAmount = value;
	}
}
