using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardMovementScr : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera MainCamera;
    Vector3 offset;
    public Transform DefaultParent, DefaultTempCardParent; 
    GameObject TempCardGo; 
    public GameManagerScr GameManager;
    public bool IsDraggable; 
    int startID;
    void Awake()
    {
        MainCamera = Camera.allCameras[0]; 
        TempCardGo = GameObject.Find("TempCardGO"); 
        GameManager = FindObjectOfType<GameManagerScr>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position); // ������� ����� - ���-� ����

        DefaultParent = DefaultTempCardParent = transform.parent; 

        IsDraggable = (DefaultParent.GetComponent<DropPlaceScr>().Type == FieldType.SELF_HAND) && 
                       GameManager.IsPlayerTurn; 

        if (!IsDraggable)
            return;

        startID = transform.GetSiblingIndex();

        TempCardGo.transform.SetParent(DefaultParent);  
        TempCardGo.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData) 
    {
        if (!IsDraggable)
            return;

        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position); 
        transform.position = newPos + offset;

        if (TempCardGo.transform.parent != DefaultTempCardParent) 
            TempCardGo.transform.SetParent(DefaultTempCardParent);

        // if (DefaultParent.GetComponent<DropPlaceScr>().Type != FieldType.SELF_FIELD)
        CheckPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsDraggable)
            return;

        transform.SetParent(DefaultParent); 
        GetComponent<CanvasGroup>().blocksRaycasts = true; // �������� ������� ����� ����� ����������� �� ����

        transform.SetSiblingIndex(TempCardGo.transform.GetSiblingIndex()); // ����� ����� ������ ����� ������� ������� ������ ������� ��� � ��������� 
        TempCardGo.transform.SetParent(GameObject.Find("Canvas").transform); 
        TempCardGo.transform.localPosition = new Vector3(2500, 0); 
    }

    void CheckPosition() // ������� ��� �������� �� ��� ��� �������� �����, ��� ���� ��������� ����� ������������ � ������ ������ �� ��������� � ������ ������ 
    {
        int newIndex = DefaultTempCardParent.childCount;

        for (int i = 0; i < DefaultTempCardParent.childCount; i++)
        {
            if (transform.position.x < DefaultTempCardParent.GetChild(i).position.x)
            {
                newIndex = i;

                if (TempCardGo.transform.GetSiblingIndex() < newIndex)
                {
                    newIndex--;
                }

                break;
            }
        }

        if(TempCardGo.transform.parent == DefaultParent)
        {
            newIndex = startID;
        }

        TempCardGo.transform.SetSiblingIndex(newIndex);
    }

    public void MoveToField(Transform field)
    {
        transform.DOMove(field.position, .5f);
    }
    public void MoveToFieldQuickly(Transform field)
    {
        transform.DOMove(field.position, .2f);
    }
}
