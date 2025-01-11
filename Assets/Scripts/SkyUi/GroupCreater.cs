using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupCreater : MonoBehaviour
{
    #region Properties
    [Header("Родитель групп")]
    public Transform groupParent;
    [Header("Текущие группы")]
    [SerializeField]
    private List<GameObject> groupBox;
    [Header("Максимум элементов в группе")]
    public int groupSize;
    [Header("Префабы группы и элемента")]
    public GameObject groupSample;
    public GameObject buttonSample;
    [Header("Стрелки для пролистывания")]
    public GameObject leftArrow;
    public GameObject rightArrow;
    [HideInInspector]
    public GameObject _newItem;
    private int _id = 0;
    #endregion

    #region Creater
    public void CreateNewItem()
    {
        _id = groupBox.Count - 1;
        ChangeList(true);
        if (groupBox[_id].transform.childCount >= groupSize)// иначе создаём новую страницу и повторяем цикл
        {
            if (_id + 1 >= groupBox.Count)
            {
                groupBox.Add(Instantiate(groupSample, groupParent));
            }
            ChangeList(true);
        }
        if (groupBox[_id].transform.childCount < groupSize) // если детей меньше заданного количества, то добавляем и заканчиваем
        {
            _newItem = Instantiate(buttonSample, groupBox[_id].transform);
            //groupBox[_id].GetComponent<GroupList>().items.Add(_newItem); // возможно пригодится
        }
    }

    
    public void ChangeList(bool isNext) // функция листает страницы
    {
        if (isNext && _id + 1 < groupBox.Count) _id++;
        else if (!isNext && _id - 1 >= 0) _id--;

        for (int i = 0; i < groupBox.Count; i++)
        {
            groupBox[i].SetActive(false);
        }
        groupBox[_id].SetActive(true);

        if (_id == 0) leftArrow.SetActive(false);
        else leftArrow.SetActive(true);
        if (_id >= groupBox.Count - 1) rightArrow.SetActive(false);
        else rightArrow.SetActive(true);
    }
    #endregion

    #region Default
    private void Start() => ChangeList(true);
    public GameObject GetItem() => _newItem; // возвращает только что созданный итем
    public void DeleteItem() // удаляет итем, его родитель остаётся
    {
        Destroy(_newItem);
        if (groupBox[_id].transform.childCount <= 1) ChangeList(false);
    }
    #endregion
}
