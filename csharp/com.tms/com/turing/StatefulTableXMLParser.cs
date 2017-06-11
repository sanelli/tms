using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace com.tms.turing
{
   public static class StatefulTableXMLParser
   {

      private static StatefulTable<TState, TSymbol> Load<TState, TSymbol>(XmlReader reader,
          StateSerializer<TState> stateSerializer, SymbolSerializer<TSymbol> symbolSerializer)
      {
         // Read symbols
         reader.ReadToFollowing("symbols");
         var symbolStr = reader.ReadElementContentAsString();
         var symbols = new List<TSymbol>();
         for (int index = 0; index < symbolStr.Length; index++)
         {
            symbols.Add(symbolSerializer.FromString($"{symbolStr[index]}"));
         }

         // Read statuses
         var states = new List<TState>();
         var initialState = default(TState);
         var finalStates = new List<TState>();
         reader.ReadToFollowing("states");
         while (reader.ReadToFollowing("state"))
         {
            var isStart = reader.HasAttributes && "yes".Equals(ReadAttribute(reader, "start"));
            var isHalt = reader.HasAttributes && "yes".Equals(ReadAttribute(reader, "halt"));
            reader.MoveToElement();
            var statusString = reader.ReadElementContentAsString();

            var status = stateSerializer.FromString(statusString);
            states.Add(status);

            if (isStart)
               initialState = status;

            if (isHalt)
               finalStates.Add(status);
         }

         var table = new StatefulTable<TState, TSymbol>(states, symbols, initialState, finalStates);

         reader.ReadToFollowing("ransition-function");
         while (reader.ReadToFollowing("mapping"))
         {
            reader.ReadToFollowing("from");
            var currentState = ReadAttribute(reader, "current-state");
            var currentSymbol = ReadAttribute(reader, "current-symbol");
            reader.ReadToFollowing("to");
            var nextState = ReadAttribute(reader, "next-state");
            var nextSymbol = ReadAttribute(reader, "next-symbol");
            var movement = ReadAttribute(reader, "movement");

            var key = new TableKey<TState, TSymbol>(stateSerializer.FromString(currentState),
                        symbolSerializer.FromString(currentSymbol));
            var value = new TableValue<TState, TSymbol>(stateSerializer.FromString(nextState),
                        symbolSerializer.FromString(nextSymbol), GetMoveActionFromString(movement));

            table[key] = value;
         }

         return table;
      }

      private static string ReadAttribute(XmlReader reader, string attributeName)
      {
         if (reader.MoveToAttribute("start"))
            return reader.Value;
         return string.Empty;
      }

      private static MoveAction GetMoveActionFromString(string movement)
      {
         switch (movement)
         {
            case "right":
               return MoveAction.RIGHT;
            case "left":
               return MoveAction.LEFT;
            default:
               return MoveAction.NONE;
         }
      }

      public static StatefulTable<TState, TSymbol> LoadFromString<TState, TSymbol>(string xml,
            StateSerializer<TState> stateSerializer, SymbolSerializer<TSymbol> symbolSerializer)
      {
         using (var reader = XmlReader.Create(new StringReader(xml)))
         {
            return Load(reader, stateSerializer, symbolSerializer);
         }
      }

      public static StatefulTable<TState, TSymbol> LoadFromFile<TState, TSymbol>(string filename,
          StateSerializer<TState> stateSerializer, SymbolSerializer<TSymbol> symbolSerializer)
      {
         return LoadFromFile(string.Join("\n", File.ReadAllLines(filename)), stateSerializer, symbolSerializer);
      }

   }
}