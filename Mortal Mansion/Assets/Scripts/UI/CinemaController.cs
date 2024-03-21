using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class CinemaController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Header("Cinema Structure")]
    [SerializeField] private RawImage rawImage;
    [SerializeField] private float baseWidth;
    [SerializeField] private List<Texture2D> scenes = new();
    [SerializeField] private List<float> sceneWidths = new();

    [Header("Cinema Animation Effects")]
    [SerializeField] private PostProcessing effects;
    [SerializeField] private Volume volume;
    [SerializeField] private float fadePercentTime;
    [SerializeField] private float baseSceneTime;
    [SerializeField] private List<float> moveTimes = new();
    [SerializeField] private float transitionTime;

    [Header("Cinema Lore")]
    [SerializeField] private DataLoader data;
    [SerializeField] private TextPrinter textPrinter;
    [SerializeField] private TextMeshProUGUI cinemaText;
    [SerializeField] private float sentenceDelay;
    [SerializeField] private float charDelay;
    [SerializeField] public bool cinemaFinished;

    [Header("Cinema Skip Button")]
    [SerializeField] private Button skipButton;
    [SerializeField] private RawImage skipIcon;
    [SerializeField] private float skipFadeTime;

    private Color tempColor1 = new();
    private Color tempColor2 = new();
    private float zeroAlpha = 0.0f; private float maxSkipAlpha = 0.15f; private float maxTextAlpha = 0.8f;

    private string punctuation = ".?!";
    private bool setupReady, updateSceneReady, transitionReady;
    private Vector2 newWidth;
    private Vector2 targetPos;
    private bool lockCinema;


    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setupText(){
        tempColor2 = cinemaText.color;
        tempColor2.a = maxTextAlpha;

        cinemaText.color = tempColor2;

        cinemaText.text = "";
    }

    private IEnumerator showSkip(){

        gameManager.mouseController.hideMouse(true);

        // make skip button invisible and non-interactable
        tempColor1 = skipIcon.color;
        tempColor1.a = zeroAlpha;
        
        skipIcon.color = tempColor1;

        skipButton.interactable = false;

        yield return new WaitForSeconds(4);

        skipButton.interactable = true;

        float elapsedTime = 0;

        while(elapsedTime <= skipFadeTime){
            tempColor1.a = Mathf.Lerp(zeroAlpha, maxSkipAlpha, elapsedTime/skipFadeTime);

            skipIcon.color = tempColor1;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        gameManager.mouseController.hideMouse(false);
    }

    public void pressSkip(){

        StartCoroutine(actuallySkip());
    }
    public IEnumerator actuallySkip(){
        // bool fadeReady = false; 

        skipButton.interactable = false;

        float elapsedTime = 0;
        float currIntensity = gameManager.systemVignette.intensity.value;

        // fade out cinema text and skip button text
        while(elapsedTime <= effects.cin_vignDuration){

            if(elapsedTime < skipFadeTime){
                tempColor1.a = Mathf.Lerp(maxSkipAlpha, zeroAlpha, elapsedTime/skipFadeTime);
                tempColor2.a = Mathf.Lerp(maxTextAlpha, zeroAlpha, elapsedTime/skipFadeTime);

            }
            if(elapsedTime < effects.cin_vignDuration){
                gameManager.systemVignette.intensity.Override(Mathf.Lerp(currIntensity, effects.cin_maxIntensity, elapsedTime/effects.cin_vignDuration));
            }
            
            skipIcon.color = tempColor1;
            cinemaText.color = tempColor2;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        lockCinema = true;
        
    }

    public void startCinema(){
        setupReady = false;
        updateSceneReady = false;
        transitionReady = false;
        cinemaFinished = false;

        lockCinema = false;

        setupText();

        effects.updateVignette(gameManager.systemVignette, effects.cin_maxIntensity);

        StartCoroutine(showSkip());

        StartCoroutine(setupCinema());
        StartCoroutine(playCinema());
    }

    public void resetCinema(){

        rawImage.rectTransform.anchoredPosition = Vector2.zero;

        updateSceneReady = false;
        transitionReady = false;
        cinemaFinished = false;
        lockCinema = false;
    }

    private IEnumerator playCinema(){

        while(!setupReady){
            yield return null;
        }

        for(int i=0; i<scenes.Count; i++){

            if(lockCinema){
                break;
            }

            updateSceneReady = false;
            transitionReady = false;

            StartCoroutine(updateScene(scenes[i], sceneWidths[i], moveTimes[i]));

            yield return new WaitForSeconds(2);

            StartCoroutine(textPrinter.printToMonologue(data.cinemaLore[i], charDelay, sentenceDelay, cinemaText));

            while(!updateSceneReady){
                yield return null;
            }

            StartCoroutine(transitionScene());

            while(!transitionReady){
                yield return null;
            }
        }

        gameManager.showMainMenu();
    }

    private IEnumerator updateScene(Texture2D background, float sceneWidth, float moveTime){

        float elapsedTime = 0;
        float fadeTime = moveTime*fadePercentTime;

        // update current scene background
        rawImage.texture = background;

        // adjust the width of the panoramic scene in relation to length of respective script
        newWidth = rawImage.rectTransform.offsetMax; 
        newWidth.x = sceneWidth;

        rawImage.rectTransform.offsetMax = newWidth;

        targetPos = Vector2.zero;
        targetPos.x = baseWidth - sceneWidth;



        // moving from the leftmost edge of the panoramic scene to the rightmost edge within moveTime
        while(elapsedTime <= moveTime && !lockCinema){

            // fading into new scene
            if(elapsedTime <= fadeTime){
                gameManager.systemVignette.intensity.Override(Mathf.Lerp(effects.cin_maxIntensity, effects.cin_minIntensity, elapsedTime/fadeTime));
            }

            // fading out of scene near the end of movement
            else if(fadeTime > moveTime - elapsedTime){
                gameManager.systemVignette.intensity.Override(Mathf.Lerp(effects.cin_minIntensity, effects.cin_maxIntensity, (fadeTime - (moveTime - elapsedTime))/fadeTime));
            }

            // moving panoramic scene to the left
            rawImage.rectTransform.anchoredPosition = Vector2.Lerp(Vector2.zero, targetPos, elapsedTime/moveTime);
            // rawImage.rectTransform.anchoredPosition = Vector2.MoveTowards(rawImage.rectTransform.anchoredPosition, targetPos, speed*Time.deltaTime);
            
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        updateSceneReady = true;
    }

    private IEnumerator transitionScene(){
        // if(!lockCinema){
            rawImage.rectTransform.anchoredPosition = Vector2.zero;

            yield return new WaitForSeconds(transitionTime);    

            transitionReady = true;    
        // }
        
    }

    private IEnumerator setupCinema(){
        while(!data.cinemaDataReady){
            yield return null;
        }

        int scriptLength;
        int sentenceNum;

        float printDelay;
        float newWidth;

        for(int i=0; i<scenes.Count; i++){
            scriptLength = data.cinemaLore[i].Length;
            sentenceNum = countSentences(data.cinemaLore[i]);

            printDelay = Mathf.Max(scriptLength*charDelay + sentenceNum*sentenceDelay, baseSceneTime);
            // totalDelay = printDelay + baseSceneTime;

            // moveTimes.Add(totalDelay);
            moveTimes.Add(printDelay);


            // newWidth = totalDelay * (baseWidth/baseSceneTime) * Time.deltaTime;
            newWidth = Mathf.Max(printDelay * (baseWidth/baseSceneTime), baseWidth);

            sceneWidths.Add(newWidth);
        }

        setupReady = true;
    }

    private int countSentences(string script){
        int count = 0; 

        foreach(char val in punctuation){
            count += script.Count(c => c == val);
        }

        return count;
    }
}
