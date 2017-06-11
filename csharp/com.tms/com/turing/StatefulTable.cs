using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace com.tms.turing
{

   public class StatefulTable<TState, TSymbol> : Table<TState, TSymbol>
   {

      private readonly TState _initialState;
      private TState _currentState;
      private readonly IReadOnlyCollection<TState> _finalStates;

      public TState InitialState => _initialState;
      public TState CurrentState => _currentState;
      public IEnumerable<TState> FinalStates => _finalStates;

      public bool Terminated => _finalStates.Contains(_currentState);

      public StatefulTable(IEnumerable<TState> states, IEnumerable<TSymbol> symbols,
         TState initialState, IEnumerable<TState> finalStates) : base(states, symbols)
      {

         if (!states.Contains(initialState))
            throw new ArgumentException(nameof(initialState));

         if (finalStates.Any(f => !states.Contains(f)))
            throw new ArgumentException(nameof(finalStates));

         _initialState = initialState;
         _currentState = _initialState;
         _finalStates = new ReadOnlyCollection<TState>(finalStates.ToList());
      }

      public TableValue<TState, TSymbol> Move(TSymbol symbol)
      {
         var next = this[new TableKey<TState, TSymbol>(_currentState, symbol)];
         _currentState = next.State;
         return next;
      }

      public void Reset(){
         _currentState = _initialState;
      }

   }

}