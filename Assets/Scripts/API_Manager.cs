using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
    public Dictionary<int, ClientData> data;
}


public class API_Manager : MonoBehaviour
{
    string APIURL = "https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";
    void Start()
    {
        StartCoroutine(FetchClientData(APIURL));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator FetchClientData(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtility.FromJson<APIResponse>(request.downloadHandler.text);
                Debug.Log("Fetching Clients Count : " + response.clients.Count);
            }
            else
            {
                Debug.LogError("Data Fetching Failed");
            }
        }
    } 
}
