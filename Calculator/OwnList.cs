namespace Calculator;

public class OwnList<T>
{
    private T[] _array= new T[1];
    private int _capacity = 1;
    private int _pointer = 0;

    public void Append(T element)
    {
        if (_pointer == _capacity)
        {
            T[] tempArray = new T[_capacity * 2];
            for (int index = 0; index < _pointer; index++)
            {
                tempArray[index] = _array[index];
            }

            _capacity *= 2;
            _array = tempArray;
        }

        _array[_pointer] = element;
        _pointer++;
    }

    public void Insert(T element, int index)
    {
        if (index > _pointer)
        {
            throw new Exception("There is no such index to insert to.");
        }
        Append(_array[_pointer - 1]);// to make sure that size is enough
        for (int cycleIndex = _pointer; cycleIndex > index; cycleIndex--)
        {
            _array[cycleIndex] = _array[cycleIndex - 1];
        }

        _array[index] = element;
    }

    public void RemoveByIndex(int index)
    {
        if (index > _pointer)
        {
            throw new Exception("Index is to large for this list");
        }

        _pointer--;
        for (int primaryIndex = index; primaryIndex < _pointer; primaryIndex++)
        {
            _array[primaryIndex] = _array[primaryIndex + 1];
        }

        _array[_pointer] = default(T);

    }
    public void Remove(T element)// removes first element equal to T
    {
        for (int index = 0; index < _pointer; index++)
        {
            if (EqualityComparer<T>.Default.Equals(_array[index], element))
            {
                RemoveByIndex(index);
            }
        }
    }
    public void ShowList()
    {
        for (int index = 0; index < _pointer; index++)
        {
            Console.WriteLine(_array[index]);
        }
    }
}