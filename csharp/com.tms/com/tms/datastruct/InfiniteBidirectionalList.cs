using System.Collections;
using System.Collections.Generic;

namespace com.tms.datastruct {

   public class InfiniteBidirctionalList<T> : ITmsList<T> {
      private readonly InfiniteList<T> _negative;
      private readonly InfiniteList<T> _positive;

      public int MinIndex => _negative.Count() == 0 ? _positive.MinIndex : _negative.MinIndex;
      public int MaxIndex => _positive.Count() == 0 ? _negative.MaxIndex : _positive.MaxIndex;

      public T Null => _positive.Null;

      public InfiniteBidirctionalList(T nullValue) { 
         _positive = new InfiniteList<T>(0, nullValue, true);
         _negative = new InfiniteList<T>(-1, nullValue, false);
      }

      private ITmsList<T> GetListByIndex(int index) {
         return index >= 0 ? _positive : _negative;
      }

      public void Append(T item) { 
         _positive.Append(item);
      }

      public void RemoveAt(int index){
         GetListByIndex(index).RemoveAt(index);
      }

      public T this[int index]{
         get {
            return GetListByIndex(index)[index];
         }
         set{
            GetListByIndex(index)[index] = value;
         }
      }

      public void Strip(){
         _negative.Strip();
         _positive.Strip();
      }

      public int Count(){
         return _negative.Count() + _positive.Count();
      }

      public void Clear(){
         _negative.Clear();
         _positive.Clear();
      }

      public IEnumerator<T> GetEnumerator(){ return new TmsListEnumerator<T>(this); }
      IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

   }
}