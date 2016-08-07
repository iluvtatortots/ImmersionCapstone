using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

    public GameObject[] platforms;
    public GameObject happyPrefab;
    public GameObject hellPrefab;
    private GameObject targetPrefab = null;
    public float delay = 2f;
    public bool active = true;
    public Vector2 delayRange = new Vector2(1, 2);
    public delegate void SpawnDelegate(InstantVelocity objectVelocity);
    public event SpawnDelegate OnSpawn;

    // private float platformWidth;
    private int platformSelector = 0;
    // private float[] platformWidths;

    // Use this for initialization

    [SerializeField]
    private bool affectedByHard = true;
    [SerializeField]
    private float hardModeSpeedMultiplier;

    // Update is called once per frame
    void Start () {

        ResetDelay();
        StartCoroutine(EnemyGenerator());
	}


    IEnumerator EnemyGenerator()
    {
        yield return new WaitForSeconds(delay);

        if (active)
        {
            if (HappinessManager.isHard)
            {
                targetPrefab = hellPrefab;
            }
            else
            {
               targetPrefab = happyPrefab;
            }
            

            GameObject newPlatform = (GameObject)Instantiate(targetPrefab, transform.position, transform.rotation);
            if (OnSpawn != null)
            {
                OnSpawn(newPlatform.GetComponent<InstantVelocity>());
            }

            ResetDelay();
        }
        StartCoroutine(EnemyGenerator());

    }
    void ResetDelay()
    {
        delay = Random.Range(delayRange.x, delayRange.y);
        if (HappinessManager.isHard && affectedByHard)
        {
            delay *= hardModeSpeedMultiplier;
        }
    }
}
