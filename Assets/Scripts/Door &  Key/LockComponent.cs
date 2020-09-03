using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockComponent : MonoBehaviour
{
    [Tooltip("Unique ID to match with key")]
    [SerializeField] private int _lockID = 1;

    [Header("Notification")]
    [SerializeField] private GameObject _notification = null;
    [SerializeField] private float _notificationTime = 1.5f;

    private Material _mat;    
    
    private int _cuttoffID;
    private float _value;

    private void Awake()
    {
        _mat = GetComponent<MeshRenderer>().material;       
        _cuttoffID = Shader.PropertyToID("_Cutoff");

        _notification.SetActive(false);
    }

    public void UseKey(List<int> keys)
    {
        bool keyMatch = keys.Contains(_lockID);       

        //If have key, open it.
        if(keyMatch)
        {
            Open();
        }
        else
        {
            ShowNotify();
        }
    }

    private void Open() => StartCoroutine("Cuttoff");
    private void ShowNotify()
    {
        _notification.SetActive(true);
        Invoke("DisableNotify", _notificationTime);        
    }

    private IEnumerator Cuttoff()
    {
        while(_value < 1.0f)
        {
            //Increase the values by the time. 
            _value = Mathf.Clamp(_value + Time.deltaTime, 0.0f, 1.0f);

            //SetValue
            _mat.SetFloat(_cuttoffID, _value);

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
    private void DisableNotify() => _notification.SetActive(false);
}
