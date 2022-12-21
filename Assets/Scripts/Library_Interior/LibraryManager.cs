using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryManager : MonoBehaviour
{
    [SerializeField] Notification activationNotification;
    public PopUp_Manager popUp_manager;

    private void Awake()
    {
        if (SaveObject.Instance.firstTimeInLibrary == false)
        {
            SaveObject.Instance.firstTimeInLibrary = true;
            popUp_manager.addNotificationToQueue(activationNotification);
        }
    }
}
