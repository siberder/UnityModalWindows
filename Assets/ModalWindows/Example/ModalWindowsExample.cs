using UnityEngine;

public class ModalWindowsExample : MonoBehaviour
{
    public void CreateSimpleModal()
    {
        SimpleModalWindow.Create()
                   .SetHeader("Simple Modal")
                   .SetBody("Here goes body")
                   .Show();
    }

    public void CreateModalWithButtons()
    {
        SimpleModalWindow.Create()
                   .SetHeader("Modal With Buttons")
                   .SetBody("Here goes body")
                   .AddButton("Yes", () => print("Yes pressed"), ModalButtonType.Success)
                   .AddButton("No", () => print("No pressed"), ModalButtonType.Danger)
                   .AddButton("Cancel")
                   .Show();
    }

    public void CreateNonIgnorableModal()
    {
        SimpleModalWindow.Create(false)
                   .SetHeader("Modal With Buttons")
                   .SetBody("You must press one of the buttons")
                   .AddButton("Yes", () => print("Yes pressed"), ModalButtonType.Success)
                   .AddButton("No", () => print("No pressed"), ModalButtonType.Danger)
                   .Show();
    }

    public void CreateInputModal()
    {
        InputModalWindow.Create()
                   .SetHeader("Input Field Modal")
                   .SetBody("Enter something:")
                   .SetInputField((inputResult) => print("Text: " + inputResult), "Initial value", "It is a placeholder")
                   .Show();
    }

    public void CreateToastModal()
    {
        ToastModalWindow.Create(ignorable: true)
                        .SetHeader("Hey!")
                        .SetBody("Hello there! This is a toast modal window.")
                        .SetDelay(3f) // Set it to 0 to make popup persistent
                        //.SetIcon(sprite) // Also you can set icon
                        .Show();
    }
}
