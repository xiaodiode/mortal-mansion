using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using System.Runtime.CompilerServices;

public class CinemaController : MonoBehaviour
{
    [Header("Cinema Structure")]
    [SerializeField] private RawImage rawImage;
    [SerializeField] private float baseWidth;
    [SerializeField] private List<Texture2D> scenes = new();
    [SerializeField] private List<float> sceneWidths = new();
    [SerializeField] private int currSceneNum;

    [Header("Cinema Animation Effects")]
    [SerializeField] private Volume volume;
    [SerializeField] private Vignette vignette;
    [SerializeField] [Range(0.5f, 1f)] private float maxVignette;
    [SerializeField] [Range(0, 0.49f)] private float minVignette;
    [SerializeField] private float fadeTime;
    [SerializeField] private float baseSceneTime;
    [SerializeField] private List<float> moveTimes = new();
    [SerializeField] private float transitionTime;

    [Header("Cinema Lore")]
    [SerializeField] private DataLoader data;
    [SerializeField] private TextMeshProUGUI cinemaText;
    [SerializeField] private float sentenceDelay;
    [SerializeField] private float charDelay;

    private string punctuation = ".?!";
    private bool setupReady, updateSceneReady, transitionReady;
    private Vector2 newWidth;
    private Vector2 currPos, targetPos;

    // Start is called before the first frame update
    void Start()
    {
        currSceneNum = 0;

        setupReady = false;
        updateSceneReady = false;
        transitionReady = false;

        volume.profile.TryGet(out Vignette vignette);
        
        vignette.intensity.Override(maxVignette);
        

        

        StartCoroutine(playCinema());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator playCinema(){
        StartCoroutine(setupCinema());

        while(!setupReady){
            yield return null;
        }

        

        for(int i=0; i<scenes.Count; i++){
            updateSceneReady = false;
            transitionReady = false;

            StartCoroutine(updateScene(scenes[i], sceneWidths[i], moveTimes[i]));

            while(!updateSceneReady){
                yield return null;
            }

            StartCoroutine(transitionScene());

            while(!transitionReady){
                yield return null;
            }
        }

        yield return null;
    }

    private IEnumerator updateScene(Texture2D background, float sceneWidth, float moveTime){
        volume.profile.TryGet(out Vignette vignette);

        float elapsedTime = 0;

        // volume.profile.TryGet(out Vignette vignette);

        // update current scene background
        rawImage.texture = background;

        // adjust the width of the panoramic scene in relation to length of respective script
        newWidth = rawImage.rectTransform.offsetMax; 
        newWidth.x = sceneWidth;

        rawImage.rectTransform.offsetMax = newWidth;

        targetPos = Vector2.zero;
        targetPos.x = baseWidth - sceneWidth;

        // moving from the leftmost edge of the panoramic scene to the rightmost edge within moveTime
        while(elapsedTime < moveTime){

            // fading into new scene
            if(elapsedTime < fadeTime){
                vignette.intensity.Override(Mathf.Lerp(maxVignette, minVignette, elapsedTime/fadeTime));
            }

            // fading out of scene near the end of movement
            else if(fadeTime > moveTime - elapsedTime){
                vignette.intensity.Override(Mathf.Lerp(minVignette, maxVignette, (fadeTime - (moveTime - elapsedTime))/fadeTime));
            }

            // moving panoramic scene to the left
            rawImage.rectTransform.anchoredPosition = Vector2.Lerp(Vector2.zero, targetPos, elapsedTime/moveTime);
            
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        updateSceneReady = true;
    }

    private IEnumerator transitionScene(){
        rawImage.rectTransform.anchoredPosition = Vector2.zero;

        yield return new WaitForSeconds(transitionTime);    

        transitionReady = true;
    }

    private IEnumerator setupCinema(){
        while(!data.cinemaDataReady){
            yield return null;
        }

        int scriptLength;
        int sentenceNum;

        float printDelay, totalDelay;
        float newWidth;

        for(int i=0; i<scenes.Count; i++){
            scriptLength = data.cinemaLore[i].Length;
            sentenceNum = countSentences(data.cinemaLore[i]);

            printDelay = scriptLength*charDelay + sentenceNum*sentenceDelay;
            totalDelay = printDelay + baseSceneTime;

            moveTimes.Add(totalDelay);


            newWidth = (totalDelay/baseSceneTime) * baseWidth;

            sceneWidths.Add(newWidth);
        }

        setupReady = true;
    }

    private int countSentences(string script){
        int count = 0; 

        foreach(char val in punctuation){
            count += punctuation.Count(c => c == val);
        }

        return count;
    }
}
