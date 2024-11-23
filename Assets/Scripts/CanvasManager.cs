using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public API_Manager APIManager;
    public GameObject ListPrefab,ListContent;
    APIResponse APIData;

    public void ClearList()
    {
        for (int i = ListContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(ListContent.transform.GetChild(i).gameObject);
        }
        APIManager.FetchData();
    }
    public void RefetchData()
    {
        ClearList();
    }
    public void DataFetched(APIResponse RecievedData)
    {
        APIData = RecievedData;

        if (RecievedData == null || RecievedData.clients == null)//Making sure datais valid
        {
            Debug.LogError("No data available to display.");
            return;
        }

        foreach (Client client in RecievedData.clients)
        {
            // Instantiate the prefab for each client
            GameObject listItem = Instantiate(ListPrefab, ListContent.transform);

            // Get the ListItem script attached to the instantiated prefab
            ListItem listItemScript = listItem.GetComponent<ListItem>();

            if (listItemScript != null)
            {
                // Get the ClientData for the client
                ClientData clientData = RecievedData.data.ContainsKey(client.id+"") ? RecievedData.data[client.id+""] : null;

                // Set the data in the ListItem script
                listItemScript.SetData(client, clientData);
            }
        }
    }
}
