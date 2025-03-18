using UnityEngine;

public class ChangeSize : MonoBehaviour
{
    [SerializeField] private float modifiedScale = 2f;
    [SerializeField] private float changeRate = 5f;
    [SerializeField] private float targetLightIntensity = 2f; // Target light intensity
    [SerializeField] private float targetNearPlane = 10f; // Target range for the point light

    private Vector3 initialScale;
    private bool isScaled = false;

    private Light childLight = null; // Reference to the point light

    private float initialLightIntensity = 1f; // Initial intensity of the light
    private float initialNearPlane = 5f; // Initial range of the point light

    private float scaleLerpFactor = 0f; // Factor to interpolate between initial and target scale

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        FindPointLightInChildren();
        
        if (childLight != null)
        {
            initialLightIntensity = childLight.intensity;
            initialNearPlane = childLight.shadowNearPlane;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Interpolate the scale based on the change rate
        float targetScale = isScaled ? modifiedScale : initialScale.x;
        scaleLerpFactor = Mathf.MoveTowards(scaleLerpFactor, isScaled ? 1f : 0f, changeRate * Time.deltaTime);

        // Apply the interpolated scale to the object
        transform.localScale = Vector3.Lerp(initialScale, Vector3.one * targetScale, scaleLerpFactor);

        // If a point light is found, update its intensity and range
        if (childLight != null)
        {
            childLight.intensity = Mathf.Lerp(initialLightIntensity, targetLightIntensity, scaleLerpFactor);
            childLight.shadowNearPlane = Mathf.Lerp(initialNearPlane, targetNearPlane, scaleLerpFactor);
        }
    }

    // Get the target scale based on the isScaled flag
    private Vector3 GetTargetScale()
    {
        return isScaled ? Vector3.one * modifiedScale : initialScale;
    }

    // Get the target light intensity based on the isScaled flag
    private float GetTargetLightIntensity()
    {
        return isScaled ? targetLightIntensity : initialLightIntensity; // Default intensity is the initial value if not scaled
    }

    // Get the target light range based on the isScaled flag
    private float GetTargetNearPlane()
    {
        return isScaled ? targetNearPlane : initialNearPlane; // Default range is the initial value if not scaled
    }

    // Toggles the scaling effect
    public void ToggleScale()
    {
        isScaled = !isScaled;
    }

    // Find a point light in the child objects
    private void FindPointLightInChildren()
    {
        // Look through all child objects to find a Light component
        foreach (Transform child in transform)
        {
            Light light = child.GetComponent<Light>();
            if (light != null && light.type == LightType.Point)
            {
                childLight = light;
                break; // Exit once the first point light is found
            }
        }
    }
}


//using UnityEngine;
//
//public class ChangeSize : MonoBehaviour
//{
//    [SerializeField] private float modifiedScale = 2f;
//    [SerializeField] private float changeRate = 5f;
//
//    private Vector3 initialScale;
//    private bool isScaled = false;
//    // Start is called before the first frame update
//    void Start()
//    {
//        initialScale = transform.localScale;
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        transform.localScale = Vector3.MoveTowards(transform.localScale, GetTargetScale(), changeRate * Time.deltaTime);
//    }
//
//    private Vector3 GetTargetScale()
//    {
//        return isScaled ? Vector3.one * modifiedScale : initialScale;
//    }
//
//    public void ToggleScale()
//    {
//        isScaled = !isScaled;
//    }
//}
//