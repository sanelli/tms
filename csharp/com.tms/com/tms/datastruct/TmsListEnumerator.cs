using System;
using System.Collections;
using System.Collections.Generic;

namespace com.tms.datastruct {

   public class TmsListEnumerator<T> : IEnumerator<T> {
      private ITmsList<T> _list;
      private int _currentIndex;

      public TmsListEnumerator(ITmsList<T> list){
         _list = list;
         Reset();
      }

      public T Current => _list[_currentIndex];

      object IEnumerator.Current => Current;

      public bool MoveNext() { 
         _currentIndex++;
         return _currentIndex <= _list.MaxIndex && _list.Count() > 0;
      }

      public void Reset() {
         _currentIndex = _list.MinIndex - 1;
      }

      void IDisposable.Dispose(){ _list = null; }

   }

}