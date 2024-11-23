using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CanvasManager : MonoBehaviour
{

    public enum Filter
    {
        AllClients,
        Managers,
        NonManagers
    };


    public API_Manager APIManager;
    public GameObject ListPrefab,ListContent;
    APIResponse APIData;
    public Filter ClientsFilter;
    public GameObject Details;
    public void ClearList()
    {
        for (int i = ListContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(ListContent.transform.GetChild(i).gameObject);
        }
    }
    public void RefetchData()
    {
        ClearList();
        APIManager.FetchData();
    }
    public void DataFetched(APIResponse RecievedData)
    {
        APIData = RecievedData;

        ListClients();
    }
    public void ListClients()
    {
        // Clear existing prefabs
        ClearList();

        List<Client> filteredClients = new List<Client>();// Making Empty List

        switch (ClientsFilter)
        {
            case Filter.AllClients:
                filteredClients = APIData.clients; // Show all clients
                break;

            case Filter.Managers:
                filteredClients = APIData.clients.FindAll(client => client.isManager); // Showing Only managers
                break;

            case Filter.NonManagers:
                filteredClients = APIData.clients.FindAll(client => !client.isManager); // Showing Only non-managers
                break;

            default:
                Debug.LogWarning("Unknown filter applied.");
                return;
        }

        // Adding Filtered clients to list
        foreach (Client client in filteredClients)
        {
            GameObject listItem = Instantiate(ListPrefab, ListContent.transform);
            ListItem listItemScript = listItem.GetComponent<ListItem>();

            if (listItemScript != null)
            {
                // Retrieve client data from the dictionary
                APIData.data.TryGetValue(client.id+"", out ClientData clientData);

                // Set data in the ListItem script
                listItemScript.SetData(client, clientData,Details);
            }
        }
    }
}
