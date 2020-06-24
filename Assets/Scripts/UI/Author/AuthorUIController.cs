using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AuthorUIController : MonoBehaviour
{
    public Button back;

    private void OnEnable(){
        back.onClick.AddListener(UnloadScene);
    }

    private void UnloadScene() {
        //SceneManager.UnloadSceneAsync(gameObject.scene);
        gameObject.SetActive(false);
    }
}
