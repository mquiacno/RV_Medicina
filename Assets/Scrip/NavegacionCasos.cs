using UnityEngine;
using UnityEngine.SceneManagement;

public class NavegacionCasos : MonoBehaviour
{
    public void CargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}