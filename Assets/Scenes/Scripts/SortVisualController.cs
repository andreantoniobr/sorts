using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortVisualController : MonoBehaviour
{
    [SerializeField] private Bar barBase;
    [SerializeField] private int mergeSortOffsetY = 20;
    [SerializeField] private int heapSortOffsetY = -20;
    [SerializeField] private int barsAmount = 200;
    [SerializeField] private int distanceX = 20;
    [SerializeField] private int[] arr;
    [SerializeField] private MergeSortAlgorithm mergeSortAlgorithm;
    [SerializeField] private HeapSortAlgorithm heapSortAlgorithm;

    private int minBarSize = 1;
    private int maxValue;
    private List<Bar> mergeSortBars = new List<Bar>();
    private List<Bar> hepSortBars = new List<Bar>();

    private void Awake()
    {
        arr = ArrayGenerator.GetRandomArr(minBarSize, barsAmount);
        maxValue = arr.Max();

        SubscribeInEvents();

        StartBars(mergeSortBars, mergeSortOffsetY);
        StartBars(hepSortBars, heapSortOffsetY);
    }

    private void StartBars(List<Bar> listBars, int offsetY)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Bar bar = Instantiate(barBase);
            UpdateBarScaleyY(bar, arr[i]);
            UpdateBarPositionX(bar, i);
            UpdateBarPositionY(bar, arr[i], offsetY);
            UpdateBarcolor(bar, arr[i]);
            listBars.Add(bar);
        }
    }

    private void OnDestroy()
    {
        UnsubscribeInEvents();
    }

    private void SubscribeInEvents()
    {
        if (mergeSortAlgorithm)
        {
            mergeSortAlgorithm.UpdateValueEvent += OnMergSortUpdateBar;
        }

        if (heapSortAlgorithm)
        {
            heapSortAlgorithm.UpdateValueEvent += OnHeapSortUpdateBar;
        }
    }

    private void UnsubscribeInEvents()
    {
        if (mergeSortAlgorithm)
        {
            mergeSortAlgorithm.UpdateValueEvent -= OnMergSortUpdateBar;
        }

        if (heapSortAlgorithm)
        {
            heapSortAlgorithm.UpdateValueEvent -= OnHeapSortUpdateBar;
        }
    }

    private void Start()
    {    
        if (mergeSortAlgorithm)
        {            
            mergeSortAlgorithm.Arr = arr;
            mergeSortAlgorithm.Play();
        }

        if (heapSortAlgorithm)
        {
            heapSortAlgorithm.Arr = (int[])arr.Clone();
            heapSortAlgorithm.Play();
        }
    }

    void UpdateBarScaleyY(Bar bar, int height)
    {
        Vector3 scale = bar.transform.localScale;
        scale.y = height / 2f;
        bar.transform.localScale = scale;
    }

    void UpdateBarPositionX(Bar bar, int index)
    {
        Vector3 position = Vector3.zero;
        position.x += index * distanceX;
        bar.Transform.position = position;
    }

    void UpdateBarPositionY(Bar bar, int height, int offsetY)
    {
        Vector3 position = bar.Transform.position;
        position.y = offsetY + (height * 5 / 2f);
        bar.Transform.position = position;
    }

    void UpdateBarcolor(Bar bar, int height)
    {
        bar.SetColor(height, maxValue);
    }

    private void OnMergSortUpdateBar(int index, int height)
    {
        Bar bar = mergeSortBars[index];
        OnUpdateBar(bar, height, mergeSortOffsetY);
    }

    private void OnHeapSortUpdateBar(int index, int height)
    {
        Bar bar = hepSortBars[index];
        OnUpdateBar(bar, height, heapSortOffsetY);
    }

    private void OnUpdateBar(Bar bar, int height, int offsetY)
    {
        UpdateBarScaleyY(bar, height);
        UpdateBarPositionY(bar, height, offsetY);
        UpdateBarcolor(bar, height);
    }
}

