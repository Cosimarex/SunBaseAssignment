using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListItem : MonoBehaviour
{

    Client ItemClient;
    ClientData ItemData;
    public TextMeshProUGUI DisplayText;
    public void SetData(Client client, ClientData clientData)
    {
        ItemClient = client;

        if(clientData != null) //added this incase the data is not valid for client then it will still work
        {
            ItemData = clientData;
        }

        int points = ItemData != null ? ItemData.points : 0; // Showing points as 0 if the data is not valid

        DisplayText.SetText("Label : "+ ItemClient.label +" Points : " + points);
    }
}
