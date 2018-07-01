#pragma once

#include <string>
#include <set>
#include <algorithm>
#include <exception>
#include <functional>

#include <iostream>

#include "table.hpp"
#include "tape.hpp"

namespace tms
{
std::string version();

std::string convert_null_char_into_space(char c);

template <typename T = char, typename TState = std::string,
          typename TTape = Tape<>, typename TTable = Table<>,
          typename TTableKey = TableKey<>,
          typename TTableEntry = TableEntry<>>
class machine
{
 private:
   TTape _tape;
   TTable _table;
   TState _initialState;
   std::set<TState> _finalStates;
   TState _currentState;

 public:
   machine(const TTape &tape, const TTable &table, const TState &initState, const std::set<TState> &finalStates)
       : _tape(tape), _table(table), _initialState(initState), _finalStates(finalStates), _currentState(initState) {}

   bool done()
   {
      return std::find(_finalStates.begin(), _finalStates.end(), _currentState) != _finalStates.end();
   }

   void next()
   {
      if (done())
         throw std::runtime_error("Cannot execute next(). Machine alredy on terminal state.");
      auto symbol = _tape.current();
      auto key = TTableKey(symbol, _currentState);
      auto entry = _table.get(key);
      auto entrySymbol = entry.symbol();
      _tape.set(entrySymbol);
      _tape.shift(entry.direction());
      _currentState = entry.state();
   }

   void run()
   {
      while (!done())
         next();
   }

   std::string get_tape_to_string(const std::function<std::string(T)> translate = {})
   {
      return _tape.to_string(translate);
   }
};

} // namespace tms