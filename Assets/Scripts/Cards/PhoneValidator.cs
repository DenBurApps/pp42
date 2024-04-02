using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneValidator : MonoBehaviour
{
    private TMP_InputField _phoneInput;

    private void Awake()
    {
        _phoneInput = GetComponent<TMP_InputField>();
        _phoneInput.onEndEdit.AddListener(OnNumberEndEdit);
    }

    private void OnNumberEndEdit(string number)
    {
        string cleanInput = System.Text.RegularExpressions.Regex.Replace(number, "[^0-9]", "");

        if (cleanInput == "")
        {
            _phoneInput.text = "";
            return;
        }
        // Если строка не начинается с "+", добавьте его
        if (!cleanInput.StartsWith("+"))
        {
            cleanInput = "+" + cleanInput;
        }
        _phoneInput.text = cleanInput;
    }

}
