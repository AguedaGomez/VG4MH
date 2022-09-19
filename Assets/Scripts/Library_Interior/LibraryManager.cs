using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryManager : MonoBehaviour
{
    [SerializeField] Notification activationNotification;
    public PopUp_Manager popUp_manager;

    private void Awake()
    {
        if (SaveObject.Instance.enterInLibraryToday == false)
        {
            SaveObject.Instance.enterInLibraryToday = true;
            SaveObject.Instance.activationValue += 10;
            SaveObject.Instance.updateActivationValueOnLastQuestionnaire();
            popUp_manager.addNotificationToQueue(activationNotification);
        }
    }
}
