#include "tms.hpp"
#include "config.hpp"
#include <string>
#include <sstream>

#include "tape.hpp"
#include "infiniteBidirectionalList.hpp"
#include "infiniteList.hpp"
#include "table.hpp"

std::string tms::version()
{
   return std::string("v") + std::to_string(MAJOR_VERSION) + "." + std::to_string(MINOR_VERSION);
}

std::string tms::convert_null_char_into_space(char c)
{
   if (c == 0)
      return " ";
   return std::string({c});
}
