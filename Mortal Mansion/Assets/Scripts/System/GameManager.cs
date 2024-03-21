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
    [SerializeField] private List<Image> mainMenuIcons;
    [SerializeField] private float mm_textFadeTime;
    [SerializeField] private float mm_textMaxAlpha, mm_textMinAlpha;

    // post processing
    [SerializeField] private Volume systemVolume;
    [SerializeField] public Vignette systemVignette;


    [Header("Gameplay")]
    [SerializeField] private List<GameObject> gameplayObjects;
    [SerializeField] private Volume gameVolume;
    [SerializeField] public Vignette gameVignette;


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

        PlayCinema();
        // showMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayCinema(){

        setActive(cinemaObjects, true);

        setActive(mainMenuObjects, false);

        setActive(gameplayObjects, false);

        cinemaController.startCinema();
    }

    public void showMainMenu(){

        cinemaController.resetCinema();

        setActive(cinemaObjects, false);

        setActive(mainMenuObjects, true);
        
        StartCoroutine(effects.updateVignette(systemVignette, effects.mm_maxIntensity, effects.mm_minIntensity, effects.mm_vignDuration));

        UI.fadeText(mainMenuTexts, mm_textMinAlpha, mm_textMaxAlpha, mm_textFadeTime);
    }

    public void setActive(List<GameObject> objects, bool active){

        foreach(GameObject thing in objects){
            thing.SetActive(active);
        }
    }

    public void playGame(){

        setActive(systemUIObjects, false);

        setActive(gameplayObjects, true);
    }


}
