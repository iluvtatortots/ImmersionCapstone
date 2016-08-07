using UnityEngine;
using System.Collections;

public class MonsterManager : MonoBehaviour {

	private float twoHealthPosition;
	[SerializeField]
	private float oneHealthPosition;
	[SerializeField]
	private float zeroHealthPosition;
	private AnimationCurve positionCurve = new AnimationCurve(new Keyframe(0,0), new Keyframe(1,1));

	private void Start() {
		twoHealthPosition = this.transform.position.x;
		positionCurve = AnimationCurve.Linear(0, oneHealthPosition, 1, twoHealthPosition);
    }

	public void MonsterMovement(float timeElapsed, float maxTime) {
		this.transform.position = new Vector2(positionCurve.Evaluate(timeElapsed / maxTime), this.transform.position.y);
	}

	public void DeathMonsterMovement() {
		this.transform.position = new Vector2(zeroHealthPosition, this.transform.position.y);
	}
}
