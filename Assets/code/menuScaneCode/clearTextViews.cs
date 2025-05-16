using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class clearTextViews : MonoBehaviour
{
    
    [SerializeField] private TMP_InputField inputFieldLoginName;
    [SerializeField] private TMP_InputField inputFieldLoginPassword;
    [SerializeField] private TMP_InputField inputFieldSigninName;
    [SerializeField] private TMP_InputField inputFieldSigninPassword;

    [SerializeField] private TMP_InputField inputFieldSearch;

    [SerializeField] private TextMeshProUGUI TextForErrorLogin;
     [SerializeField] private TextMeshProUGUI TextForErrorSignin;


    public void ClearField()
    {
        inputFieldLoginName.text = "";
        inputFieldLoginPassword.text = "";
        inputFieldSigninName.text = "";
        inputFieldSigninPassword.text = "";

        inputFieldSearch.text = "";
        
        TextForErrorLogin.text = "";
        TextForErrorSignin.text = "";
    }
}
