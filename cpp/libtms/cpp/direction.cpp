#include <string>
#include <exception>

#include "direction.hpp"

tms::Direction tms::direction_from_string(std::string direction)
{
   if (direction == "left")
      return tms::Direction::LEFT;
   if (direction == "right")
      return tms::Direction::RIGHT;
   if (direction == "none")
      return tms::Direction::NONE;
   throw std::runtime_error("Unexpected direction");
}

std::string tms::to_string(tms::Direction direction)
{
   switch (direction)
   {
   case tms::Direction::LEFT:
      return "left";
   case tms::Direction::RIGHT:
      return "right";
   case tms::Direction::NONE:
      return "none";
   }
   throw std::runtime_error("Unexpected direction");
}
