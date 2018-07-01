#pragma once

#include <functional>
#include <string>

#include "direction.hpp"
#include "infiniteBidirectionalList.hpp"

namespace tms
{

template <typename T = char, typename TBiList = InfiniteBidirectionalList<char>>
class Tape
{
 private:
   TBiList _tape;
   int _cursor;

 public:
   Tape()
       : _tape(), _cursor(0) {}
   Tape(const TBiList &tape, int initialCursor)
       : _tape(tape), _cursor(initialCursor) {}
   Tape(const Tape &other)
       : _tape(other._tape), _cursor(other._cursor) {}
   Tape(Tape &&other)
       : _tape(std::move(other._tape)), _cursor(std::move(other._cursor)) {}

   Tape &operator=(Tape &other)
   {
      _tape = other._tape;
      _cursor = other._cursor;
   }

   T current()
   {
      return _tape.at(_cursor);
   }

   int cursor() const
   {
      return _cursor;
   }

   void set(T &item)
   {
      _tape.at(_cursor) = item;
   }

   void shift(Direction direction)
   {
      switch (direction)
      {
      case Direction::LEFT:
         --_cursor;
         break;
      case Direction::RIGHT:
         ++_cursor;
         break;
      case Direction::NONE:
         break;
      }
   }

   std::string to_string(const std::function<std::string(T)> translate = {})
   {
      return _tape.to_string(translate);
   }

   template <typename TTape, typename TBegin, typename TEnd, typename TConverter>
   friend void init_tape(TTape &tape, TBegin start, TEnd stop, const TConverter &converter);
};

template <typename TTape, typename TBegin, typename TEnd, typename TConverter>
void init_tape(TTape &tape, TBegin start, TEnd stop, const TConverter &converter)
{
   tape._tape.clear();
   int index = 0;
   for (; start != stop; ++start, ++index)
      tape._tape.at(index) = converter(*start);
}

} // namespace tms