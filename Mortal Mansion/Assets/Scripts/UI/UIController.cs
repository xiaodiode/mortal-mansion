using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator fadeText(List<TextMeshProUGUI> texts, float startAlpha, float targetAlpha, float duration){
        Color tempColor;
        
        float elapsedTime = 0;

        foreach(TextMeshProUGUI text in texts){

            while(elapsedTime <= duration){
                tempColor = text.color;
                tempColor.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime/duration);

                text.color = tempColor;

                yield return null;
            }
        }
    }
}
