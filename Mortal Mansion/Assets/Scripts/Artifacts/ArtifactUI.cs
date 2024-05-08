using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArtifactUI : MonoBehaviour
{
    [SerializeField] public GameObject artifactUI;
    [SerializeField] public bool artifactDisplayed;
    [SerializeField] private DataLoader data;
    [SerializeField] private TextMeshProUGUI artifactDate;
    [SerializeField] private TextMeshProUGUI artifactContent;
    [SerializeField] private TextMeshProUGUI artifactWriter;
    [SerializeField] private int currArtifactIndex;
    [SerializeField] public int maxArtifactEntries;
    // Start is called before the first frame update
    void Start()
    {
        artifactDisplayed = false;

        currArtifactIndex = 0;

        StartCoroutine(setupArtifact());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator setupArtifact(){
        while(!data.artifactDataReady){
            yield return null;
        }

        artifactDate.text = data.artifactDate[currArtifactIndex];
        artifactContent.text = data.artifactContent[currArtifactIndex];
        artifactWriter.text = data.artifactWriter[currArtifactIndex];

        maxArtifactEntries = data.artifactDate.Count;

        displayArtifact(false);
    }

    public void toggleArtifact(){
        artifactDisplayed = !artifactDisplayed;

        displayArtifact(artifactDisplayed);
    }

    public void displayArtifact(bool enable){
        artifactUI.SetActive(enable);
    }

    public void updateArtifact(){
        if(currArtifactIndex + 1 != maxArtifactEntries){
            currArtifactIndex++;

            artifactDate.text = data.artifactDate[currArtifactIndex];
            artifactContent.text = data.artifactContent[currArtifactIndex];
            artifactWriter.text = data.artifactWriter[currArtifactIndex];
        }
        
    }
}
