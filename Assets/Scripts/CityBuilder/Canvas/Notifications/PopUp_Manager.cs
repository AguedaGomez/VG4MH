using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp_Manager : MonoBehaviour
{
    public static PopUp_Manager Instance { get; private set; }

    [SerializeField] GameObject popUp_Prefab;
    public float notificationDuration;
    public float popUp_Offset;

    //private List<Notification> notificationsToShow = new List<Notification>();
    private Queue<Notification> notificationsToShow = new Queue<Notification>();
    bool showingNotifications = false;

    //La cuestión estaría en con una función añadir notificaciones a la lista
    //y que la lista detecte cuando le ha entrado una notificación y active la corrutina que las muestr
    //Esta corrutina tambien tiene que ser capaz de, una vez mostrada la notificación, eliminarla y ver 
    //si hay más en la lista para mostrarlas individualmente hasta que ya no haya ninguna

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void addNotificationToQueue(Notification newNotification)
    {
        notificationsToShow.Enqueue(newNotification);
        
        if(!showingNotifications)
        {
            StartCoroutine(showNotificationsInQueue());
        }
    }

    IEnumerator showNotificationsInQueue()
    {
        showingNotifications = true;

        while(notificationsToShow.Count != 0)
        {
            //Mostrar cada notificación

            //Se obtiene la información a mostrar y se instancia la nueva notificación
            Notification actualNotification = notificationsToShow.Dequeue();
            GameObject newNotificationObject = Instantiate(popUp_Prefab, this.transform);

            //Se coloca la información en el objeto notificación
            newNotificationObject.GetComponent<NotificationObject>().setUpNotification(actualNotification.textNotification, actualNotification.imageNotification);
            
            //Se mueve la notificación, se muestra durante dos segundos y se recoge
            newNotificationObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 240, 0);
            LeanTween.move(newNotificationObject.GetComponent<RectTransform>(), new Vector3(0, 0 - popUp_Offset, 0), 0.7f);
            yield return new WaitForSeconds(0.7f + notificationDuration);
            LeanTween.move(newNotificationObject.GetComponent<RectTransform>(), new Vector3(0, 240, 0), 0.7f);
            yield return new WaitForSeconds(1);

            //Finalmente se destruye el objeto y se reinicia el bucle
            Destroy(newNotificationObject);
        }

        showingNotifications = false;
        yield return null;
    }
}
