using UnityEngine;

public class SecretDoorsBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float speed;
    private Vector3 startingPos;
    private Vector3 endingPos;
    private float tLerp;
    private bool isActive = false;

    public void Open()
    {
        isActive = true;
        startingPos = transform.position;
        endingPos = startingPos + _offset;
        tLerp = 0;
    }
    private void Opening()
    {
        if(tLerp > 1 || !isActive)
        {
            return;
        }
        tLerp += Time.deltaTime * speed;
        gameObject.transform.position = Vector3.Lerp(startingPos, endingPos, tLerp);
        if(tLerp > 1)
        {
            isActive = false;
        }
    }
    private void Update()
    {
        Opening();
    }
}
