using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputModalWindow : ModalWindow<InputModalWindow>
{
    [SerializeField] private InputField inputField;

    private Action<string> onInputFieldDone;

    public new InputModalWindow Create(bool ignorable)
    {
        return ModalWindow<InputModalWindow>.Create(ignorable);
    }

    public InputModalWindow SetInputField(Action<string> onDone, string initialValue = "", string placeholderValue = "Type here")
    {
        inputField.text = initialValue;
        ((Text)inputField.placeholder).text = placeholderValue;
        onInputFieldDone = onDone;

        return this;
    }

    protected override void OnBeforeShow()
    {
        inputField.Select();
    }

    void SubmitInput()
    {
        onInputFieldDone?.Invoke(inputField.text);
        onInputFieldDone = null;
        Close();
    }

    private void Update()
    {
        if (inputField.isFocused && inputField.text != "" && Input.GetKeyUp(KeyCode.Return))
        {
            SubmitInput();
        }
    }

    public void UI_InputFieldOKButton()
    {
        SubmitInput();
    }
}
