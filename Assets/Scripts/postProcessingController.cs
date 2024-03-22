using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class postProcessingController : MonoBehaviour
{
    [SerializeField] private Volume postProcessingVolume;
    [SerializeField] private bool isDisabled;

    [Header("Post Processing Profiles")]
    [SerializeField] private VolumeProfile postMainProfile;
    [SerializeField] private VolumeProfile postSecondaryProfile;

    public void MainPostProcessing()
    {
        postProcessingVolume.profile = postMainProfile;
    }

    public void SecondaryPostProcessing()
    {
        postProcessingVolume.profile = postSecondaryProfile;
    }

    public void DisablePostProcessing()
    {
        isDisabled = !isDisabled;
        postProcessingVolume.enabled = isDisabled;
    }
}
