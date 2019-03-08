using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    /// <summary>
    /// Go to the game.
    /// </summary>
    public void ToGame() {
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// Go to the credits.
    /// </summary>
    public void ToCredits() {
        SceneManager.LoadScene("Credits");
    }

    /// <summary>
    /// Go to the control page.
    /// </summary>
    public void ToControls() {
        SceneManager.LoadScene("controlPage");
    }
}
