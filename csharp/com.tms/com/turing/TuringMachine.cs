using System;

namespace com.tms.turing
{

   public class TuringMachine<TState, TSymbol>
   {
      private StatefulTable<TState, TSymbol> _table;
      private Tape<TSymbol> _tape;
      private Tape<TSymbol> _initialTape;

      public bool Terminated => _table.Terminated;
      public IReadonlyTape<TSymbol> Tape => _tape;

      public TuringMachine(StatefulTable<TState, TSymbol> table, Tape<TSymbol> tape)
      {
         _table = table;
         _tape = tape.Clone();
         _initialTape = tape.Clone();
      }

      public void Reset()
      {
         _table.Reset();
         _tape = _initialTape.Clone();
      }

      public void Step()
      {
         if (Terminated)
            throw new InvalidOperationException();
         var next = _table.Move(_tape.Current);
         _tape.Set(next.Symbol);
         _tape.Shift(next.Action);
      }

      public void Run()
      {
         while (!Terminated)
            Step();
      }

   }

}