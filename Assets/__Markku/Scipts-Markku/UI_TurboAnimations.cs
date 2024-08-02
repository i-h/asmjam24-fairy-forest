using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * -------------Unity 2022.2.5f1 -------------------<3
 * This script provides three types of animations:
 * 1. Rotation: An animation that rotates the component around the Z-axis.
 * 2. Scaling: Pulsating scaling that changes the size of the component over time following a defined curve.
 * 3. Motion: An animation that moves the component along the X and Y axes using animation curves to control movement.
 * Animations can be enabled or disabled individually, and their speed can be adjusted.
 * This script is designed for use with the RectTransform component, which allows for animating UI elements.
 * You can freely use this code in your own projects without needing to mention me.
 *-------------Markku Hietamäki 2023----------------  
 */

public class UI_TurboAnimations : MonoBehaviour
{
    [Header("On/Off")]
    [Tooltip("Activate or deactivate the animation.")]
    public bool animationActive = true;

    [Header("Rotation Settings")]
    [Tooltip("Activate or deactivate rotation animation.")]
    public bool rotationActive = true;
    [Tooltip("Maximum rotation degree on the Z-axis.")]
    public float maxRotation = 5.0f;
    [Tooltip("Speed of the rotation animation.")]
    public float rotationSpeed = 1.0f;

    [Header("Scaling Settings")]
    [Tooltip("Activate or deactivate pulsating scaling.")]
    public bool scalingActive = true;
    [Tooltip("Curve of the scaling animation.")]
    public AnimationCurve scalingCurve;
    [Tooltip("Speed of the scaling animation.")]
    public float scalingSpeed = 1.0f;

    [Header("Motion Animation Settings")]
    [Tooltip("Activate or deactivate motion animation.")]
    public bool motionAnimationActive = true;
    [Tooltip("Motion animation curve on the X-axis.")]
    public AnimationCurve motionXCurve;
    [Tooltip("Motion animation curve on the Y-axis.")]
    public AnimationCurve motionYCurve;
    [Tooltip("Speed of the motion animation.")]
    public float motionSpeed = 1.0f;

    private Vector3 originalPosition;
    private Vector3 originalScale;
    private float scalingTime = 0.0f;
    private float motionTime = 0.0f;
    private RectTransform rectTransform;
    private Vector2 originalAnchorPosition;

    void Start()
    {
        
        rectTransform = GetComponent<RectTransform>();
        originalAnchorPosition = rectTransform.anchoredPosition;
        originalScale = rectTransform.localScale;
    }

    void Update()
    {
        if (!animationActive) return;

        if (rotationActive)
        {
            float zRotation = Mathf.Sin(Time.time * rotationSpeed) * maxRotation;
            rectTransform.rotation = Quaternion.Euler(0, 0, zRotation);
        }

        if (scalingActive)
        {
            scalingTime += Time.deltaTime * scalingSpeed;
            float scale = scalingCurve.Evaluate(scalingTime);
            rectTransform.localScale = originalScale * scale;
        }

        if (motionAnimationActive)
        {
            motionTime += Time.deltaTime * motionSpeed;
            float motionX = motionXCurve.Evaluate(motionTime);
            float motionY = motionYCurve.Evaluate(motionTime);
            rectTransform.anchoredPosition = originalAnchorPosition + new Vector2(motionX, motionY);
        }
    }
}