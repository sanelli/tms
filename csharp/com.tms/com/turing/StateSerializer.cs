using System;

namespace com.tms.turing{

   public interface StateSerializer<TState> {

      string ToString(TState item);
      TState FromString(string s);
   }

   public class StringStateSerializer : StateSerializer<string>
   {
      public string FromString(string s)
      {
         return s;
      }

      public string ToString(string item)
      {
         return item;
      }
   }
}