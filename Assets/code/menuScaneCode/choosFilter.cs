using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class choosFilter : MonoBehaviour
{
    
    private shopScroll shopScroll;
    private fromDataBase fromDataBase;
    private AccountManager AccountManager;

    public float filter = 0;
    public Image[] buttonImages; 
    public TextMeshProUGUI[] buttonText; 


     public float pulseDuration = 0.3f;
    public float pulseScale = 1.2f;
    public int pulseCount = 3;

    private Vector3 originalScale;

    public void Start(){
        shopScroll=FindObjectOfType<shopScroll>();
        fromDataBase=FindObjectOfType<fromDataBase>();
        AccountManager = FindObjectOfType<AccountManager>();

        //originalScale = fromDataBase.TextMyMoney.transform.localScale;

        for(int i = 0; i < 13; i++){
            int isBought = (1<<(13-i-1))&fromDataBase.boughtCostume;
            if(isBought!=0){buttonImages[i].enabled = false; buttonText[i].enabled = false;}// Destroy(buttonImages[i]); Destroy(buttonText[i]);}
            else {buttonImages[i].enabled = true; buttonText[i].enabled = true;}
        }

        originalScale = fromDataBase.TextMyMoney.transform.localScale;
    }

   // чтоб показать что денег нет(анимеция)
    System.Collections.IEnumerator PulseText()
    {
        Debug.Log("pulsing " + pulseCount);
        for (int i = 0; i < pulseCount; i++)
        {
            yield return ScaleText(pulseScale);
            yield return ScaleText(1f);
        }

        fromDataBase.TextMyMoney.transform.localScale = originalScale;
    }

    System.Collections.IEnumerator ScaleText(float targetScale)
    {
        float elapsed = 0f;
        Vector3 start = fromDataBase.TextMyMoney.transform.localScale;
        Vector3 end = originalScale * targetScale; 

        while (elapsed < pulseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / pulseDuration;
            fromDataBase.TextMyMoney.transform.localScale = Vector3.Lerp(start, end, t);
            yield return null;
        }

        fromDataBase.TextMyMoney.transform.localScale = end;
    }

    

    public void chooseFilter()
    {
        switch(shopScroll.currentIndex){
            case 0:
                filter=-30;
                break;
            case 1:
                filter=-60;
                break;
            case 2:
                filter=-90;
                break;
            case 3:
                filter=-120;
                break;
            case 4:
                filter=-150;
                break;
            case 5:
                filter=-180;
                break;
            case 6:
                filter=0;
                break;
            case 7:
                filter=30;
                break;
            case 8:
                filter=60;
                break;
            case 9:
                filter=90;
                break;
            case 10:
                filter=106;
                break;
            case 11:
                filter=120;
                break;
            case 12:
                filter=150;
                break;
        }

        int isBought = (1<<(13-shopScroll.currentIndex-1))&fromDataBase.boughtCostume;
        if(isBought==0) filter=0;

    }

    public void buy()
    {

        if(buttonText[shopScroll.currentIndex].enabled  && fromDataBase.myMoney>=10){

            buttonImages[shopScroll.currentIndex].enabled = false; buttonText[shopScroll.currentIndex].enabled = false;

            fromDataBase.boughtCostume=fromDataBase.boughtCostume|(1<<(13-shopScroll.currentIndex-1));

            fromDataBase.myMoney-=10;
            fromDataBase.saveMyBallance();
            fromDataBase.updateTextView();
            AccountManager.Load();
        }
        else if(buttonText[shopScroll.currentIndex].enabled){
          Time.timeScale = 1f;
            StartCoroutine(PulseText());
        }
    }
}