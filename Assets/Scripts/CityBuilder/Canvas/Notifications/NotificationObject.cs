using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationObject : MonoBehaviour
{
    public Image iconImage;
    public Text notificationText;

    public void setUpNotification(string notificationTextToAdd, Sprite imageToAdd = null)
    {
        notificationText.text = notificationTextToAdd;

        if(imageToAdd != null)
        {
            iconImage.sprite = imageToAdd;
        }
    }
}
