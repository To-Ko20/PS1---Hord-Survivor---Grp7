using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
 public void PlayGame()
    {
        SceneManager.LoadScene(1); //modif la scene par scene de jeu finale
    }

    public void QuitGame() //FONCTIONNE UNIQUMENT SUR APLICATION BUILD
    {
        Application.Quit();
    }
}
