using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckTMPInputLength : MonoBehaviour
{
    [SerializeField] private TMP_InputField tmpInputFieldPassword;
    [SerializeField] private TMP_InputField tmpInputFieldName;

    [SerializeField] private TextMeshProUGUI warningTextError;

    private Coroutine resetCoroutine;


    void Start()
    {
        tmpInputFieldPassword.onValueChanged.AddListener(CheckLengthOfPassword);
        tmpInputFieldName.onValueChanged.AddListener(CheckLengthOfName);

       // warningTextError.gameObject.SetActive(false);
    }

    void CheckLengthOfPassword(string text)
    {
        if (text.Length == 11)
        {
            warningTextError.text = "Password must be no longer than 10 characters";
           // wait ();
           // warningTextError.gameObject.SetActive(true);
        }
        else
        {
            if(tmpInputFieldName.text.Length !=11) warningTextError.text = "";
            else warningTextError.text = "Username must be no longer than 10 characters";
            //warningTextError.gameObject.SetActive(false);
        }
    }
    void CheckLengthOfName(string text)
    {
        if (text.Length == 11)
        {
            warningTextError.text = "Username must be no longer than 10 characters";
           // wait ();
           // warningTextError.gameObject.SetActive(true);
        }
        else
        {
             if(tmpInputFieldPassword.text.Length !=11) warningTextError.text = "";
             else warningTextError.text = "Password must be no longer than 10 characters";
            //warningTextError.gameObject.SetActive(false);
        }
    }

    void wait (){
         if (resetCoroutine != null)
                StopCoroutine(resetCoroutine);

        resetCoroutine = StartCoroutine(Delay(3f));
        
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        warningTextError.text = "";
    }
}
