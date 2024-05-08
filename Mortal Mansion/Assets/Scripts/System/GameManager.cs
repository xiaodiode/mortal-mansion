using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   

    [Header("System UI")]
    [SerializeField] private List<GameObject> systemUIObjects;

    // cinema
    [SerializeField] private List<GameObject> cinemaObjects;

    // main menu
    [SerializeField] private List<GameObject> mainMenuObjects;
    [SerializeField] private List<TextMeshProUGUI> mainMenuTexts; 
    [SerializeField] private List<float> mainMenuTextAlpha = new();
    [SerializeField] private List<Image> mainMenuIcons;
    [SerializeField] private List<float> mainMenuIconAlpha = new();
    [SerializeField] private float mm_UIFadeTime;
    [SerializeField] private float mm_UIMaxAlpha, mm_UIMinAlpha;
    [SerializeField] private RectTransform mainMenuBG;
    [SerializeField] private float mm_zoomTime;
    [SerializeField] private float mm_zoomMin, mm_zoomMax;
    [SerializeField] private bool mm_animReady = false;

    // post processing
    [SerializeField] private Volume systemVolume;
    [SerializeField] public Vignette systemVignette;


    [Header("Gameplay")]
    [SerializeField] private List<GameObject> gameplayObjects;
    [SerializeField] private Volume gameVolume;
    [SerializeField] public Vignette gameVignette;
    [SerializeField] public ArtifactUI artifactUI;


    [Header("Systems")]
    [SerializeField] private PostProcessing effects;
    [SerializeField] private UIController UI;
    [SerializeField] private CinemaController cinemaController;
    [SerializeField] public MouseController mouseController;

    // Start is called before the first frame update
    void Start()
    {
        
        gameVolume.profile.TryGet(out gameVignette);
        systemVolume.profile.TryGet(out systemVignette);

        // setupMainMenu();
        // playCinema();
        
        /* Comment out the lines below to only display game */
        // showMainMenu(); // debugging
        setActive(mainMenuObjects, false);
        setActive(gameplayObjects, true);
        playGame(); // debugging
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playCinema(){

        setActive(cinemaObjects, true);

        setActive(mainMenuObjects, false);

        setActive(gameplayObjects, false);

        cinemaController.startCinema();
    }

    public void setupMainMenu(){
        foreach(Image image in mainMenuIcons){
            mainMenuIconAlpha.Add(image.color.a);
        }

        foreach(TextMeshProUGUI text in mainMenuTexts){
            mainMenuTextAlpha.Add(text.color.a);
        }
    }

    public void showMainMenu(){

        cinemaController.resetCinema();

        setActive(cinemaObjects, false);

        setActive(mainMenuObjects, true);
        
        StartCoroutine(effects.updateVignette(systemVignette, effects.mm_maxIntensity, effects.mm_minIntensity, effects.mm_vignDuration));

        for(int i=0; i < mainMenuTexts.Count; i++){
            UI.fadeText(mainMenuTexts[i], mainMenuTextAlpha[i], mm_UIMaxAlpha, mm_UIFadeTime);
        }

        for(int i=0; i < mainMenuIcons.Count; i++){
            UI.fadeImage(mainMenuIcons[i], mainMenuIconAlpha[i], mm_UIMaxAlpha, mm_UIFadeTime);
        }

        mouseController.mouseLightOn = true;
    }

    public IEnumerator hideMainMenu(){
        // for(int i=0; i < mainMenuTexts.Count; i++){
        //     UI.fadeText(mainMenuTexts[i], mm_UIMaxAlpha, mainMenuTextAlpha[i], mm_UIFadeTime);
        // }

        // for(int i=0; i < mainMenuIcons.Count; i++){
        //     UI.fadeImage(mainMenuIcons[i], mm_UIMaxAlpha, mainMenuIconAlpha[i], mm_UIFadeTime);
        // }

        // yield return new WaitForSeconds(mm_UIFadeTime);

        // UI.zoomGameObject(mainMenuBG, mm_zoomMin, mm_zoomMax, mm_zoomTime);

        StartCoroutine(effects.updateVignette(systemVignette, effects.mm_minIntensity, effects.mm_maxIntensity, mm_zoomTime));

        yield return new WaitForSeconds(mm_zoomTime);

        mm_animReady = true;

    }

    public void resetMainMenu(){
        mainMenuBG.localScale = Vector3.one;
    }

    public void setActive(List<GameObject> objects, bool active){

        foreach(GameObject thing in objects){
            thing.SetActive(active);
            Debug.Log("object: " + thing + " - active? "  + active);
        }

    }

    public void pressPlay(){
        StartCoroutine(playGame());
    }

    public IEnumerator playGame(){

        StartCoroutine(hideMainMenu());

        while(!mm_animReady){
            yield return null;
        }

        mouseController.mouseLightOn = false;

        resetMainMenu();

        setActive(systemUIObjects, false);

        setActive(gameplayObjects, true);
    }


}
