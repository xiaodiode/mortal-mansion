using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPrinter : MonoBehaviour
{
    [SerializeField] private bool isFinished;

    private string punctuation = ".?!";

    // Start is called before the first frame update
    void Start()
    {
        isFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator printToMonologue(string toPrint, float charSpeed, float pauseSpeed, TextMeshProUGUI monologueText){
        resetText(monologueText);
        
        // mouse.lockMouse(true);
        bool isPunctuation = false;
        foreach(char character in toPrint){

            monologueText.text += character;

            if(character.Equals(' ')){
                continue;
            }
            
            foreach(char period in punctuation){
                if(character == period){
                    isPunctuation = true;
                    break;
                }
            }

            if(isPunctuation){
                yield return new WaitForSeconds(charSpeed + pauseSpeed);
                isPunctuation = false;
                resetText(monologueText);
            }
            else{
                yield return new WaitForSeconds(charSpeed);
            }
            
        }

        isFinished = true;
    }

    private void resetText(TextMeshProUGUI monologueText){

        monologueText.text = "";
    }


}
