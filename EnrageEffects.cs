using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class EnrageEffects : MonoBehaviour
{
    // properties of class
    public float endDist = 10f;
    public float startDist = 5f;
    public float ZoomOutSpeed;

    LensDistortion lensdist = null;

    private void Start()
    {
        // somewhere during initializing
        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out lensdist);

        lensdist.intensity.value = startDist;
    }
    private void Update()
    {
        if(lensdist.intensity.value > endDist)
        {
            lensdist.intensity.value -= ZoomOutSpeed;
        }
    }
}
