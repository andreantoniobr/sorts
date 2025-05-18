using System;
using System.Collections;
using UnityEngine;


public class MergeSortAlgorithm : MonoBehaviour
{
    [SerializeField] private float stepDelay = 0.3f;
    
    private int[] arr;    

    public int[] Arr 
    { 
        get => arr;
        set => arr = value; 
    }

    public event Action<int, int> UpdateValueEvent;
  

    public void Play()
    {      
        StartCoroutine(MergeSort(arr, 0, arr.Length - 1));
    }

    IEnumerator MergeSort(int[] arr, int left, int right)
    {
        if (left >= right) yield break;

        int mid = (left + right) / 2;

        yield return StartCoroutine(MergeSort(arr, left, mid));
        yield return StartCoroutine(MergeSort(arr, mid + 1, right));
        yield return StartCoroutine(Merge(arr, left, mid, right));
    }

    IEnumerator Merge(int[] arr, int left, int mid, int right)
    {
        int[] temp = new int[right - left + 1];
        int i = left;
        int j = mid + 1;
        int k = 0;

        while (i <= mid && j <= right)
        {
            if (arr[i] <= arr[j])
                temp[k++] = arr[i++];
            else
                temp[k++] = arr[j++];
        }

        while (i <= mid)
            temp[k++] = arr[i++];

        while (j <= right)
            temp[k++] = arr[j++];

        // Copia de volta e atualiza visual
        for (int m = 0; m < temp.Length; m++)
        {
            arr[left + m] = temp[m];
            UpdateBar(left + m, temp[m]);
            yield return new WaitForSeconds(stepDelay);
        }
    }

    private void UpdateBar(int index, int height)
    {
        UpdateValueEvent?.Invoke(index, height);
    }
}