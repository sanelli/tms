using System;
using System.Linq;
using com.tms.datastruct;

namespace com.tms.turing {

   class Tape<T> {
      private InfiniteBidirctionalList<T> _tape;
      private int _position;
      TapeItemSerializer<T> _serializer;

      public T Null => _tape.Null;

      public Tape(TapeItemSerializer<T> serializer, T nullValue = default(T), int position = 0) {
         _serializer = serializer;
         _tape = new InfiniteBidirctionalList<T>(nullValue);
         _position = position;
      }

      public void FillFromString(string s, int position = 0) { 
         Reset();
         _position = position;
         var items = s.Split(_serializer.Separator);
         foreach(var item in items) { 
            _tape.Append(_serializer.FromString(item));
         }
      }

      public void Set(T item) {
         _tape[_position] = item;
      }
      public T Current => _tape[_position];

      public void Shift(MoveAction action, int shift = 1) {
         if(shift < 1)
            throw new ArgumentException(nameof(shift));
         _position += ((int)action) * shift;
      }

      public void Strip(){ _tape.Strip(); }

      public void Reset() {
         _tape.Clear();
         _position = 0;
      }

      public override string ToString() {
         return $"{_serializer.Separator}{string.Join(_serializer.Separator.ToString(), _tape.Select( i => _serializer.ToString(i)))}{_serializer.Separator}";
      }

   }

}