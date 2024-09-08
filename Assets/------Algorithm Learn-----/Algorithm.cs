using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class Algorithm : MonoBehaviour
{
    private int[] mylist = { 9, 5, 1, 12, 33, 2, 4, 6, 18 };

    [SerializeField] private bool AscendingControl;

    private int listLength;

    private void Start()
    {
        Print_list(mylist, nameof(Start));
        listLength = mylist.Length;
    }

    private void Update()
    {
        var pc = Keyboard.current;
        if (pc is null)
        {
            return;
        }

        if (pc.bKey.wasPressedThisFrame)
        {
            // Bubble(mylist, AscendingControl);
            // Insertion(mylist, AscendingControl);
            // Selection(mylist, AscendingControl);
            QuickShort(mylist, 0, 8);
        }
    }

    #region 搜索算法

    //Debug.Log(BinarySearch(list, 4, 0, list.Length - 1, isAscending));
    public static int BinarySearch(int[] list, int target, int left, int right, bool isAscending = true)
    {
        int mid = (left + right) / 2;
        if (list[mid] == target) return mid;

        if (isAscending ? list[mid] < target : list[mid] > target)
            return BinarySearch(list, target, mid + 1, right, isAscending);
        return BinarySearch(list, target, left, mid - 1, isAscending);
    }

    #endregion

    #region 排序算法

    public static void Print_list(int[] printList, string name = "")
    {
        StringBuilder mybuilder = new StringBuilder();
        for (int i = 0; i < printList.Length; i++)
        {
            if (i != printList.Length - 1)
                mybuilder.Append(printList[i] + ",");
            else
                mybuilder.Append(printList[i]);
        }

        mybuilder.Append(@"    FunctionName:" + name);
        Debug.Log(mybuilder);
    }


    /// <summary>
    /// 冒泡排序
    /// </summary>
    /// <param name="list"></param>
    /// <param name="isAscending">排序规则</param>
    public static void Bubble(int[] list, bool isAscending = true)
    {
        for (int i = 0; i < list.Length - 1; i++)
        {
            for (int j = 0; j < list.Length - i - 1; j++)
            {
                if (isAscending ? list[j] > list[j + 1] : list[j] < list[j + 1])
                {
                    (list[j], list[j + 1]) = (list[j + 1], list[j]);
                }
            }
        }

        Print_list(list, nameof(Bubble));
    }


    /// <summary>
    /// 插入排序
    /// </summary>
    /// <param name="list"></param>
    /// <param name="isAscending">排序规则</param>
    public static void Insertion(int[] list, bool isAscending = true)
    {
        // int tempIndex;
        // int currentNumber;
        //
        // for (int i = 1; i < list.Length; i++)
        // {
        //     currentNumber = list[i];
        //     tempIndex = i;
        //     for (int j = i-1; j>=0; j--)
        //     {
        //         if (list[j]>currentNumber)
        //         {
        //             list[j+1] = list[j];
        //             tempIndex = j;
        //         }
        //         
        //     }
        //     list[tempIndex] = currentNumber;
        // }

        int temp;
        int current;

        for (int i = 1; i < list.Length; i++)
        {
            current = list[i];
            temp = i;
            for (int j = i - 1; j >= 0; j--)
            {
                if (isAscending ? current < list[j] : current > list[j])
                {
                    list[j + 1] = list[j];
                    temp = j;
                }
                else
                {
                    break; //优化
                }
            }

            list[temp] = current;
        }

        Print_list(list, nameof(Insertion));
    }

    /// <summary>
    /// 选择排序
    /// </summary>
    /// <param name="list"></param>
    /// <param name="isAscending"></param>
    public static void Selection(int[] list, bool isAscending = true)
    {
        // int currentIndex;
        //
        // for (int i = 0; i < list.Length-1; i++)
        // {
        //     currentIndex = i;
        //     for (int j = i; j < list.Length; j++)
        //     {
        //         if (isAscending ? list[j] < list[i] : list[j] > list[i])
        //         {
        //             currentIndex = j;
        //         }
        //     }
        //
        //     (list[i], list[currentIndex]) = (list[currentIndex], list[i]);
        // }


        //两头找
        int left = 0;
        int right = list.Length - 1;
        int maxIndex, minIndex;
        while (left < right)
        {
            minIndex = left;
            maxIndex = right;
            for (int i = left; i < right; i++)
            {
                if (list[i] < list[minIndex]) minIndex = i;
                if (list[i] > list[maxIndex]) maxIndex = i;
            }

            (list[left], list[minIndex]) = (list[minIndex], list[left]);

            if (left == maxIndex)
            {
                maxIndex = minIndex;
            }

            (list[right], list[maxIndex]) = (list[maxIndex], list[right]);

            left++;
            right--;
        }


        Print_list(list, nameof(Selection));
    }


    /// <summary>
    /// 快速排序
    /// </summary>
    /// <param name="list"></param>
    /// <param name="isAscending"></param>
    public void QuickShort(int[] list, int start, int end, bool isAscending = true)
    {
        if (start >= end)
        {
            // Debug.Log($"{start}111111111{end}");
            return;
        }
        
        int left = start, right = end, pivot = list[left];
        while (left < right)
        {
            while (left < right && list[right] >= pivot) right--;
            list[left] = list[right];
            while (left < right && list[left] <= pivot) left++;
            list[right] = list[left];
        }
        
        list[left] = pivot;
        
        QuickShort(list, start, left - 1);
        QuickShort(list, left + 1, end);
        
        

        
        // if (start >= end)
        // {
        //     return;
        // }
        //
        // int left = start, right = end;
        // int pivot = list[(start + end) / 2];  // 选择中间位置作为基准值
        //
        // do
        // {
        //     while (isAscending ? list[left] < pivot : list[left] > pivot)
        //     {
        //         left++;
        //     }
        //     while (isAscending ? list[right] > pivot : list[right] < pivot)
        //     {
        //         right--;
        //     }
        //
        //     if (left <= right)
        //     {
        //         // 交换 list[left] 和 list[right]
        //         (list[left], list[right]) = (list[right], list[left]);
        //         left++;
        //         right--;
        //     }
        // } while (left <= right);
        //
        // // 递归排序左右两边
        // QuickShort(list, start, right);
        // QuickShort(list, left, end);
    }

    #endregion
}