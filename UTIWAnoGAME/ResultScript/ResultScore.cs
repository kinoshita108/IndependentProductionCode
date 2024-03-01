// �Q�[���I�u�W�F�N�g: [Score]

// �ŏI�X�R�A��\��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScore : MonoBehaviour
{
    [SerializeField] private float resultScore;  // ���U���g��ʂɕ\������X�R�A
    
    public TextMeshProUGUI scoreText;

    private GameObject stageTaggedObj;
    private Score scoreComponent;

    // Start is called before the first frame update
    void Start()
    {
        // �^�O�ŃI�u�W�F�N�g�̌��������A�m�F�ł���΃R���|�[�l���g���擾
        stageTaggedObj = GameObject.FindGameObjectWithTag("Stage");
        if(stageTaggedObj != null)
        { 
            scoreComponent = stageTaggedObj.GetComponent<Score>();
        }

        // ���C���V�[���ł̍ŏI�X�R�A�����U���g�X�R�A�ɑ��
        resultScore = scoreComponent.currentScore;
        scoreText.text = "Score: " + resultScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
