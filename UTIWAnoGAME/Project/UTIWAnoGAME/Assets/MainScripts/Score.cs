// �Q�[���I�u�W�F�N�g: [Stage]

// ItemScript���ŉ��_���ꂽ���݂̃X�R�A��\��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [SerializeField] public float currentScore;  // ���݂̃X�R�A
    [SerializeField] public float addScore = 10f;  // ���_��
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // ���U���g�V�[���J�ڎ��ɔj�󂳂�Ȃ��悤�ɐݒ�
        if(SceneManager.GetActiveScene().name == "SampleScene")
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // ���݂̃X�R�A��\��
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
