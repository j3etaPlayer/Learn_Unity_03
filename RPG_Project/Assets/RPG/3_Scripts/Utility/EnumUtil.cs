using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumUtil<T>
{
    public static T Parse(string s)
    {
        return (T) Enum.Parse(typeof(T), s);
    }
}
