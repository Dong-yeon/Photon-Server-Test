using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Car : MonoBehaviourPun
{
    [SerializeField] UnityStandardAssets.Vehicles.Car.CarController carController;

    void Start()
    {
        if (photonView.IsMine)
        {
            GameManager.Inst.SetCamTarget(transform);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (GameManager.Inst.state == State.RacingStart)
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                float drift = Input.GetAxis("Jump");

                carController.Move(horizontal, vertical, vertical, drift);
            }
            else
                carController.Move(0, 0, 0, 1);
        }
    }
}