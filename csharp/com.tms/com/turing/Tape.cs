using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using com.tms.datastruct;

namespace com.tms.turing {

   public class Tape<TSymbol> : IReadonlyTape<TSymbol> {
      private readonly InfiniteBidirctionalList<TSymbol> _tape;
      private int _position;
      private readonly SymbolSerializer<TSymbol> _serializer;
      private readonly int _initialPosition;

      public TSymbol Null => _tape.Null;

      public Tape(SymbolSerializer<TSymbol> serializer, TSymbol nullValue = default(TSymbol), int position = 0) {
         _serializer = serializer;
         _tape = new InfiniteBidirctionalList<TSymbol>(nullValue);
         _position = position;
         _initialPosition = position;
      }

      public void FillFromString(string s, int position = 0) { 
         Reset();
         _position = position;
         var items = s.Split(_serializer.Separator).Where(x => !string.IsNullOrEmpty(x));
         foreach(var item in items) { 
            _tape.Append(_serializer.FromString(item));
         }
      }

      public void Set(TSymbol item) {
         _tape[_position] = item;
      }
      public TSymbol Current => _tape[_position];

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

      public string ToPlainString(){
        return string.Join(string.Empty, _tape.Select( i => _serializer.ToString(i)));
      }

      public IEnumerable<TSymbol> CurrentTape => _tape;
      public int Position => _position;

      public Tape<TSymbol> Clone() {
         var tape = new Tape<TSymbol>(_serializer, Null, _initialPosition);
         tape.FillFromString(ToString());
         return tape;
       }

   }

}