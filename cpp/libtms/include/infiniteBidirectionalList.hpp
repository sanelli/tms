#pragma once

#include <vector>
#include <string>
#include <functional>
#include <utility>
#include <exception>

#include "infiniteList.hpp"

namespace tms
{
template <typename T, typename TContainter = std::vector<T>>
class InfiniteBidirectionalList
{
 private:
   InfiniteList<T, TContainter> _positive;
   InfiniteList<T, TContainter> _negative;

 public:
   InfiniteBidirectionalList()
       : _positive(true, 0),
         _negative(false, -1) {}
   InfiniteBidirectionalList(const InfiniteBidirectionalList &other)
       : _positive(other._positive),
         _negative(other._negative) {}
   InfiniteBidirectionalList(InfiniteBidirectionalList &&other)
       : _positive(std::move(other._positive)),
         _negative(std::move(other._negative)) {}

   InfiniteBidirectionalList &operator=(const InfiniteBidirectionalList &other)
   {
      _positive = other._positive;
      _negative = other._negative;
      return *this;
   }

   T &at(int index)
   {
      return index >= 0
                 ? _positive.at(index)
                 : _negative.at(index);
   }

   void clear(){
      _positive.clear();
      _negative.clear();
   }

   void trim()
   {
      _negative.trim();
      _positive.trim();
   }

   int minIndex()
   {
      if (size())
         throw std::runtime_error("Cannot get index for an empty list.");
      return _negative.size() > 0
                 ? -_negative.size()
                 : 0;
   }

   int maxIndex()
   {
      if (size() == 0)
         throw std::runtime_error("Cannot get index for an empty list.");
      return _positive.size() > 0
                 ? _positive.size() - 1
                 : -1;
   }

   int size()
   {
      return _negative.size() + _positive.size();
   }

   std::string to_string(const std::function<std::string(T)> translate = {})
   {
      std::stringstream stream;
      stream << _negative.to_string(translate);
      stream << _positive.to_string(translate);
      return stream.str();
   }
};

} // namespace tms