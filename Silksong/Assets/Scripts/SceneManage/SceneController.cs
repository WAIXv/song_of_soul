using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// �������л� ����
/// </summary>
public class SceneController : MonoBehaviour
{
    public static SceneController Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<SceneController>();

            if (instance != null)
                return instance;

            GameObject sceneControllerGameObject = new GameObject("SceneManager");
            instance = sceneControllerGameObject.AddComponent<SceneController>();

            return instance;
        }
    }//����

    protected static SceneController instance;

    public bool m_Transitioning;

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }


    /// <summary>
    /// ����Ϸ�������ڵ���һ������
    /// </summary>
    /// <param name="transitionPoint"></param>
    public static void TransitionToScene(SceneTransitionPoint transitionPoint)
    {
        CameraController.Instance.BeforeChangeScene();
        Instance.StartCoroutine(Instance.Transition(transitionPoint.newSceneName, transitionPoint, transitionPoint.resetInputValuesOnTransition));
    }

   /* public static void TransitionToScene(string SceneName,bool resetInputValuesOnTransition)//�Ӳ˵�����Ϸ������ �ݲ���
    {
        Instance.StartCoroutine(Instance.Transition(SceneName, SceneEntrance.EntranceTag.A, resetInputValuesOnTransition));
    }*/


    protected IEnumerator Transition(string newSceneName, SceneTransitionPoint destination, bool resetInputValues)
    {
        m_Transitioning = true;


        PlayerAnimatorParamsMapping.SetControl(false);

        //  yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Loading));
        yield return SceneManager.LoadSceneAsync(newSceneName);//�첽���س���
        GameObjectTeleporter.Instance.playerEnterSceneFromTransitionPoint(destination);//��ҵ�������� 

        // yield return StartCoroutine(ScreenFader.FadeSceneIn());

        PlayerAnimatorParamsMapping.SetControl(true);

        m_Transitioning = false;
    }

}
