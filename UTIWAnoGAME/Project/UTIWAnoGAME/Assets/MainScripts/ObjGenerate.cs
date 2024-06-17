// �Q�[���I�u�W�F�N�g: [Stage]

// �A�C�e���̃����_���X�|�[��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGenerate : MonoBehaviour
{
    [SerializeField] public float itemRemainingNum;  // �X�e�[��̃A�C�e���̎c��
    [SerializeField] private float spawnNum = 5f;  // �A�C�e�����X�|�[�������鐔
    public GameObject spawnItem;  // �X�|�[��������A�C�e��
    public GameObject[] spawnPoint;  // �X�|�[���ʒu�ɐݒ肷��I�u�W�F�N�g
    

    // Start is called before the first frame update
    void Start()
    {
        itemRemainingNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // �X�e�[��ɃA�C�e����1���Ȃ����
        if(itemRemainingNum <= 0)
        {
            HandleItemSpawn();

            // �X�|�[����A�c�����w��̐��ɐݒ�
            itemRemainingNum = spawnNum;
        }
            
        
    }

    // �A�C�e�����X�|�[�������鏈��
    private void HandleItemSpawn()
    {
        // �X�|�[���ʒu�̃��X�g
        List<int> selectedSpawnPoint = new List<int>();

        // �����_���Ɏw��̐��I��
        while (selectedSpawnPoint.Count < spawnNum)
        {
            int randomIndex = Random.Range(0, spawnPoint.Length);

            // �I�΂ꂽ�烊�X�g�ɒǉ�
            if (!selectedSpawnPoint.Contains(randomIndex))
            {
                selectedSpawnPoint.Add(randomIndex);
            }
        }
        
        // �I�΂ꂽ�X�|�[���ʒu�ɃA�C�e�����X�|�[��
        foreach (int spawn in selectedSpawnPoint)
        {
            GameObject selectedPoint = spawnPoint[spawn];
            Instantiate(spawnItem, selectedPoint.transform.position, selectedPoint.transform.rotation);
        }
    }
}
