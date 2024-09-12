using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private float _sceneChangeTime = 1f;
    [SerializeField] private Animator _animator;

    [SerializeField] private string _changeSceneTag = "Player";

    public bool sceneIsChanging = false;

    public IEnumerator FadeToNewScene()
    {
        sceneIsChanging = true;

        _animator.Play("fadeOut");

        yield return new WaitForSeconds(_sceneChangeTime);

        if (!InteractionCanvasRoom.gameIsEnding)
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex +1, LoadSceneMode.Single);
        else
        {
            InteractionCanvasRoom.gameIsEnding = false;
            BackgroundMusic.instance.backgroundMusic.Stop();
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
    }

    public IEnumerator FadeToMainMenu()
    {
        _animator.Play("fadeOut");
        yield return new WaitForSeconds(1f);
        ForestNoise.instance.forestNoise.Play();
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _changeSceneTag)
        {
            StartCoroutine(FadeToNewScene());
        }
    }
}
