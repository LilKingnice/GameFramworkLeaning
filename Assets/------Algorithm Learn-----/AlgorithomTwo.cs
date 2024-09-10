using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlgorithomTwo : MonoBehaviour
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
            QuickShort(mylist, 0, mylist.Length - 1);
            Print_list(mylist, nameof(QuickShort));
        }

        if (pc.aKey.wasPressedThisFrame)
        {
        }
    }

    #region 搜索算法

    //Debug.Log(BinarySearch(list, 4, 0, list.Length - 1, isAscending));
    public static int BinarySearch(int[] list, int target, int left, int right, bool isAscending = true)
    {
        int mid = (left + right) / 2;
        if (list[mid] == target) return mid;
        if (list[mid] > target) return BinarySearch(list, target, left, mid - 1);
        return BinarySearch(list, target, mid + 1, right);
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
        for (int i = 0; i < list.Length; i++)
        {
            for (int j = 0; j < list.Length - 1 - i; j++)
            {
                if (list[j] < list[j + 1])
                {
                    (list[j], list[j + 1]) = (list[j + 1], list[j]);
                }
            }
        }

        Print_list(list, nameof(Bubble));
        Debug.Log(BinarySearch(list, 4, 0, list.Length - 1));
    }


    /// <summary>
    /// 插入排序
    /// 核心思路：
    /// 每次都是找一个规则合适的最小（大）值，取出来，
    /// 一直从前面已经排列好顺序的队列中对比，不合适就将当前对比的数据往后挪一个位置，
    /// 知道找到合适的位置，就插入到合适的位置
    /// </summary>
    /// <param name="list"></param>
    /// <param name="isAscending">排序规则</param>
    public static void Insertion(int[] list, bool isAscending = true)
    {
        int currentIndex, currentNumber;
        for (int i = 1; i < list.Length; i++)
        {
            currentIndex = i;
            currentNumber = list[i];
            for (int j = i - 1; j >= 0; j--)
            {
                if (list[j] > currentNumber)
                {
                    list[j + 1] = list[j];
                    currentIndex = j;
                }
            }

            list[currentIndex] = currentNumber;
        }


        Print_list(list, nameof(Insertion));
    }

    /// <summary>
    /// 选择排序
    /// 核心思路：
    /// 这是通常的排序思路，选一个最小（大）值与当前存储的索引值做交换
    /// </summary>
    /// <param name="list"></param>
    /// <param name="isAscending"></param>
    public static void Selection(int[] list, bool isAscending = true)
    {
        // int currentIndex;
        //
        // for (int i = 0; i < list.Length - 1; i++)
        // {
        //     currentIndex = i;
        //     for (int j = i + 1; j < list.Length; j++)
        //     {
        //         if (list[j] < list[currentIndex])
        //         {
        //             currentIndex = j;
        //         }
        //     }
        //
        //     (list[i], list[currentIndex]) = (list[currentIndex], list[i]);
        // }


        //两头查找
        int left = 0, right = list.Length - 1;
        int maxIndex, minIndex;
        while (left < right)
        {
            maxIndex = right;
            minIndex = left;
            for (int i = left; i < list.Length; i++)
            {
                if (list[minIndex] > list[i]) minIndex = i;
                if (list[maxIndex] < list[i]) maxIndex = i;
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
    public void QuickShort(int[] list, int start, int end)
    {
        // //joker双指针，需要注意的点，轴必须先从right开始选起
        // if (start >= end) return;
        // int left = start, right = end, pivot = list[left];
        // while (left < right)
        // {
        //     while (left < right && list[right] >= pivot) right--;
        //     list[left] = list[right];
        //
        //     while (left < right && list[left] <= pivot) left++;
        //     list[right] = list[left];
        // }
        //
        // list[left] = pivot;
        // QuickShort(list, start, left - 1);
        // QuickShort(list, left + 1, end);


        //这是gpt给的方案
        if (start >= end) return;

        int left = start, right = end;
        int pivot = list[(start + end) / 2];

        // 分区过程
        while (left <= right)
        {
            while (list[left] < pivot) left++;
            while (list[right] > pivot) right--;
            if (left <= right)
            {
                // 交换左右指针所指向的元素
                (list[left], list[right]) = (list[right], list[left]);
                left++;
                right--;
            }
        }

        // 递归调用
        if (start < right) QuickShort(list, start, right);
        if (left < end) QuickShort(list, left, end);
    }

    #endregion
}