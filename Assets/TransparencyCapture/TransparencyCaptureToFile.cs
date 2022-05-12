using System.Collections;
using UnityEngine;

public class TransparencyCaptureToFile:MonoBehaviour
{
    [SerializeField] private string fileName = "Capture";
    [SerializeField]
    [Tooltip(
        "Aquí se debe indicar el directorio o ruta local dentro de 'Assets' en el que se quiere almacenar la captura, el directorio o ruta debe existir previamente." +
        "En caso de ser una ruta se debe utilizar el símbolo '/' entre los directorios"
        )] 
    private string localFolder = "Captures";
    public IEnumerator capture()
    {

        yield return new WaitForEndOfFrame();
        //After Unity4,you have to do this function after WaitForEndOfFrame in Coroutine
        //Or you will get the error:"ReadPixels was called to read pixels from system frame buffer, while not inside drawing frame"
        zzTransparencyCapture.captureScreenshot(Application.dataPath + "/" + localFolder + "/" + name + ".png");
        Debug.Log("imagen guardada");
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pulsando espacio");
            StartCoroutine(capture());
        }
            
    }
}