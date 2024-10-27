using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPlayer : MonoBehaviour, ISingleton
{
    Information Information = new Information();

    public void LoadData(Information information)
    {
        Information = information;
    }

    public void GetData()
    {

    }
}

public class Information
{
    public string Uid;
    public string Name;
}

