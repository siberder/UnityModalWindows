using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleModalWindow : ModalWindow<SimpleModalWindow>
{
    public static new SimpleModalWindow Create(bool ignorable = false)
    {
        return ModalWindow<SimpleModalWindow>.Create(ignorable);
    }
}
