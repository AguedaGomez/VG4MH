using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testNotifications : MonoBehaviour
{

    public PopUp_Manager notificationManager;

    public List<Notification> notificationsToSend;

    public void sendNotificationToManager()
    {
        if(notificationsToSend.Count != 0)
        {
            notificationManager.addNotificationToQueue(notificationsToSend[0]);
            notificationsToSend.RemoveAt(0);
        }else
        {
            Debug.Log("No hay mas notificaciones a mostrar");
        }
    }

}
