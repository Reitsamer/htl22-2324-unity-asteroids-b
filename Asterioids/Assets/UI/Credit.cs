using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Credit
{
    public string task;
    public string[] names;
}

[Serializable]
public class Credits 
{
    public Credit[] credits;
}
