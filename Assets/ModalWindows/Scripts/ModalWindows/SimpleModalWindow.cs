using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleModalWindow : ModalWindow<SimpleModalWindow>
{
    public static new SimpleModalWindow Create(bool ignorable = true)
    {
        return ModalWindow<SimpleModalWindow>.Create(ignorable);
    }

}
