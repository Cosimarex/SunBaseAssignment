using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

[System.Serializable]
public class Client
{
    public bool isManager;
    public int id;
    public string label;
}

[System.Serializable]
public class ClientData
{
    public string address;
    public string name;
    public int points;
}

[System.Serializable]
public class APIResponse
{
    public List<Client> clients;
    public Dictionary<string, ClientData> data;
    public string label;
}

public class API_Manager : MonoBehaviour
{
    private string APIURL = "https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";
    public CanvasManager CanvasManager;
    void Start()
    {
        FetchData();
    }
    public void FetchData()
    {
        StartCoroutine(FetchClientData(APIURL));
    }
    private IEnumerator FetchClientData(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("Raw JSON Response: " + jsonResponse);//Raw Response

                try
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(jsonResponse);//using newtonsoft.json due to dicitionary usage

                    // Printing alldata fetched from api
                    Debug.Log($"Clients Count: {apiResponse.clients.Count}");
                    foreach (var client in apiResponse.clients)
                    {
                        Debug.Log($"Client ID: {client.id}, Label: {client.label}, IsManager: {client.isManager}");
                    }

                    Debug.Log($"Data Count: {apiResponse.data.Count}");
                    foreach (var key in apiResponse.data.Keys)
                    {
                        var clientData = apiResponse.data[key];
                        Debug.Log($"Key: {key}, Name: {clientData.name}, Address: {clientData.address}, Points: {clientData.points}");
                    }

                    CanvasManager.DataFetched(apiResponse);//Telling CanvasManager that Data is Fetched
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Error parsing JSON: " + ex.Message);
                }
            }
            else
            {
                Debug.LogError("Data Fetching Failed: " + request.error);
            }
        }
    }
}
