using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace com.tms.datastruct {
   public class InfiniteList<T> : ITmsList<T>{
      private List<T> _list;
      private int _startIndex = 0;
      private T _nullValue = default(T);
      private bool _increasing = false;

      public T Null => _nullValue;
      public int StartIndex => _startIndex;
      public bool Increasing => _increasing;

      public int MinIndex => _increasing ? IntToExt(0) : IntToExt(_list.Count() - 1);
      public int MaxIndex => _increasing ? IntToExt(_list.Count() - 1) : IntToExt(0);

      public InfiniteList(int startIndex = 0, T nullValue = default(T), bool increase = true){
         _list = new List<T>();
         _startIndex = startIndex;
         _nullValue = nullValue;
         _increasing = increase;
      }

      private int ExtToInt(int index){
         int iIndex = 0;
         if(_increasing){
            iIndex = index - _startIndex;
         } else {
            iIndex = -index + _startIndex;
         }
         return iIndex;
      }

      private int IntToExt(int index){
         int oIndex = 0;
         if(_increasing){
             oIndex = index + _startIndex;
         } else {
            oIndex = -index + _startIndex;
         }
         return oIndex;
      }

      private void Extend(int size) { 
         while(_list.Count() < size) 
            Append(_nullValue);
      }

      private int ExtendByExtIndex(int oIndex){
         var iIndex = ExtToInt(oIndex); 
            if(iIndex >= _list.Count())
               Extend(iIndex + 1);
         return iIndex;
      }

      public void Append(T element) { 
         _list.Add(element);
      }

      public void Strip() {
         while(_list.Count() > 0 && _list.Last().Equals(Null))
            _list.RemoveAt(_list.Count()-1);
      }

      public T this[int index]
      {
         get { return _list[ExtendByExtIndex(index)]; }
         set {  _list[ExtendByExtIndex(index)] = value; }
      }

      public void RemoveAt(int index){
         _list.RemoveAt(ExtendByExtIndex(index));
      }

      public int Count(){
         return _list.Count();
      }

      public void Clear() {
            _list.Clear();
      }

      public IEnumerator<T> GetEnumerator(){ return new TmsListEnumerator<T>(this); }
      IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
   }
}

