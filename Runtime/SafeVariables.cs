using System;

namespace CustomTool.SafeVariables {
    /// <summary> Variable que contiene una bool con un offset añadido, que ofusca su valor real en memoria. </summary>
    public struct SafeBool {
        // ==================== VARIABLES ===================
        private static Random random = new();
        
        private int offset;
        private int value;

        // ==================== METODOS ====================
        public SafeBool(bool _value = false) {
            offset = random.Next(-1000, +1000);
            value = (_value ? 1 : 0) + offset;
        }
        
        public bool Value {
            readonly get => (value - offset) == 1;
            set => this = new(value);
        }

        public void Dispose() {
            offset = 0;
            value = 0;
        }
    }
    
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    /// <summary> Variable que contiene un int con un offset añadido, que ofusca su valor real en memoria. </summary>
    public struct SafeInt {
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
        // ==================== VARIABLES ===================
        private static Random random = new();
        
        private int offset;
        private int value;
        
        // ==================== METODOS ====================
        public SafeInt(int _value = 0) {
            offset = random.Next(-1000, +1000);
            value = _value + offset;
        }
        
        public int Value {
            readonly get => value - offset;
            set => this = new(value);
        }

        public void Dispose() {
            offset = 0;
            value = 0;
        }

        public override readonly string ToString() {
            return Value.ToString();
        }

        #region Operators
        public static SafeInt operator +(SafeInt i1, SafeInt i2) {
            return new(i1.Value + i2.Value);
        }

        public static SafeInt operator +(SafeInt i1, SafeFloat f2) {
            return new((int)Math.Round(i1.Value + f2.Value));
        }

        public static SafeInt operator ++(SafeInt i1) {
            return new(i1.Value + 1);
        }

        public static SafeInt operator -(SafeInt i1, SafeInt i2) {
            return new(i1.Value - i2.Value);
        }

        public static SafeInt operator -(SafeInt i1, SafeFloat f2) {
            return new((int)Math.Round(i1.Value - f2.Value));
        }
        
        public static SafeInt operator --(SafeInt i1) {
            return new(i1.Value - 1);
        }

        public static SafeInt operator /(SafeInt i1, SafeInt i2) {
            return new(i1.Value / i2.Value);
        }

        public static SafeInt operator /(SafeInt i1, SafeFloat f2) {
            return new((int)Math.Round(i1.Value / f2.Value));
        }

        public static SafeInt operator *(SafeInt i1, SafeInt i2) {
            return new(i1.Value * i2.Value);
        }

        public static SafeInt operator *(SafeInt i1, SafeFloat f2) {
            return new((int)Math.Round(i1.Value * f2.Value));
        }

        public static SafeInt operator %(SafeInt i1, SafeInt i2)  {
            return new(i1.Value % i2.Value);
        }

        public static SafeInt operator %(SafeInt i1, SafeFloat f2) {
            return new((int)Math.Round(i1.Value % f2.Value));
        }

        public static bool operator ==(SafeInt i1, SafeInt i2) {
            return i1.Value == i2.Value;
        }

        public static bool operator !=(SafeInt i1, SafeInt i2) {
            return i1.Value != i2.Value;
        }

        public static bool operator ==(SafeInt i1, SafeFloat f2) {
            return i1.Value == f2.Value;
        }

        public static bool operator !=(SafeInt i1, SafeFloat f2) {
            return i1.Value != f2.Value;
        }

        // public static bool operator ==(SafeInt i1, SafeLong l2) {
        //     return i1.Value == l2.Value;
        // }

        // public static bool operator !=(SafeInt i1, SafeLong l2) {
        //     return i1.Value != l2.Value;
        // }

        public static bool operator >(SafeInt i1, SafeInt i2) {
            return i1.Value > i2.Value;
        }

        public static bool operator <(SafeInt i1, SafeInt i2) {
            return i1.Value < i2.Value;
        }

        public static bool operator >=(SafeInt i1, SafeInt i2) {
            return i1.Value >= i2.Value;
        }
        
        public static bool operator <=(SafeInt i1, SafeInt i2) {
            return i1.Value <= i2.Value;
        }

        public static bool operator >(SafeInt i1, SafeFloat f2) {
            return i1.Value > f2.Value;
        }
        
        public static bool operator <(SafeInt i1, SafeFloat f2) {
            return i1.Value < f2.Value;
        }

        public static bool operator >=(SafeInt i1, SafeFloat f2) {
            return i1.Value >= f2.Value;
        }

        public static bool operator <=(SafeInt i1, SafeFloat f2) {
            return i1.Value <= f2.Value;
        }

        // public static bool operator >(SafeInt i1, SafeLong l2) {
        //     return i1.Value > l2.Value;
        // }

        // public static bool operator <(SafeInt i1, SafeLong l2) {
        //     return i1.Value < l2.Value;
        // }

