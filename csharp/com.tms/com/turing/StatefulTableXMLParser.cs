using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System;
using System.Xml.Linq;
using System.Linq;

namespace com.tms.turing
{
   public static class StatefulTableXMLParser
   {
      public static StatefulTable<TState, TSymbol> Load<TState, TSymbol>(XDocument document, TSymbol nullValue, 
        StateSerializer<TState> stateSerializer, SymbolSerializer<TSymbol> symbolSerializer)
      {
         // Get symbols
         var symbols = new List<TSymbol>();
         symbols.Add(nullValue);
         foreach (var element in document.Root.Elements())
         {
            if ("symbols".Equals(element.Name.LocalName))
            {
               for (int index = 0; index < element.Value.Length; index++)
               {
                  symbols.Add(symbolSerializer.FromString($"{element.Value[index]}"));
               }
               break;
            }
         }

         // Get status
         var states = new List<TState>();
         var initialState = default(TState);
         var finalStates = new List<TState>();
         foreach (var element in document.Root.Elements())
         {
            if ("states".Equals(element.Name.LocalName))
            {
               foreach (var stateTag in element.Elements())
               {
                  bool isStart = false;
                  bool isHalt = false;
                  if ("state".Equals(stateTag.Name.LocalName))
                  {
                     foreach (var attribute in stateTag.Attributes())
                     {
                        if ("start".Equals(attribute.Name.LocalName))
                        {
                           isStart = "yes".Equals(attribute.Value);
                        }
                        else if ("halt".Equals(attribute.Name.LocalName))
                        {
                           isHalt = "yes".Equals(attribute.Value);
                        }
                     }

                     var status = stateSerializer.FromString(stateTag.Value);
                     states.Add(status);

                     if (isStart)
                        initialState = status;

                     if (isHalt)
                        finalStates.Add(status);
                  }
               }
               break;
            }
         }

         // Create the table
         var table = new StatefulTable<TState, TSymbol>(states, symbols, initialState, finalStates);

         // Transition function
         foreach (var element in document.Root.Elements())
         {
            if ("transition-function".Equals(element.Name.LocalName))
            {
               foreach (var mappingTag in element.Elements())
               {
                  if ("mapping".Equals(mappingTag.Name.LocalName))
                  {
                     var currentState = string.Empty;
                     var currentSymbol = string.Empty;
                     var nextState = string.Empty;
                     var nextSymbol = string.Empty;
                     var movement = string.Empty;
                     foreach (var fromTag in mappingTag.Elements())
                     {
                        if ("from".Equals(fromTag.Name.LocalName))
                        {
                           foreach (var attribute in fromTag.Attributes())
                           {
                              if ("current-state".Equals(attribute.Name.LocalName))
                              {
                                 currentState = attribute.Value;
                              }
                              else if ("current-symbol".Equals(attribute.Name.LocalName))
                              {
                                 currentSymbol = attribute.Value;
                              }
                           }
                           break;
                        }
                     }
                     foreach (var toTag in mappingTag.Elements())
                     {
                        if ("to".Equals(toTag.Name.LocalName))
                        {
                           foreach (var attribute in toTag.Attributes())
                           {
                              if ("next-state".Equals(attribute.Name.LocalName))
                              {
                                 nextState = attribute.Value;
                              }
                              else if ("next-symbol".Equals(attribute.Name.LocalName))
                              {
                                 nextSymbol = attribute.Value;
                              }
                              else if ("movement".Equals(attribute.Name.LocalName))
                              {
                                 movement = attribute.Value;
                              }
                           }
                           break;
                        }
                     }

                     var key = new TableKey<TState, TSymbol>(stateSerializer.FromString(currentState),
                                symbolSerializer.FromString(currentSymbol));
                     var value = new TableValue<TState, TSymbol>(stateSerializer.FromString(nextState),
                                 symbolSerializer.FromString(nextSymbol), GetMoveActionFromString(movement));

                     table[key] = value;
                  }
               }
               break;
            }
         }
         return table;
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

      public static StatefulTable<TState, TSymbol> LoadFromString<TState, TSymbol>(string xml, TSymbol nullValue, 
            StateSerializer<TState> stateSerializer, SymbolSerializer<TSymbol> symbolSerializer)
      {
         byte[] byteArray = Encoding.UTF8.GetBytes(xml);
         MemoryStream stream = new MemoryStream(byteArray);
         var document = XDocument.Load(stream);
         return Load(document, nullValue, stateSerializer, symbolSerializer);
      }

      public static StatefulTable<TState, TSymbol> LoadFromFile<TState, TSymbol>(string filename, TSymbol nullValue, 
          StateSerializer<TState> stateSerializer, SymbolSerializer<TSymbol> symbolSerializer)
      {
         var document = XDocument.Load(filename);
         return Load(document, nullValue, stateSerializer, symbolSerializer);
      }

   }
}