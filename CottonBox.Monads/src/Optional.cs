using System;

namespace CottonBox.Monads
{
    public static class Optional
    {
        public sealed class OptionalNone { }
        public static OptionalNone None { get; } = new OptionalNone();
        
        public static Optional<T> Some<T>(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value;
        }
    }

    public readonly struct Optional<T>
    {
        private readonly T _currentValue;
        private readonly bool _hasValue;

        private Optional(T value)
        {
            _currentValue = value;
            _hasValue = true; 
        }

        public static implicit operator Optional<T>(T value) =>
            value == null ? new Optional<T>() : new Optional<T>(value);
        
        public static implicit operator Optional<T>(Optional.OptionalNone value) => 
            new Optional<T>();

        public bool TryGetValue(out T value)
        {
            if (_hasValue)
            {
                value = _currentValue;
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
