#pragma once

#include <string>
#include <utility>
#include <map>
#include <set>
#include <algorithm>
#include <exception>

#include "direction.hpp"

namespace tms
{

template <typename T = char, typename TState = std::string>
class TableKey
{
 private:
   T _symbol;
   TState _state;

 public:
   TableKey() : TableKey(T(0), TState("")) {}
   TableKey(T &symbol, std::string &state)
       : _symbol(symbol),
         _state(state) {}
   TableKey(const TableKey &other)
       : _symbol(other._symbol),
         _state(other._state) {}
   TableKey(TableKey &&other)
       : _symbol(std::move(other._symbol)),
         _state(std::move(other._state)) {}

   TableKey &operator=(const TableKey &other)
   {
      _symbol = other._symbol;
      _state = other._state;
      return *this;
   }

   bool operator==(const TableKey &other) const
   {
      return _symbol == other._symbol && _state == other._state;
   }

   inline bool operator<(const TableKey &other) const
   {
      return (_symbol != other._symbol)
                 ? _symbol < other._symbol
                 : _state < other._state;
   }

   T symbol() const { return _symbol; }
   TState state() const { return _state; }
};

template <typename T = char, typename TState = std::string>
class TableEntry
{
 private:
   T _symbol;
   TState _state;
   Direction _direction;

 public:
   TableEntry() : _symbol(T(0)),
         _state(TState("")),
         _direction(Direction::NONE){}
   TableEntry(T &symbol, TState &state, Direction direction)
       : _symbol(symbol),
         _state(state),
         _direction(direction) {}
   TableEntry(const TableEntry &other)
       : _symbol(other._symbol),
         _state(other._state),
         _direction(other._direction) {}
   TableEntry(TableEntry &&other)
       : _symbol(std::move(other._symbol)),
         _state(std::move(other._state)),
         _direction(std::move(other._direction)) {}

   TableEntry &operator=(const TableEntry &other)
   {
      _symbol = other._symbol;
      _state = other._state;
      _direction = other._direction;
      return *this;
   }

   inline bool operator==(const TableEntry &other) const
   {
      return _symbol == other._symbol && _state == other._state && _direction == other._direction;
   }

   T symbol() const { return _symbol; }
   TState state() const { return _state; }
   Direction direction() const { return _direction; }
};

template <typename T = char, typename TState = std::string,
          typename TKey = TableKey<>, typename TEntry = TableEntry<>>
class Table
{
 private:
   std::map<TKey, TEntry> _map;
   std::set<T> _alphabet;
   std::set<TState> _states;

 public:
   Table(std::set<T> &alphabet, std::set<TState> &states)
       : _alphabet(alphabet), _states(states) {}

   void add(const TKey &key, const TEntry &entry)
   {
      if (!hasSymbol(key.symbol()))
         throw std::runtime_error("Invalid key symbol");
      if (!hasState(key.state()))
         throw std::runtime_error("Invalid key state");
      if (!hasSymbol(entry.symbol()))
         throw std::runtime_error("Invalid entry symbol");
      if (!hasState(entry.state()))
         throw std::runtime_error("Invalid entry state");
      _map.insert(std::pair<TKey, TEntry>(key, entry));
   }

   TEntry get(TKey &key)
   {
      return _map[key];
   }

   auto begin()
   {
      return _map.begin();
   }

   auto end()
   {
      return _map.end();
   }

 private:
   bool hasSymbol(const T &letter) const
   {
      if (letter == T(0))
         return true;
      return std::find(_alphabet.begin(), _alphabet.end(), letter) != _alphabet.end();
   }

   bool hasState(const TState &state) const
   {
      return std::find(_states.begin(), _states.end(), state) != _states.end();
   }
};

} // namespace tms