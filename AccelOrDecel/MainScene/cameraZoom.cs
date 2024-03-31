using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.U2D;

public class cameraZoom : MonoBehaviour
{
    public float moveScale = 0.002f;
    public float newScale = 1.1f;
    public float defaultScale = 1.0f;

    //public PixelPerfectCamera pixelPerfectCamera;



    // Start is called before the first frame update
    void Start()
    {
        // �I�u�W�F�N�g�̃X�P�[����ύX�����
       // ChangeScale(new Vector3(defaultScale, defaultScale, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        // �J�����̎擾
        Camera mainCamera = Camera.main;

        // PixelPerfectCamera �R���|�[�l���g���擾
        // pixelPerfectCamera = GetComponent<PixelPerfectCamera>();

        if (mainCamera != null)
        {
            // Missile��Tag���t�����I�u�W�F�N�g�̎擾
            GameObject[] missiles = GameObject.FindGameObjectsWithTag("Missile") ;
            GameObject[] rMissiles = GameObject.FindGameObjectsWithTag("Rmissile");


            foreach (GameObject missile in  missiles)
            {
                // �~�T�C���̃��[���h���W���X�N���[�����W�ɕϊ�
                Vector3 viewportPos = mainCamera.WorldToViewportPoint(missile.transform.position);

                // �X�N���[�����W����ʓ��ɂ��邩�ǂ������m�F
                if (IsObjectInViewport(viewportPos))
                {
                    // ��ʓ���Missile��Tag���t�����I�u�W�F�N�g������ꍇ�̏���
                    Debug.Log("Missile����ʓ��Ɍ���܂����I");

                    defaultScale += moveScale;

                    ChangeScale(new Vector3(defaultScale, defaultScale, 1f));

                    if (defaultScale >= 1.002f)
                    {
                        defaultScale = 1.1f;
                        ChangeScale(new Vector3(newScale, newScale, 1f));
                    }
                }
                else //if (!IsObjectInViewport(viewportPos))
                {
                    defaultScale -= moveScale;

                    ChangeScale(new Vector3(defaultScale, defaultScale, 1f));

                    if (defaultScale <= 1f)
                    {
                        defaultScale = 1f;
                        ChangeScale(new Vector3(defaultScale, defaultScale, 1f));
                    }
                }
            }

            foreach (GameObject rMissile in  rMissiles)
            {
                // �~�T�C���̃��[���h���W���X�N���[�����W�ɕϊ�
                Vector3 viewportPos = mainCamera.WorldToViewportPoint(rMissile.transform.position);

                // �X�N���[�����W����ʓ��ɂ��邩�ǂ������m�F
                if (IsObjectInViewport(viewportPos))
                {
                    // ��ʓ���Missile��Tag���t�����I�u�W�F�N�g������ꍇ�̏���
                    Debug.Log("rMissile����ʓ��Ɍ���܂����I");

                    defaultScale += moveScale;

                    ChangeScale(new Vector3(defaultScale, defaultScale, 1f));

                    if (defaultScale >= 1.002f)
                    {
                        defaultScale = 1.1f;
                        ChangeScale(new Vector3(newScale, newScale, 1f));
                    }
                }
                else //if (!IsObjectInViewport(viewportPos))
                {
                    defaultScale -= moveScale;

                    ChangeScale(new Vector3(defaultScale, defaultScale, 1f));

                    if (defaultScale <= 1f)
                    {
                        defaultScale = 1f;
                        ChangeScale(new Vector3(defaultScale, defaultScale, 1f));
                    }
                }
            }
        }
    }

    void ChangeScale(Vector3 newScale)
    {
        // �I�u�W�F�N�g��Transform�R���|�[�l���g���擾
        Transform objTransform = transform;

        // �I�u�W�F�N�g�̃X�P�[����ύX
        objTransform.localScale = newScale;
    }

    bool IsObjectInViewport(Vector3 viewportPos)
    {
        return viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1;
    }
}
