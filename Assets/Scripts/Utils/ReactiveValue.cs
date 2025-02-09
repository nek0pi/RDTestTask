using System;
using UnityEngine;

namespace Utils
{
    public abstract class ReactiveValue<T>
    {
        public Action<T> OnChanged;
        public Action<T, T> OnChangedWithPrevious;
        public T Min { get; protected set; }
        public T Max { get; protected set; }
        public T Default { get; protected set; }

        public T Previous { get; protected set; }

        protected T _current;

        public void SetToDefault()
        {
            _current = Default;
        }

        public void ChangeMax(T max)
        {
            Max = max;
        }

        public void ChangeMin(T min)
        {
            Min = min;
        }
    }

    public class ReactiveFloat : ReactiveValue<float>
    {
        public float Current
        {
            get => _current;
            set
            {
                if (Math.Abs(value - _current) < 0.0001) return;
                Previous = _current;
                _current = Math.Clamp(value, Min, Max);
                OnChanged?.Invoke(_current);
                OnChangedWithPrevious?.Invoke(_current, Previous);
            }
        }

        public ReactiveFloat(float value)
        {
            Min = 0;
            Max = float.MaxValue;
            Current = value;
        }

        public ReactiveFloat(float def = 0, float max = float.MaxValue, float min = 0)
        {
            Max = max;
            Min = min;
            Default = def;
            _current = def;
        }
    }

    public class ReactiveInt : ReactiveValue<int>
    {
        public int Current
        {
            get => _current;
            set
            {
                if (value == Current) return;

                Previous = _current;
                _current = Math.Clamp(value, Min, Max);
                OnChanged?.Invoke(_current);
                OnChangedWithPrevious?.Invoke(_current, Previous);
            }
        }

        public ReactiveInt(int def = 0, int max = int.MaxValue, int min = 0)
        {
            Max = max;
            Min = min;
            Default = def;
            _current = def;
        }
    }

    public class ReactiveVector3 : ReactiveValue<Vector3>
    {
        public Vector3 Current
        {
            get => _current;
            set
            {
                if (value == Current) return;

                Previous = _current;

                _current = value;
                OnChanged?.Invoke(_current);
                OnChangedWithPrevious?.Invoke(_current, Previous);
            }
        }

        public ReactiveVector3(Vector3 def)
        {
            Default = def;
            _current = def;
        }
    }

    public class ReactiveBool : ReactiveValue<bool>
    {
        public bool Current
        {
            get => _current;
            set
            {
                if (value == Current) return;

                Previous = _current;

                _current = value;
                OnChanged?.Invoke(_current);
                OnChangedWithPrevious?.Invoke(_current, Previous);
            }
        }

        public ReactiveBool(bool def = false)
        {
            Default = def;
            _current = def;
        }
    }
}