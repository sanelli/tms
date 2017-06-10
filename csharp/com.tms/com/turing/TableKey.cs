namespace com.tms.turing { 

   public class TableKey<TState, TSymbol> {

      private TState _state;
      private TSymbol _symbol;

      public TableKey(TState state, TSymbol symbol) { 
         _state = state;
         _symbol = symbol;
      } 

      public TState State => _state;
      public TSymbol Symbol => _symbol;

      public override bool Equals(object o){
         if(o == null) return false;
         if(!(o is TableKey<TState, TSymbol>)) return false;
         return Equals(o as TableKey<TState, TSymbol>);
      }

       private bool Equals(TableKey<TState, TSymbol> other){
         return State.Equals(other.State) && Symbol.Equals(other.Symbol);
      }

      public override int GetHashCode(){ 
         return State.GetHashCode() * 317 + Symbol.GetHashCode(); 
      }

   }

}