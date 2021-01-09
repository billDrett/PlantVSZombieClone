using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject instantiatedPrefab;


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("RAYCAST SUCCESFULL");
            Debug.Log("Instatiate zombie at pos" + hit.point);
            Vector3 spawnPoint = new Vector3(hit.point.x, instantiatedPrefab.transform.position.y, hit.point.z);
            GameObject instantiatedObj = Instantiate(instantiatedPrefab, spawnPoint, Quaternion.identity);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
