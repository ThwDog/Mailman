using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnVechice : MonoBehaviour
{
    private BikeCotroller bike;
    [Header("Bike")]
    public Transform Seat;
    [SerializeField][Range(0,20)]private float seatYPos;
    [SerializeField]private float speed;
    private Vector3 bike_tran;

    private void Start() 
    {
        bike = gameObject.GetComponent<BikeCotroller>();    
    }

    public void hopONBike(Transform player)
    {
        bike._playerOnBike = true;
        float h_speed = speed * Time.deltaTime;
        Transform bike_pos = bike.gameObject.transform;
        bike_tran = new Vector3(bike_pos.position.x,bike_pos.position.y + seatYPos,bike_pos.position.z);
        //player.position = Vector3.MoveTowards(playerPos,Seat.position,h_speed);
        player.position = bike_tran;
    }

    public void offBike()
    {
        bike._playerOnBike = false;
        float h_speed = speed * Time.deltaTime;
        Transform bike_pos = bike.gameObject.transform;
        //bike_tran = new Vector3(bike_pos.position.x - 10,0,0);
        //player.position = bike_tran;
    }
}
