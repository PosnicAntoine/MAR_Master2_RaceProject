using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{

    public float speed;
    public float nightSpeedModifier;
    public float dayLightIntensity;
    public float nightLightIntensity;
    public bool isSpinning;

    private Transform t;
    private Light l;

    // Use this for initialization
    void Start()
    {
        t = GetComponent<Transform>();
        l = GetComponent<Light>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSpinning)
        {
            if(IsNight(t))
            {
                t.Rotate(speed * Time.deltaTime * nightSpeedModifier, 0, 0);
                l.intensity = nightLightIntensity;
            }
            else
            {
                t.Rotate(speed * Time.deltaTime, 0, 0);
                l.intensity = dayLightIntensity;
            }
        }

    }

    bool IsNight(Transform t)
    {
        return (t.eulerAngles.x < 0) || (t.eulerAngles.x > 180);
    }

}
