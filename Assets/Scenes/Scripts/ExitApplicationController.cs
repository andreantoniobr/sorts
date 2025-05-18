using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApplicationController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitApplication();
        }
    }

    private void QuitApplication()
    {
#if UNITY_EDITOR        
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Para fechar o aplicativo no build
        Application.Quit();
#endif
    }
}
