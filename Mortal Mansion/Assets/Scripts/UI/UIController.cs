using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public IEnumerator fadeText(TextMeshProUGUI text, float startAlpha, float targetAlpha, float duration){
        Color tempColor;
        
        float elapsedTime = 0;

        while(elapsedTime <= duration){
            tempColor = text.color;
            tempColor.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime/duration);

            text.color = tempColor;

            yield return null;
        }
    }

    public IEnumerator fadeImage(Image image, float startAlpha, float targetAlpha, float duration){
        Color tempColor; 

        float elapsedTime = 0;
                
        while(elapsedTime <= duration){
            tempColor = image.color;
            tempColor.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime/duration);

            image.color = tempColor;

            yield return null;
        }
    }

    public IEnumerator zoomGameObject(RectTransform zoomableObject, float startZoom, float targetZoom, float duration){
        float elpasedTime = 0;
        Vector3 start = Vector3.zero;
        Vector3 target = Vector3.zero;

        start.x = startZoom; start.y = startZoom; start.z = startZoom;
        target.x = targetZoom; target.y = targetZoom; target.z = targetZoom;

        while(elpasedTime <= duration){

            zoomableObject.localScale = Vector3.Lerp(start, target, elpasedTime/duration);

            elpasedTime += Time.deltaTime;

            yield return null;
        }

    }
}
