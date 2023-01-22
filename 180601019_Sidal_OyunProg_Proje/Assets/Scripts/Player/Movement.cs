using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public Animator animatorCntlr;
    public GameObject myCar;
    public GameObject myYacht;
    public GameObject areasCar;
    public GameObject areasYacht;
    public GameObject exitCarButton;
    public GameObject exitYachtButton;

    public float playerMoveSpeed = 5f;
    public float rotationSpeed = 10f;

    void Update()
    {
        Vector2 myMoveDirection = variableJoystick.Direction;
        Vector3 myMovementVector = new Vector3(myMoveDirection.x, 0, myMoveDirection.y);

        myMovementVector = myMovementVector * Time.deltaTime * playerMoveSpeed;
        transform.position += myMovementVector;

        if (myMovementVector.magnitude != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(myMovementVector, Vector3.up), Time.deltaTime * rotationSpeed);
        }

        bool isWalking = myMoveDirection.magnitude > 0;
        animatorCntlr.SetBool("isWalking", isWalking);
        animatorCntlr.SetFloat("Speed", myMoveDirection.magnitude);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            myCar.SetActive(true);
            areasCar.SetActive(false);
            exitCarButton.SetActive(true);

        }

        if (other.CompareTag("Yacht"))
        {
            myYacht.SetActive(true);
            areasYacht.SetActive(false);
            exitYachtButton.SetActive(true);
        }
    }

    public void ExitCar()
    {
        myCar.SetActive(false);
        areasCar.SetActive(true);
        this.transform.position = new Vector3(this.transform.position.x - 3, this.transform.position.y, this.transform.position.z);
        exitCarButton.SetActive(false);

    }

    public void ExitYacth()
    {
        myYacht.SetActive(false);
        areasYacht.SetActive(true);
        this.transform.position = new Vector3(-98, this.transform.position.y, -1);
        exitYachtButton.SetActive(false);
    }
}
