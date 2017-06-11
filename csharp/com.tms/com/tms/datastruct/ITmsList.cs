using System.Collections.Generic;

namespace com.tms.datastruct {

   public interface ITmsList<T> : IEnumerable<T> {
      void Append(T Item);
      void RemoveAt(int index);
      T this[int index]{get; set; }
      void Strip();
      int Count();
      int MinIndex { get; }
      int MaxIndex { get; }
      void Clear();
   }

}