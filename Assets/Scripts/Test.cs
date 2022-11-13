using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    /// <summary>
    /// ��Դ
    /// </summary>
    public AudioSource _audio;
    /// <summary>
    /// ���Ƶ�����ݵ����鳤��    tips:���ȱ���Ϊ2��n�η�����С64�����8192(�����ĵ����ܳ���ס�Ļ�)
    /// </summary>
    [Range(64, 128 * 2)]
    public int _sampleLenght = 128 * 2;
    /// <summary>
    /// ��ƵƵ������
    /// </summary>
    private float[] _samples;
    /// <summary>
    /// UIList
    /// </summary>
    private List<Image> _uiList = new List<Image>();
    /// <summary>
    /// UI���ڵĸ�����
    /// </summary>
    public RectTransform _uiParentRect;
    /// <summary>
    /// ��Ƶ������Ԥ��
    /// </summary>
    public GameObject _prefab;
    /// <summary>
    /// ����ÿ������������һ��UI
    /// </summary>
    public float _uiDistance;
    /// <summary>
    /// �½��ķ��ȱ�ֵ
    /// </summary>
    [Range(1, 30)]
    public float UpLerp = 12;


    void Start()
    {
        //���ɲ���ȡȫ��UI
        CreatUI();
        _samples = new float[_sampleLenght];
    }


    /// <summary>
    /// ��̬����UI
    /// </summary>
    private void CreatUI()
    {
        for (int i = 0; i < _sampleLenght; i++)
        {
            GameObject _prefab_GO = Instantiate(_prefab, _uiParentRect.transform);
            //Ϊ���ɵ�ui����
            _prefab_GO.name = string.Format("Sample[{0}]", i + 1);
            _uiList.Add(_prefab_GO.GetComponent<Image>());
            RectTransform _rectTransform = _prefab_GO.GetComponent<RectTransform>();
            //����λ��
            _rectTransform.localPosition = new Vector3(_rectTransform.sizeDelta.x + _uiDistance * i, 0, 0);
        }
    }

    void Update()
    {
        //��ȡƵ��
        _audio.GetSpectrumData(_samples, 0, FFTWindow.BlackmanHarris);
        //ѭ��
        for (int i = 0; i < _uiList.Count; i++)
        {
            //ʹ��Mathf.Clamp���м�λ�õĵ�y������һ����Χ���������
            //Ƶ��ʱԽ���ԽС�ģ�Ϊ�����������ݱ仯�����ԣ���������samples[i]ʱ������50+i * i*0.5f
            Vector3 _v3 = _uiList[i].transform.localScale;
            _v3 = new Vector3(1, Mathf.Clamp(_samples[i] * (50 + i * i * 2), 0, 50), 1);
            _uiList[i].transform.localScale = Vector3.Lerp(_uiList[i].transform.localScale, _v3, Time.deltaTime * UpLerp);
        }
    }
}