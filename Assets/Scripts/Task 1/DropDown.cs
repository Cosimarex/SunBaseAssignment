using UnityEngine;

public class DropDown : MonoBehaviour
{
    public CanvasManager CanvasManager;

    public void OnDropdownValueChanged(int value)
    {
        switch (value)
        {
            case 0:
                CanvasManager.ClientsFilter = CanvasManager.Filter.AllClients;
                break;
            case 1:
                CanvasManager.ClientsFilter = CanvasManager.Filter.Managers;
                break;
            case 2:
                CanvasManager.ClientsFilter = CanvasManager.Filter.NonManagers;
                break;
            default:
                Debug.LogWarning("Unknown dropdown value selected.");
                return;
        }

        // Update the client list based on the selected filter
        StartCoroutine(CanvasManager.ListClients());
    }
}
