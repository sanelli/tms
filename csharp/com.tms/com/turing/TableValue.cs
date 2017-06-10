namespace com.tms.turing { 

   public class TableValue<TState, TSymbol> : TableKey<TState, TSymbol> {

      protected MoveAction _action;

      public TableValue(TState state, TSymbol symbol, MoveAction action) : base(state,symbol) { 
         _action = action;
      } 

      public MoveAction Action => _action;

      public override bool Equals(object o){
         if(o == null) return false;
         if(!(o is TableValue<TState, TSymbol>)) return false;
         return Equals(o as TableValue<TState, TSymbol>);
      }

       private bool Equals(TableValue<TState, TSymbol> other){
         return base.Equals(other) && _action.Equals(other._action);
      }

      public override int GetHashCode(){ 
         return base.GetHashCode() * 317 + Action.GetHashCode(); 
      }

   }

}