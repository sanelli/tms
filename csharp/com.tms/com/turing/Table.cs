using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace com.tms.turing {

   public class Table<TState, TSymbol> {

      public readonly IReadOnlyCollection<TState> _states;
      public readonly IReadOnlyCollection<TSymbol> _symbols;
      public readonly IDictionary<TableKey<TState,TSymbol>, TableValue<TState, TSymbol>> _table;

      public IEnumerable<TState> States => _states;
      public IEnumerable<TSymbol> Symbols => _symbols;

      public Table(IEnumerable<TState> states, IEnumerable<TSymbol> symbols) {
         _states = new ReadOnlyCollection<TState>(states.ToList());
         _symbols = new ReadOnlyCollection<TSymbol>(symbols.ToList());
         _table = new Dictionary<TableKey<TState,TSymbol>, TableValue<TState, TSymbol>>();
      }

      public TableValue<TState, TSymbol> this[TableKey<TState,TSymbol> key]{
         get { 
            if(States.Contains(key.State))
               throw new ArgumentException(nameof(key.State));
            if(Symbols.Contains(key.Symbol))
               throw new ArgumentException(nameof(key.Symbol));
            
            return _table[key]; }
         set { 
             if(States.Contains(key.State))
               throw new ArgumentException(nameof(key.State));
            if(Symbols.Contains(key.Symbol))
               throw new ArgumentException(nameof(key.Symbol));
            if(States.Contains(value.State))
               throw new ArgumentException(nameof(value.State));
            if(Symbols.Contains(value.Symbol))
               throw new ArgumentException(nameof(value.Symbol));
            _table[key] = value; 
         }
      }

      public void Clear(){ _table.Clear(); }
      public int Count() { return _table.Count(); }
      public IEnumerable<TableKey<TState,TSymbol>> Keys => _table.Keys;
   }
}