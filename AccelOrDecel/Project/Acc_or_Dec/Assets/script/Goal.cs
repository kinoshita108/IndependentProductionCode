using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    //�S�[��������ɃX�e�[�W�Z���N�g�ֈړ�
    public GameObject resultUI;
    private bool isUsed;
    [SerializeField] private GameObject _warpObject;
    [SerializeField] private AudioClip _goalSound;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !isUsed)
        {
            isUsed = true;
            GameObject player = collider.gameObject;
            player.transform.position = new Vector2(transform.position.x, transform.position.y);
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            playerScript.DeleteGoal();
            Invoke("BootWarp", 2.0f);
            Debug.Log("�S�[��");
        }
    }
    private void BootWarp()
    {
        Instantiate(_warpObject, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
        _audioSource.PlayOneShot(_goalSound);
        Invoke("Stagefinished", 2.0f);
    }
    private void Stagefinished()
    {
        resultUI.SetActive(true);
        Result();
        //SceneManager.LoadScene("StageSlect");
    }
    public void Result()
    {
        Time.timeScale = 0f;
        //Debug.Log("owari");
        resultUI.SetActive(true);
    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        //PlayerPrefs��SCORE��3�Ƃ����l������
    //        PlayerPrefs.SetInt("StageUnlock", 3);
    //        //PlayerPrefs���Z�[�u����         
    //        PlayerPrefs.Save();
    //    }
    //}
}