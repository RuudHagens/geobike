using UnityEngine;
using System.Collections;

public class BikeInput : MonoBehaviour
{
    // The rotation speed per update call in degrees
    private float rotationSpeed = 2.0f;
    // The target rotation
    private Quaternion targetRotation;
    // The bike swing position. -1 = left, 0 = middle, 1 = right.
    private int swingPosition = 0;
    // The target swing rotation
    private Vector3 targetSwingRotation;
    // The max swing rotation offset.
    private float maxSwingOffset = 20.0f;

	// Use this for initialization
	void Start ()
    {
        this.targetRotation = transform.rotation;
        this.targetSwingRotation = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Apply this Quaternion rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, this.targetRotation, Time.deltaTime * this.rotationSpeed);
        //transform.localEulerAngles = Vector3.Slerp(transform.rotation.eulerAngles, new Vector3(transform.rotation.eulerAngles.x + this.targetSwingRotation.x, transform.rotation.eulerAngles.y + this.targetSwingRotation.y, transform.rotation.eulerAngles.z + this.targetSwingRotation.z), Time.deltaTime * this.rotationSpeed);
    }

    /// <summary>
    /// Method to set the new rotation of the bike.
    /// </summary>
    /// <param name="selectedNode">The selected node.</param>
    public void RotateBike(GameObject selectedNode)
    {
        // Get the direction vector to the selected node
        Vector3 vectorToTarget = selectedNode.transform.position - transform.position;

        // Get the angle
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 180;

        // Create a Quaternion rotation from the angle
        this.targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /// <summary>
    /// Method to make the bike swing from left to right.
    /// CURRENTLY NOT WORKING!
    /// </summary>
    /// <param name="bikeSpeed">the speed that the bike is going at</param>
    public void Swing(float bikeSpeed)
    {
        if (bikeSpeed < 10) {
            this.swingPosition = 0;
         } else {
            if (this.swingPosition == 0) {
                this.swingPosition = 1;
            } else {
                this.swingPosition *= -1;
            }
        }

        this.targetSwingRotation = new Vector3(this.swingPosition * this.maxSwingOffset, 0, 0);
    }
}
