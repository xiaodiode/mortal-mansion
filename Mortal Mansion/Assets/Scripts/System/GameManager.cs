using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private List<GameObject> mainMenuObjects;

    [Header("Cinema")]
    [SerializeField] private List<GameObject> cinemaObjects;

    [Header("System UI")]
    [SerializeField] private List<GameObject> systemUIObjects;

    [Header("Gameplay")]
    [SerializeField] private List<GameObject> gameplayObjects;


    [Header("Systems")]
    [SerializeField] private CinemaController cinemaController;
    [SerializeField] private MouseController mouseController;

    [SerializeField] private Volume volume;
    private Vignette vignette;

    [SerializeField] private float vignetteMM;

    // Start is called before the first frame update
    void Start()
    {
        
        // setActive(cinemaObjects, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cinemaFinished(){

        setActive(cinemaObjects, false);

        setActive(mainMenuObjects, true);

        volume.profile.TryGet(out vignette);
        
        vignette.intensity.Override(vignetteMM);
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
