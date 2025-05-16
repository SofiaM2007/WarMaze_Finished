using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoSpacesInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputFieldLoginNiknameText;
    [SerializeField] private TMP_InputField inputFieldLoginPassordText;
    [SerializeField] private TMP_InputField inputFieldSigninNiknameText;
    [SerializeField] private TMP_InputField inputFieldSinginPassordText;

    void Start()
    {
        inputFieldLoginNiknameText.onValueChanged.AddListener(RemoveSpaces1);
        inputFieldLoginPassordText.onValueChanged.AddListener(RemoveSpaces2);
        inputFieldSigninNiknameText.onValueChanged.AddListener(RemoveSpaces3);
        inputFieldSinginPassordText.onValueChanged.AddListener(RemoveSpaces4);
    }

    void RemoveSpaces1(string text)
    {
        string noSpaces = text.Replace(" ", "");
        if (text != noSpaces)
        {
            inputFieldLoginNiknameText.text = noSpaces;
        }
    }
    void RemoveSpaces2(string text)
    {
        string noSpaces = text.Replace(" ", "");
        if (text != noSpaces)
        {
            inputFieldLoginPassordText.text = noSpaces;
        }
    }
    void RemoveSpaces3(string text)
    {
        string noSpaces = text.Replace(" ", "");
        if (text != noSpaces)
        {
            inputFieldSigninNiknameText.text = noSpaces;
        }
    }
    void RemoveSpaces4(string text)
    {
        string noSpaces = text.Replace(" ", "");
        if (text != noSpaces)
        {
            inputFieldSinginPassordText.text = noSpaces;
        }
    }
}
