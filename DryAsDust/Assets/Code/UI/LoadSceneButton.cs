using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneTransitionManager sceneTransition = Transform.FindAnyObjectByType<SceneTransitionManager>();
        sceneTransition.LoadScene("Game");
    }
}
