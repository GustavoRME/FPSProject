using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AmmoComponent : MonoBehaviour
{
    [SerializeField] private int _startBullets = 60;
    [SerializeField] private int _startPills = 0;

    public int Bullets { get; private set; }
    public int Pills { get; private set; }

    private void Awake()
    {
        Bullets = _startBullets;
        Pills = _startPills;
    }

    public void IncreaseBullets(int amount) => Bullets += amount;

    public void IncreaseThrows(int amount) => Pills += amount;

    public void DecreaseBullets(int amount) => Bullets -= amount > 0 ? amount : 0;

    public void DecreaseThrows(int amount) => Pills -= amount > 0 ? amount : 0;

}
