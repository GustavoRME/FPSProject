using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeychainComponent : MonoBehaviour
{
    [Header("Notifications")]
    [Tooltip("Time in seconds that notification stay enabled")]
    [SerializeField] private float _notificationTime = 1.5f;
    [SerializeField] private GameObject _notificationUI = null;
    [SerializeField] private TextMeshProUGUI _textPro = null;

    [Header("Interaction Settings")]
    [Tooltip("Max Distance to take key and use key")]
    [SerializeField] private float _maxUseDistance = 2.0f;

    [Tooltip("Layer which this component can detect")]
    [SerializeField] private LayerMask _usableLayer = 13;

    private CameraControllerComponent _cameraController;
    private Vector2 _screenMiddle;

    private List<int> _keys;

    private void Awake()
    {
        _cameraController = FindObjectOfType<CameraControllerComponent>();
        _screenMiddle = new Vector2(.5f, .5f);
        _keys = new List<int>();

        _notificationUI.SetActive(false);
    }

    //Called when M2 is pressed by player
    public void Interact()
    {
        //Create a ray from center of screen
        Ray r = _cameraController.GetMainCamera().ViewportPointToRay(_screenMiddle);        

        if(Physics.Raycast(r, out RaycastHit hitInfo, _maxUseDistance, _usableLayer))
        {
            if(hitInfo.collider.TryGetComponent(out KeyComponent key))
            {
                key.Take();                
            }
            else if(hitInfo.collider.TryGetComponent(out LockComponent door))
            {
                door.UseKey(_keys);
            }
        }                
    }    
    public void NotifyPlayer(int keyID, string keyName)
    {
        string notify;

        if(TryAddKey(keyID))
        {
            //New key
            notify = "Chave " + keyName + " Obitda";
        }
        else
        {
            //Existent key
            notify = "Você já tem a " + keyName;
        }

        _textPro.text = notify;
        _notificationUI.SetActive(true);
        Invoke("DisableNotification", _notificationTime);
    }

    //Return false if already have key
    private bool TryAddKey(int newKey)
    {
        if (_keys.Contains(newKey))
            return false;        

        _keys.Add(newKey);
        return true;
    }
    private void DisableNotification() => _notificationUI.SetActive(false);        
}
