using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagazineUIComponent : MonoBehaviour
{
    public enum MagazineType { Text, Renderer};
    
    public MagazineType magazineType = MagazineType.Text;
    public TextMeshProUGUI textMesh = null;
    public Renderer rend = null;

    //Renderer Mode
    public float maxWobble = 0.03f;
    public float wobbleSpeed = 1f;
    public float recovery = 1f;
    
    private Vector3 _previousPosition;
    private Vector3 _velocity;
    private Vector3 _lastRotation;
    private Vector3 _angularVelocity;
    private float _wobbleAmountX;
    private float _wobbleAmountZ;
    private float _wobbleAmountToAddX;
    private float _wobbleAmountToAddZ;
    private float _pulse;
    private float _time = 0.5f;

    public Material material;

    private int _liquidRotationId;
    private int _fillAmountId;

    private int _clipAmmo = 6;
    private float _fillAmount = -0.44f;

    private void Awake()
    {
        if (magazineType == MagazineType.Renderer)
        {
            material = rend.material;

            _liquidRotationId = Shader.PropertyToID("_LiquidRotation");
            _fillAmountId = Shader.PropertyToID("_FillAmount");

            enabled = true;
        }
        else
        {
            _fillAmount = -0.44f;
            _clipAmmo = 6;
            enabled = false;
        }
    }
    private void Update()
    {
        _time += Time.deltaTime;

        // decrease wobble over time
        _wobbleAmountToAddX = Mathf.Lerp(_wobbleAmountToAddX, 0, Time.deltaTime * recovery);
        _wobbleAmountToAddZ = Mathf.Lerp(_wobbleAmountToAddZ, 0, Time.deltaTime * recovery);

        // make a sine wave of the decreasing wobble
        _pulse = 2 * Mathf.PI * wobbleSpeed;
        _wobbleAmountX = _wobbleAmountToAddX * Mathf.Sin(_pulse * _time);
        _wobbleAmountZ = _wobbleAmountToAddZ * Mathf.Sin(_pulse * _time);

        Matrix4x4 rotation = Matrix4x4.Rotate(Quaternion.AngleAxis(_wobbleAmountZ, Vector3.right) * Quaternion.AngleAxis(_wobbleAmountX, Vector3.forward));

        // send it to the shader
        material.SetMatrix(_liquidRotationId, rotation);

        // velocity
        _velocity = (_previousPosition - transform.position) / Time.deltaTime;
        _angularVelocity = transform.rotation.eulerAngles - _lastRotation;


        // add clamped velocity to wobble
        _wobbleAmountToAddX += Mathf.Clamp((_velocity.x + (_angularVelocity.z * 0.2f)) * maxWobble, -maxWobble, maxWobble);
        _wobbleAmountToAddZ += Mathf.Clamp((_velocity.z + (_angularVelocity.x * 0.2f)) * maxWobble, -maxWobble, maxWobble);

        // keep last position
        _previousPosition = transform.position;
        _lastRotation = transform.rotation.eulerAngles;
    }

    public void UpdateMagazine(int clipAmmo)
    {
        if(magazineType == MagazineType.Text)
        {
            textMesh.text = clipAmmo.ToString();
        }
        else
        {            
            _fillAmount = clipAmmo < _clipAmmo ? _fillAmount - 0.01f : -0.44f;
            _clipAmmo = clipAmmo;

            material.SetFloat(_fillAmountId, _fillAmount);            
        }
    }
}
