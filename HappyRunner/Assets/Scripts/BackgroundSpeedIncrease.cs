using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundSpeedIncrease : MonoBehaviour {

    [SerializeField]
    private float maxSpeedMultiplier = 1f;
    static public float MaxSpeedMultiplier;
    [SerializeField]
    private float baseSpeed = 3.8f;
    [SerializeField]
    private float baseSkySpeed = .06f;
    private List<AnimatedTexture> animations = new List<AnimatedTexture>();
    [SerializeField]
    private AnimatedTexture skyAnimation = null;
    [SerializeField]
    private float timeForMaxSpeed = 30f;
    static public float TimeForMaxSpeed = 30f;
    private float timer = 0f;
    static public float speedTimer;
    // Use this for initialization
    void Start()
    {
        foreach (AnimatedTexture textureAnimation in this.gameObject.GetComponentsInChildren<AnimatedTexture>())
        {
            animations.Add(textureAnimation);
        }
        speedTimer = timer;
        TimeForMaxSpeed = timeForMaxSpeed;
        MaxSpeedMultiplier = maxSpeedMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!HappinessManager.isHard)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, timeForMaxSpeed);
            foreach (AnimatedTexture texture in animations)
            {
                texture.speed = new Vector2(baseSpeed * (1f + maxSpeedMultiplier * (timer / timeForMaxSpeed)), texture.speed.y);
            }
            skyAnimation.speed = new Vector2(baseSkySpeed * (1f + maxSpeedMultiplier * (timer / timeForMaxSpeed)), skyAnimation.speed.y);
        }
        else {
            timer = 0f;
            foreach (AnimatedTexture texture in animations)
            {
                texture.speed = new Vector2(baseSpeed, texture.speed.y);
            }
            skyAnimation.speed = new Vector2(baseSkySpeed, skyAnimation.speed.y);
        }
        speedTimer = timer;
    }
}
