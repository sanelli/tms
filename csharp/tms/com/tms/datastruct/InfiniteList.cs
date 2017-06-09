using System;
using System.Collections.Generic;

namespace com.tms.datastruct {
   class InfiniteList<T> {
      private List<T> _list;
      private int _startIndex = 0;
      private T _nullValue = default(T);
      private bool _increasing = false;

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
         while(_list.Count < size) 
         _list.Add(_nullValue);
      }
   }
}

