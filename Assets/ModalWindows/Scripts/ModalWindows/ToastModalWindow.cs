using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastModalWindow : ModalWindow<ToastModalWindow>
{
    private Image iconImage;

    public float Delay { get; private set; } = 3f;

    public ToastModalWindow SetIcon(Sprite icon)
    {
        iconImage.sprite = icon;
        return this;
    }

    public ToastModalWindow SetDelay(float seconds)
    {
        Delay = seconds;
        return this;
    }

    public override ToastModalWindow Show()
    {
        base.Show();
        if (Delay > 0) Invoke("Close", Delay);
        return this;
    }
}
