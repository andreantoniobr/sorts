using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapSortAlgorithm : MonoBehaviour
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
        StartCoroutine(HeapSort(arr));
    }

    private IEnumerator HeapSort(int[] arr)
    {
        int n = arr.Length;

        // Build max heap
        for (int i = n / 2 - 1; i >= 0; i--)
        {
            yield return StartCoroutine(Heapify(arr, n, i));
        }

        // Extract elements one by one
        for (int i = n - 1; i > 0; i--)
        {
            Swap(arr, 0, i);
            UpdateBar(0, arr[0]);
            UpdateBar(i, arr[i]);
            yield return new WaitForSeconds(stepDelay);

            yield return StartCoroutine(Heapify(arr, i, 0));
        }
    }

    private IEnumerator Heapify(int[] arr, int n, int i)
    {
        int largest = i;
        int l = 2 * i + 1;
        int r = 2 * i + 2;

        if (l < n && arr[l] > arr[largest])
            largest = l;

        if (r < n && arr[r] > arr[largest])
            largest = r;

        if (largest != i)
        {
            Swap(arr, i, largest);
            UpdateBar(i, arr[i]);
            UpdateBar(largest, arr[largest]);
            yield return new WaitForSeconds(stepDelay);

            yield return StartCoroutine(Heapify(arr, n, largest));
        }
    }

    private void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    private void UpdateBar(int index, int height)
    {
        UpdateValueEvent?.Invoke(index, height);
    }
}
