using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Details : MonoBehaviour
{
    public TextMeshProUGUI NameTF, OtherDetailsTF;

    public void UpdateDetails(string Name,string Address,int Points)
    {
        NameTF.SetText("Name : " + Name);
        OtherDetailsTF.SetText("Points : " + Points + "\n" + "Address : " + Address);
    }
}
