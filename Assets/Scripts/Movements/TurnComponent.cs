using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnComponent : MonoBehaviour
{
    [Header("Sensitivy")]
    [SerializeField] private float _turnRate = 10;    
    [SerializeField] private float _lookUpRate = 10;

    [Header("Bounds")]
    [Space]
    [Range(-180, 180)]
    [SerializeField] private float _minLookUp = -70;

    [Range(-180, 180)]
    [SerializeField] private float _maxLookUp = 70;
    
    private Vector3 _aim;
    public Vector2 MouseDelta { get; set; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (MouseDelta != Vector2.zero)
        {
            //Smoth the delta from mouse
            MouseDelta *= Time.deltaTime;       

            //Incrase at the aim movement already multiplied with respect values
            _aim += new Vector3(-MouseDelta.y * _lookUpRate, MouseDelta.x * _turnRate, 0.0f);

            //Keep the vertical move between the min and max
            _aim.x = Mathf.Clamp(_aim.x, _minLookUp, _maxLookUp);

            //set the rotation
            transform.eulerAngles = _aim;
        }
    }   

    //Can be used to set sensitivy through the menu settings
    public void SetHorizontalSensitivy(float sensivity) => _turnRate = sensivity;
    public void SetVerticalSensitivy(float sensitivy) => _lookUpRate = sensitivy;
}
