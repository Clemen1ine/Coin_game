using UnityEngine;
using UnityEngine.UI;

public class CoordinateSystem : MonoBehaviour 
{
    public GameObject player; 
    public GameObject canvas; 
    public float scalingFactor = 10.0f; 
    public Text coordinatesText; 

    private Vector3 _offset; 

    void Start () 
    {

        _offset = canvas.transform.position - player.transform.position;
    }

    void Update () 
    {

        float x = (player.transform.position.x + _offset.x) / scalingFactor;
        float y = (player.transform.position.y + _offset.y) / scalingFactor;
        float z = (player.transform.position.z + _offset.z) / scalingFactor;

        coordinatesText.text = "X: " + x.ToString("F2") + " Y: " + y.ToString("F2") + " Z: " + z.ToString("F2");
    }
}