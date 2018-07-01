#pragma once

#include <exception>
#include <algorithm>
#include <set>
#include <vector>
#include <sstream>

namespace tms
{
template <typename T>
struct null_converter
{
   T operator()(T input) const { return input; }
};

template <typename T, typename TValidator>
struct null_converter_with_validator
{
 private:
   TValidator _validator;

 public:
   null_converter_with_validator(TValidator validator)
       : _validator(validator) {}

   T operator()(T input) const
   {
      if (_validator(input))
         return input;
      else
         throw std::runtime_error("Invalid input");
   }
};

template <typename T, typename TContainer>
class contains_validator
{
   TContainer &_container;

 public:
   contains_validator(TContainer &container)
       : _container(container) {}

   bool operator()(T input) const
   {
      return std::find(_container.begin(), _container.end(), input) != _container.end();
   }
};

void ltrim(std::string &s);
void rtrim(std::string &s);
void trim(std::string &s);

std::string get_next_line(std::ifstream &input);
void remove_spaces(std::string &str);

template <typename T, typename TBegin, typename TEnd>
void populate_set(std::set<T> &set, TBegin begin, TEnd end)
{
   for (; begin != end; ++begin)
      set.insert(*begin);
}

template <typename T, typename TBegin, typename TEnd>
void populate_vector(std::vector<T> &vector, TBegin begin, TEnd end)
{
   for (; begin != end; ++begin)
      vector.push_back(*begin);
}

template <typename TBegin, typename TEnd>
std::string to_string(TBegin begin, TEnd end, std::string split = " ")
{
   std::stringstream stream;

   for(;begin != end; ++begin)
      stream << *begin << split;

   return stream.str();
}

} // namespace tms