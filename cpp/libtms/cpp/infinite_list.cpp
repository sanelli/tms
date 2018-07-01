#include "infiniteList.hpp"
#include <iostream>

int tms::IndexConverter::int2ext(int index, int startIndex, bool increasing)
{
   return increasing
              ? index + startIndex
              : -index + startIndex;
}

int tms::IndexConverter::ext2int(int index, int startIndex, bool increasing)
{
   return increasing
              ? index - startIndex
              : -index + startIndex;
}
