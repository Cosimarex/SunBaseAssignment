using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanvasManager : MonoBehaviour
{
    public enum Filter
    {
        AllClients,
        Managers,
        NonManagers
    }

    public API_Manager APIManager;
    public GameObject ListPrefab, ListContent;
    public GameObject Details;
    private APIResponse APIData;
    public Filter ClientsFilter;

    public void ClearList()
    {
        StartCoroutine(ClearListWithAnimation());
    }

    private IEnumerator ClearListWithAnimation()
    {
        while (ListContent.transform.childCount > 0)
        {
            Transform child = ListContent.transform.GetChild(0); // Always get the first child since we are POPing list 
            child.gameObject.transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack).OnComplete(() =>
            {
                Destroy(child.gameObject); // Destroy after animation completes
            });

            // Wait for the animation to finish before proceeding to the next child
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void RefetchAllData()
    {
        StartCoroutine(RefetchData());
    }

    private IEnumerator RefetchData()
    {
        // Clear existing list items with animation
        yield return ClearListWithAnimation();
        Debug.Log("Child Count after clear: " + ListContent.transform.childCount);

        // Fetch new data
        APIManager.FetchData();
    }

    public void DataFetched(APIResponse receivedData)
    {
        APIData = receivedData;
        StartCoroutine(ListClients());
    }

    public IEnumerator ListClients()
    {
        // Ensure list is cleared before adding new entries
        yield return ClearListWithAnimation();

        // Filter the clients based on the selected filter
        List<Client> filteredClients = new List<Client>();

        switch (ClientsFilter)
        {
            case Filter.AllClients:
                filteredClients = APIData.clients;
                break;

            case Filter.Managers:
                filteredClients = APIData.clients.FindAll(client => client.isManager);
                break;

            case Filter.NonManagers:
                filteredClients = APIData.clients.FindAll(client => !client.isManager);
                break;

            default:
                Debug.LogWarning("Unknown filter applied.");
                yield break;
        }

        yield return new WaitForSeconds(0.1f); // Small delay for smoother transition

        // Instantiate new list items with animation
        foreach (Client client in filteredClients)
        {
            GameObject listItem = Instantiate(ListPrefab, ListContent.transform);
            listItem.transform.localScale = Vector3.zero; // Start with zero scale
            listItem.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack); // Scale up animation

            ListItem listItemScript = listItem.GetComponent<ListItem>();

            if (listItemScript != null)
            {
                // Retrieve client data from the dictionary
                APIData.data.TryGetValue(client.id.ToString(), out ClientData clientData);

                // Set data in the ListItem script
                listItemScript.SetData(client, clientData, Details);
            }
        }
    }
}
