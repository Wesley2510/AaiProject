using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BinaryHeapNode<T>
{
    private double _NodeItem;
    public T Value { get; set; }

    public BinaryHeapNode(double pId, T pValue)
    {
        _NodeItem = pId;
        this.Value = pValue;
    }
    public double getID()
    {
        return _NodeItem;
    }
    public void setvalue(double Id)
    {
        _NodeItem = Id;
    }

    public override string ToString()
    {
        return _NodeItem.ToString();
    }
}

public class MinMaxBinaryHeap<T>
{
    private BinaryHeapNode<T>[] _array;
    private int _currentSize;
    private int _maxSize;
    public bool Max = false;

    public MinMaxBinaryHeap(int pSize)
    {
        _array = new BinaryHeapNode<T>[pSize];
        _currentSize = 0;
        _maxSize = pSize;
    }

    public bool IsEmpty()
    {
        return _currentSize == 0;
    }

    public bool add(double pId, T pValue)
    {
        if (_currentSize == _maxSize)
            return false;
        _array[_currentSize] = new BinaryHeapNode<T>(pId, pValue);
        percolateUp(_currentSize++);
        return true;
    }

    public void percolateUp(int pIndex)
    {
        int parent = (pIndex - 1) / 2;
        BinaryHeapNode<T> bottom = _array[pIndex];
        while ((!Max && pIndex > 0 && _array[parent].getID() > bottom.getID())
            || (Max && pIndex > 0 && _array[parent].getID() < bottom.getID()))
        {
            _array[pIndex] = _array[parent];
            pIndex = parent;
            parent = (parent - 1) / 2;
        }
        _array[pIndex] = bottom;
    }

    public T remove()
    {
        BinaryHeapNode<T> root = _array[0];
        _array[0] = _array[--_currentSize];
        percolateDown(0);
        return root.Value;
    }

    private void percolateDown(int pIndex)
    {
        int largerChild;
        BinaryHeapNode<T> top = _array[pIndex];
        while (pIndex < _currentSize / 2)
        {
            int leftChild = 2 * pIndex + 1;
            int rightChild = leftChild + 1;
            if ((!Max && rightChild < _currentSize && _array[leftChild].getID() > _array[rightChild].getID())
                || (Max && rightChild > _currentSize && _array[leftChild].getID() < _array[rightChild].getID()))
                largerChild = rightChild;
            else
                largerChild = leftChild;
            if ((!Max && top.getID() <= _array[largerChild].getID())
                || (Max && top.getID() >= _array[largerChild].getID()))
                break;
            _array[pIndex] = _array[largerChild];
            pIndex = largerChild;
        }
        _array[pIndex] = top;
    }

    public void addFreely(double pId, T pValue)
    {
        _array[_currentSize] = new BinaryHeapNode<T>(pId, pValue);
        _currentSize++;
    }

    public void buildHeap()
    {
        for (int i = _currentSize - 1; i >= 0; i--)
        {
            percolateDown(i);
        }
    }

    public void display()
    {
        Console.WriteLine();
        Console.Write("Elements of the Heap Array are : ");
        for (int m = 0; m < _currentSize; m++)
            if (_array[m] != null)
                Console.Write(_array[m] + " ");
            else
                Console.Write("-- ");
        Console.WriteLine();
        int emptyLeaf = 32;
        int itemsPerRow = 1;
        int column = 0;
        int j = 0;
        String separator = "...............................";
        Console.WriteLine(separator + separator);
        while (_currentSize > 0)
        {
            if (column == 0)
                for (int k = 0; k < emptyLeaf; k++)
                    Console.Write(' ');
            Console.Write(_array[j]);

            if (++j == _currentSize)
                break;
            if (++column == itemsPerRow)
            {
                emptyLeaf /= 2;
                itemsPerRow *= 2;
                column = 0;
                Console.WriteLine();
            }
            else
                for (int k = 0; k < emptyLeaf * 2 - 2; k++)
                    Console.Write(' ');
        }
        Console.WriteLine("\n" + separator + separator);
    }
}

