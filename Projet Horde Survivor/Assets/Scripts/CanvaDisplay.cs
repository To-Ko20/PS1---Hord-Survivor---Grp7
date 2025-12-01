using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CanvaDisplay : MonoBehaviour
{
    [SerializeField] private bool isLeft = true;
    [SerializeField] private Vector3 leftPos;
    [SerializeField] private Vector3 rightPos;
    
    [SerializeField] Camera cam;
    [SerializeField] Volume vol;
    [SerializeField] VolumeProfile volPrefLeft;
    [SerializeField] VolumeProfile volPrefRight;
    [SerializeField] GameObject renderCanvas;
    [SerializeField] GameObject countdownCanvas;

    [SerializeField] private Image moveIMG;
    [SerializeField] private Sprite moveLeftSprite;
    [SerializeField] private Sprite moveRightSprite;
    
    public void MoveDisplay()
    {
        if (isLeft)
        {
            isLeft = false;
            transform.position = rightPos + new Vector3(0,540,0);
            
            vol.profile = volPrefRight;

            var pos = renderCanvas.GetComponent<RectTransform>().anchoredPosition;
            pos.x = 0;
            renderCanvas.GetComponent<RectTransform>().anchoredPosition = pos;
            
            var vector2 = countdownCanvas.GetComponent<RectTransform>().anchoredPosition;
            vector2.x = 12;
            countdownCanvas.GetComponent<RectTransform>().anchoredPosition = vector2;
            
            moveIMG.sprite = moveRightSprite;
        }
        else
        {
            isLeft = true;
            transform.position = leftPos + new Vector3(0,540,0);
            
            vol.profile = volPrefLeft;
            
            var pos = renderCanvas.GetComponent<RectTransform>().anchoredPosition;
            pos.x = 200;
            renderCanvas.GetComponent<RectTransform>().anchoredPosition = pos;

            var vector2 = countdownCanvas.GetComponent<RectTransform>().anchoredPosition;
            vector2.x = 19;
            countdownCanvas.GetComponent<RectTransform>().anchoredPosition = vector2;
            
            moveIMG.sprite = moveLeftSprite;
        }
        
        var vector3 = cam.transform.position;
        vector3.x = -cam.transform.position.x;
        cam.transform.position = vector3;
        cam.GetComponent<CameraMovement>().offset.x = -cam.GetComponent<CameraMovement>().offset.x;
    }
}
