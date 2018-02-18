namespace NenrDZ1.Domains
{
    public class DomainElement
    {
        private readonly int[] _values;

        public DomainElement(int[] values)
        {
            _values = values;
        }

        public int GetNumberOfComponents()
        {
            return _values.Length;
        }

        public int GetComponentValue(int index)
        {
            return _values[index];
        }

        public static DomainElement Of(params int[] values)
        { 
            return new DomainElement(values);
        }

        public override string ToString()
        {
            if (GetNumberOfComponents() == 1)
            {
                return GetComponentValue(0).ToString();
            }

            return "(" + string.Join(",", _values) + ")";
        }

        protected bool Equals(DomainElement other)
        {
            return Equals(_values, other._values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DomainElement) obj);
        }

        public override int GetHashCode()
        {
            return (_values != null ? _values.GetHashCode() : 0);
        }

        public int this[int i] => GetComponentValue(i);
    }
}
