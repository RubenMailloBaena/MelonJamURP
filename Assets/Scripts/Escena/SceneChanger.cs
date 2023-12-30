using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private bool isIntroduction = false;
    [SerializeField]
    private string nuevaEscena;
    [SerializeField]
    private float tiempoDeEspera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && isIntroduction)
        {
            CambiarEscena(nuevaEscena);
        }
    }


    public void CambiarEscena(string newScene)
    {
      
            SceneManager.LoadScene(newScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
