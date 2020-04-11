# UnityModalWindows
Simple system for creating modal windows with callbacks in Unity, with extensible template.

# In action
### Simple modal windows
This code

```C#
SimpleModalWindow.Create()
                 .SetHeader("Simple Modal")
                 .SetBody("Here goes body")
                 .Show();
```

Gives you this simple dismissable modal window:

![Preview](/images/example1.gif)

You can also add buttons with callbacks and make modal window not dismissable:

```C#
SimpleModalWindow.Create(ignorable: false)
                 .SetHeader("Modal With Buttons")
                 .SetBody("You must press one of the buttons")
                 .AddButton("Yes", () => print("Yes pressed"), ModalButtonType.Success)
                 .AddButton("No", () => print("No pressed"), ModalButtonType.Danger)
                 .Show();
```

Which will lead you to this result:

![Preview](/images/example2.gif)

### Custom modals

You can make your own modal windows, for example, modal with input field creation simply as following:

1. Create script and inherit it from `ModalWindow<T>`
```C#
using System;
using UnityEngine;
using UnityEngine.UI;

public class InputModalWindow : ModalWindow<InputModalWindow>
{
    [SerializeField] private InputField inputField;
    private Action<string> onInputFieldDone;

    public InputModalWindow SetInputField(Action<string> onDone)
    {
        onInputFieldDone = onDone;
        return this;
    }

    void SubmitInput()
    {
        onInputFieldDone?.Invoke(inputField.text);
        onInputFieldDone = null;
        Close();
    }

    public void UI_InputFieldOKButton()
    {
        SubmitInput();
    }
}
```

2. Create prefab for it (use original modal window prefab as template) and place it under `Resources/Modal Windows`

![Preview](/images/example3.png)

3. Use it from your code in this way:
```C#
ModalWindow<InputModalWindow>.Create()
                   .SetHeader("Input Field Modal")
                   .SetBody("Enter something:")
                   .SetInputField((inputResult) => print("Text: " + inputResult))
                   .Show();
```

4. (Optional) Simplify creation by redefining `Create` method inside `InputModalWindow` class:
```C#
public static new InputModalWindow Create(bool ignorable = true)
{
    return ModalWindow<InputModalWindow>.Create(ignorable);
}
```

So you will be able to call `Create` from it directly:
```C#
InputModalWindow.Create()
                .SetHeader("Input Field Modal")
                .SetBody("Enter something:")
                .SetInputField((inputResult) => print("Text: " + inputResult))
                .Show();
```

Result:

![Preview](/images/example5.gif)

# Installation

**Option 1:**

Download latest `.unitypackage` release from the releases tab and import it in your project.

**Option 2:**

Download .zip of this repository and extract all files from `Assets/ModalWindows` to your project folder.

# Conclusion
Thanks for using this asset, i will be pleasured to see any feedback :)