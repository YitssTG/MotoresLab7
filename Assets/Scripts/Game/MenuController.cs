using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Canvas Controller")]
    [SerializeField] private GameObject PopUpPause;
    public bool isActive = false;
    [Header("Button Controller")]
    [SerializeField] private Button pauseButton;
    void Start()
    {
        if (pauseButton == null)
        {
            Debug.Log("Nose referencio boton");
        }
        else
        {
            Debug.Log("Se asigno correctamente el boton");
        }
        PopUpPause.SetActive(false);
    }
    public void TogglePause()
    {
        if (isActive)
        {
            Time.timeScale = 1.0f;
            isActive = false;
            PopUpPause.SetActive(false);
            Debug.Log("Continua juego");
        }
        else
        {
            Time .timeScale = 0.0f;
            isActive = true;
            PopUpPause.SetActive(true);
            Debug.Log("Pausa Juego");
        }

    }
}
