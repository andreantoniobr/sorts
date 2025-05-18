using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArrayGenerator
{
    public static int[] GetRandomArr(int start, int end)
    {
        int size = end - start + 1;
        int[] array = new int[size];

        // Preenche o array com todos os números do intervalo [inicio, fim]
        for (int i = 0; i < size; i++)
        {
            array[i] = start + i;
        }

        // Embaralha o array (Fisher-Yates)
        System.Random rng = new System.Random();
        for (int i = size - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        return array;
    }
}


