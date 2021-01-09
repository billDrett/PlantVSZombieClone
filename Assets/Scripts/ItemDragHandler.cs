using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] GameObject instantiatedPrefab;
    [SerializeField] GameObject zombieImgPrefab;
    GameObject zombieImg;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        zombieImg = Instantiate(zombieImgPrefab);
        zombieImg.transform.SetParent(gameObject.transform, false);
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        zombieImg.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("RAYCAST SUCCESFULL");
            Debug.Log("Instatiate zombie at pos" + hit.point);
            Vector3 spawnPoint = new Vector3(hit.point.x, instantiatedPrefab.transform.position.y, hit.point.z);
            GameObject instantiatedObj = Instantiate(instantiatedPrefab, spawnPoint, Quaternion.identity);

            //spawn it at the center of the rectangle.
        }
        Destroy(zombieImg);
    }
}
