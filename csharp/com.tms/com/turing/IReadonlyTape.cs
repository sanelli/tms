using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using com.tms.datastruct;

namespace com.tms.turing
{

   public interface IReadonlyTape<TSymbol>
   {
      IEnumerable<TSymbol> CurrentTape { get; }
      int Position { get; }
      TSymbol Current { get; }
      TSymbol Null { get; }
      string ToPlainString();
   }
}