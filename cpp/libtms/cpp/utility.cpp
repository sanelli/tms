#include <string>
#include <iostream>
#include <fstream>
#include <algorithm>

#include "utility.hpp"

// trim code from https://stackoverflow.com/questions/216823/whats-the-best-way-to-trim-stdstring

// trim from start (in place)
void tms::ltrim(std::string &s)
{
   s.erase(s.begin(), std::find_if(s.begin(), s.end(), [](int ch) {
              return !std::isspace(ch);
           }));
}

// trim from end (in place)
void tms::rtrim(std::string &s)
{
   s.erase(std::find_if(s.rbegin(), s.rend(), [](int ch) {
              return !std::isspace(ch);
           })
               .base(),
           s.end());
}

// trim from both ends (in place)
void tms::trim(std::string &s)
{
   ltrim(s);
   rtrim(s);
}

std::string tms::get_next_line(std::ifstream &input)
{
   std::string line;
   do
   {
      try
      {
         if (input.eof())
            return "";
         getline(input, line);
         tms::trim(line);
      }
      catch (...)
      {
         if (input.eof())
            return "";
         throw;
      }
   } while (line == "");
   return line;
}

void tms::remove_spaces(std::string &str)
{
   // Erease remove idiom: https://en.wikipedia.org/wiki/Erase%E2%80%93remove_idiom
   str.erase(std::remove_if(str.begin(),
                            str.end(),
                            [](unsigned char x) { return std::isspace(x); }),
             str.end());
}
