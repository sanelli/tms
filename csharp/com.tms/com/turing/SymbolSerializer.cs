using System;

namespace com.tms.turing{

   public interface SymbolSerializer<TSymbol> {

      string ToString(TSymbol item);
      char Separator { get; }
      TSymbol FromString(string s);
   }

   public class CharSymbolSerializer : SymbolSerializer<char> { 
      public string ToString(char c)
      { 
         if(c == Separator)
            throw new ArgumentException(nameof(c));
         return $"{c}"; 
      }
      public char Separator => '|';
      public char FromString(string s) { return s[0]; }
   }

   public class StringSymbolSerializer : SymbolSerializer<string> { 
      public string ToString(string c)
      { 
         if(c.Contains(Separator.ToString()))
            throw new ArgumentException(nameof(c));
         return c; 
      }
      public char Separator => '|';
      public string FromString(string s) { return s; }
   }

   public class IntSymbolSerializer : SymbolSerializer<int> { 
      public string ToString(int c){ return c.ToString(); }
      public char Separator => '|';
      public int FromString(string s) { return int.Parse(s); }
   }

   public class LongSymbolSerializer : SymbolSerializer<long> { 
      public string ToString(long c){ return c.ToString(); }
      public char Separator => '|';
      public long FromString(string s) { return long.Parse(s); }
   }

}