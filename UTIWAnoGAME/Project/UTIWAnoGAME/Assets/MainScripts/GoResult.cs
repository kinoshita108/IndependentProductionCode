// �Q�[���I�u�W�F�N�g: [GameControl]

// �Q�[���I�����A���U���g�V�[���ɑJ��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GoResult : MonoBehaviour
{
    [SerializeField] private string ResultScene;

    private GameObject timeTaggedObject;
    private TimeLimit timeLimitComponent;
    private TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        // �^�O�ŃI�u�W�F�N�g�̌��������A�m�F�ł���΃R���|�[�l���g���擾
        timeTaggedObject = GameObject.FindGameObjectWithTag("Time");
        if(timeTaggedObject != null)
        {
            timeLimitComponent = timeTaggedObject.GetComponent<TimeLimit>();
        }

        //timeLimitComponent = GameObject.FindGameObjectWithTag("Time").GetComponent<TimeLimit>();
        if(timeLimitComponent != null)
        {     
            timeText = timeLimitComponent.GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLimitComponent.isGameOver)
        {
            LoadResultScene();
        }
    }

    private void LoadResultScene()
    {
        SceneManager.LoadScene(ResultScene);
    }
}
