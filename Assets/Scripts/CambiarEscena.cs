using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CambiarDeEscena(string nombreNivel)
    {
        SceneManager.LoadScene(nombreNivel);
    }
}
