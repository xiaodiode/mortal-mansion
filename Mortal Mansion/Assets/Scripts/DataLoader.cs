using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Build.Content;

public class DataLoader : MonoBehaviour
{
    [Header("Text Files")]
    [SerializeField] private TextAsset artifactText;


    [Header("Data Structures")]

    [Space(10)]
    [Header("Lore")]
    [SerializeField] public bool artifactDataReady = false;
    [SerializeField] public Dictionary<string, string> artifactLore = new();


    private StringReader fileReader;
    private string fileLine;
    private string subject = "";
    private string content = "";

    // Start is called before the first frame update
    void Start()
    {
        parseArtifactData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void parseArtifactData(){

        fileReader = new StringReader(artifactText.text);

        while((fileLine = fileReader.ReadLine()) != null){
            fileLine = fileLine.Trim();

            if(fileLine.Contains("writer")){

                subject = fileLine.Replace("writer", "").Trim();
                // Debug.Log("dataSection: " + dataSection);
            }
            else if(fileLine.Contains("content")){
                content = fileLine.Replace("content", "").Trim();

                artifactLore.Add(subject, content);
            }

        }

        debugDictionary(false, null, artifactLore);

        artifactDataReady = true;
    }

    private void debugDictionary(bool valIsList, Dictionary<string, List<string>> dictWithList, Dictionary<string, string> dictWithString){
        if(valIsList){
            foreach(KeyValuePair<string, List<string>> pair in dictWithList){
                foreach(string val in pair.Value){
                    Debug.Log("val name: " + pair.Key + " | comment: " + val);
                }
            }
        }
        else{
            foreach(KeyValuePair<string, string> pair in dictWithString){
                Debug.Log("val name: " + pair.Key + " | comment: " + pair.Value);
            }
        }

    }
}
