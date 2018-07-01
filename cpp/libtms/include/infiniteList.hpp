#pragma once

#include <vector>
#include <string>
#include <utility>
#include <sstream>
#include <exception>
#include <algorithm>
#include <functional>

namespace tms
{

struct IndexConverter
{
   static int int2ext(int index, int startIndex, bool increasing);
   static int ext2int(int index, int startIndex, bool increasing);
};

template <typename T, typename TContainer = std::vector<T>>
class InfiniteList
{
 private:
   int _startIndex;
   bool _increasing;
   TContainer _vector;

 public:
   InfiniteList(bool increasing, int startIndex) : _increasing(true), _startIndex(startIndex) {}
   InfiniteList(bool increasing, int startIndex, const TContainer &initial)
       : _increasing(true),
         _startIndex(0),
         _vector(initial) {}
   InfiniteList(const InfiniteList &other)
       : _increasing(other._increasing),
         _startIndex(other._startIndex),
         _vector(other._vector) {}
   InfiniteList(InfiniteList &&other)
       : _increasing(std::move(other._increasing)),
         _startIndex(std::move(other._startIndex)),
         _vector(std::move(other._vector)) {}

   InfiniteList &operator=(const InfiniteList &other)
   {
      _vector = other._vector;
      _increasing = other._increasing;
      _startIndex = other._startIndex;
      return *this;
   }

   T &at(int extIndex)
   {
      auto inIndex = IndexConverter::ext2int(extIndex, _startIndex, _increasing);

      if (inIndex < 0)
         throw std::runtime_error("Unexpected index.");

      expand(inIndex);
      return _vector[inIndex];
   }

   int size() const
   {
      return _vector.size();
   }

   void trim()
   {
      while (_vector.back() == T(0) && _vector.size() > 0)
         _vector.pop_back();
   }

   void clear()
   {
      _vector.clear();
   }

   std::string to_string(const std::function<std::string(T)> translate = {})
   {
      std::stringstream stream;
      for (auto item : _vector)
         if (translate)
            stream << translate(item);
         else
            stream << item;
      auto result = stream.str();
      if (!_increasing)
         reverse(result.begin(), result.end());
      return result;
   }

 private:
   void expand(int size)
   {
      while (_vector.size() <= size)
         _vector.push_back(T(0));
   }
};

} // namespace tms