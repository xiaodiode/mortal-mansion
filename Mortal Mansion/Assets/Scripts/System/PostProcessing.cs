using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PostProcessing : MonoBehaviour
{
    [Header("Cinema Settings")]
    [SerializeField] [Range(0.5f, 1f)] public float cin_maxIntensity; //0.6
    [SerializeField] [Range(0, 0.49f)] public float cin_minIntensity; //0.12
    [SerializeField] public float cin_vignDuration;


    [Header("Main Menu Settings")]
    [SerializeField] [Range(0.5f, 1f)] public float mm_maxIntensity; 
    [SerializeField] [Range(0, 0.49f)] public float mm_minIntensity; //0.14
    [SerializeField] public float mm_vignDuration;


    // private Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator updateVignette(Vignette vignette, float initialIntensity, float targetIntensity, float duration){

        float elapsedTime = 0;

        while(elapsedTime <= duration){

            vignette.intensity.Override(Mathf.Lerp(initialIntensity, targetIntensity, elapsedTime/duration));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void updateVignette(Vignette vignette, float intensity){
        
        vignette.intensity.Override(intensity);
    }
}