        // public static bool operator >=(SafeInt i1, SafeLong l2) {
        //     return i1.Value >= l2.Value;
        // }

        // public static bool operator <=(SafeInt i1, SafeLong l2) {
        //     return i1.Value <= l2.Value;
        // }
        #endregion
    }

#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    /// <summary> Variable que contiene un float con un offset añadido, que ofusca su valor real en memoria. </summary>
    public struct SafeFloat {
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
        // ==================== VARIABLES ===================
        private static Random random = new();

        private float offset;
        private float value;

        // ==================== METODOS ====================
        public SafeFloat(float _value = 0) {
            offset = random.Next(-1000, +1000);
            value = _value + offset;
        }
        
        public float Value {
            readonly get => value - offset;
            set => this = new(value);
        }

        public void Dispose() {
            offset = 0;
            value = 0;
        }

        public override readonly string ToString() {
            return Value.ToString();
        }

        #region Operators
        public static SafeFloat operator +(SafeFloat f1, SafeFloat f2) {
            return new SafeFloat(f1.Value + f2.Value);
        }

        public static SafeFloat operator +(SafeFloat f1, SafeInt i2) {
            return new SafeFloat(f1.Value + i2.Value);
        }

        public static SafeFloat operator ++(SafeFloat f1) {
            return new SafeFloat(f1.Value + 1);
        }

        public static SafeFloat operator -(SafeFloat f1, SafeFloat f2) {
            return new SafeFloat(f1.Value - f2.Value);
        }

        public static SafeFloat operator -(SafeFloat f1, SafeInt i2) {
            return new SafeFloat(f1.Value - i2.Value);
        }

        public static SafeFloat operator --(SafeFloat f1) {
            return new SafeFloat(f1.Value - 1);
        }

        public static SafeFloat operator /(SafeFloat f1, SafeFloat f2) {
            return new SafeFloat(f1.Value / f2.Value);
        }

        public static SafeFloat operator /(SafeFloat f1, SafeInt i2) {
            return new SafeFloat(f1.Value / i2.Value);
        }

        public static SafeFloat operator *(SafeFloat f1, SafeFloat f2) {
            return new SafeFloat(f1.Value * f2.Value);
        }

        public static SafeFloat operator *(SafeFloat f1, SafeInt i2) {
            return new SafeFloat(f1.Value * i2.Value);
        }

        public static SafeFloat operator %(SafeFloat f1, SafeFloat f2) {
            return new SafeFloat(f1.Value % f2.Value);
        }

        public static SafeFloat operator %(SafeFloat f1, SafeInt i2) {
            return new SafeFloat(f1.Value % i2.Value);
        }

        public static bool operator ==(SafeFloat f1, SafeFloat f2) {
            return f1.Value == f2.Value;
        }

        public static bool operator !=(SafeFloat f1, SafeFloat f2) {
            return f1.Value != f2.Value;
        }

        public static bool operator ==(SafeFloat f1, SafeInt i2) {
            return f1.Value == i2.Value;
        }

        public static bool operator !=(SafeFloat f1, SafeInt i2) {
            return f1.Value != i2.Value;
        }

        // public static bool operator ==(SafeFloat f1, SafeLong l2) {
        //     return f1.Value == l2.Value;
        // }

        // public static bool operator !=(SafeFloat f1, SafeLong l2) {
        //     return f1.Value != l2.Value;
        // }

        public static bool operator >(SafeFloat f1, SafeFloat f2) {
            return f1.Value > f2.Value;
        }

        public static bool operator <(SafeFloat f1, SafeFloat f2) {
            return f1.Value < f2.Value;
        }

        public static bool operator >=(SafeFloat f1, SafeFloat f2) {
            return f1.Value >= f2.Value;
        }

        public static bool operator <=(SafeFloat f1, SafeFloat f2) {
            return f1.Value <= f2.Value;
        }

        public static bool operator >(SafeFloat f1, SafeInt i2) {
            return f1.Value > i2.Value;
        }

        public static bool operator <(SafeFloat f1, SafeInt i2) {
            return f1.Value < i2.Value;
        }

        public static bool operator >=(SafeFloat f1, SafeInt i2) {
            return f1.Value >= i2.Value;
        }

        public static bool operator <=(SafeFloat f1, SafeInt i2) {
            return f1.Value <= i2.Value;
        }

        // public static bool operator >(SafeFloat f1, SafeLong l2) {
        //     return f1.Value > l2.Value;
        // }

        // public static bool operator <(SafeFloat f1, SafeLong l2) {
        //     return f1.Value < l2.Value;
        // }

        // public static bool operator >=(SafeFloat f1, SafeLong l2) {
        //     return f1.Value >= l2.Value;
        // }
        
        // public static bool operator <=(SafeFloat f1, SafeLong l2) {
        //     return f1.Value <= l2.Value;
        // }
        #endregion
    }
}