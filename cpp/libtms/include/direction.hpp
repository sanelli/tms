#pragma once

#include <string>
#include <exception>

namespace tms
{
enum class Direction
{
   NONE = 0,
   LEFT = -1,
   RIGHT = +1
};

Direction direction_from_string(std::string direction);
std::string to_string(Direction direction);

} // namespace tms