using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    [SerializeField] public int totalCoins;
    [SerializeField] public int coinsCollected;


    [SerializeField] private TextMeshProUGUI coinUI;

    private string coinText;

    // Start is called before the first frame update
    void Start()
    {
        coinsCollected = 0;
       
        updateCoinsUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void coinCollected(){
        coinsCollected++;
        updateCoinsUI();
    }

    public void updateCoinsUI(){
        coinText = coinsCollected.ToString() + " / " + totalCoins.ToString();
        coinUI.text = coinText;
    }

    
}
