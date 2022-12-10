using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arm_Controller : MonoBehaviour
{
    public Slider baseSlider;
    public Slider armSlider;

    // slider value for base platform that goes from -1 to 1.
    public float baseSliderValue = 0.0f;

    // slider value for upper arm that goes from -1 to 1.
    public float upperArmSliderValue = 0.0f;

    // These slots are where you will plug in the appropriate arm parts into the inspector.
    public Transform robotBase;
    public Transform upperArm;

    // Allow us to have numbers to adjust in the inspector the speed of each part's rotation.
    public float baseTurnRate = 1.0f;
    public float upperArmTurnRate = 1.0f;

    private float baseYRot = 0.0f;
    public float baseYRotMin = -45.0f;
    public float baseYRotMax = 45.0f;

    private float upperArmXRot = 0.0f;
    public float upperArmXRotMin = -45.0f;
    public float upperArmXRotMax = 45.0f;

    void Start()
    {
        /* Set default values to that we can bring our UI sliders into negative values */
        baseSlider.minValue = -1;
        armSlider.minValue = -1;
        baseSlider.maxValue = 1;
        armSlider.maxValue = 1;
    }
    void CheckInput()
    {
        baseSliderValue = baseSlider.value;
        upperArmSliderValue = armSlider.value;
    }
    void ProcessMovement()
    {
        //rotating our base of the robot here around the Y axis and multiplying
        //the rotation by the slider's value and the turn rate for the base.
        baseYRot += baseSliderValue * baseTurnRate;
        baseYRot = Mathf.Clamp(baseYRot, baseYRotMin, baseYRotMax);
        robotBase.localEulerAngles = new Vector3(robotBase.localEulerAngles.x, baseYRot, robotBase.localEulerAngles.z);

        //rotating our upper arm of the robot here around the X axis and multiplying
        //the rotation by the slider's value and the turn rate for the upper arm.
        upperArmXRot += upperArmSliderValue * upperArmTurnRate;
        upperArmXRot = Mathf.Clamp(upperArmXRot, upperArmXRotMin, upperArmXRotMax);
        upperArm.localEulerAngles = new Vector3(upperArmXRot, upperArm.localEulerAngles.y, upperArm.localEulerAngles.z);
    }

    public void ResetSliders()
    {
        //resets the sliders back to 0 when you lift up on the mouse click down (snapping effect)
        baseSliderValue = 0.0f;
        upperArmSliderValue = 0.0f;
        baseSlider.value = 0.0f;
        armSlider.value = 0.0f;
    }

    void Update()
    {
        CheckInput();
        ProcessMovement();
    }
}
