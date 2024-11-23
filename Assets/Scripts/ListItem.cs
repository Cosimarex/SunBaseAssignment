using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListItem : MonoBehaviour
{

    Client ItemClient;
    ClientData ItemData;
    public TextMeshProUGUI DisplayText;
    GameObject DetailsTab;
    public void SetData(Client client, ClientData clientData, GameObject Details)
    {
        ItemClient = client;

        if(clientData != null) //added this incase the data is not valid for client then it will still work
        {
            ItemData = clientData;
        }

        DetailsTab = Details;

        int points = ItemData != null ? ItemData.points : 0; // Showing points as 0 if the data is not valid

        DisplayText.SetText("Label : "+ ItemClient.label +" Points : " + points);
    }

    public void ItemSelected()
    {
        DetailsTab.SetActive(true);
        DetailsTab.GetComponent<Details>().UpdateDetails(ItemData != null ? ItemData.name : "Data Not Found", ItemData != null ? ItemData.address : "Data Not Found", ItemData != null ? ItemData.points : 0);
    }
}
