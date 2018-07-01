#include <string>
#include <set>
#include <vector>

#include <iostream>
#include <fstream>
#include <sstream>

#include <exception>

#include "table.hpp"
#include "table_rw.hpp"
#include "utility.hpp"
#include "direction.hpp"

tms::io::table_rw::table_rw(std::string filename)
    : _filename(filename)
{
}

void tms::io::table_rw::load()
{
   std::ifstream input(_filename);
   input.exceptions(std::ifstream::failbit | std::ifstream::badbit);

   // Alphabet
   auto alphabet = tms::get_next_line(input);
   if (alphabet.empty())
      throw std::runtime_error("Cannot get the alphabet from the file.");
   tms::remove_spaces(alphabet);
   tms::populate_set(_alphabet, alphabet.begin(), alphabet.end());

   // states
   auto states = tms::get_next_line(input);
   if (alphabet.empty())
      throw std::runtime_error("Cannot get the states from the file.");
   std::istringstream states_stream(states);
   tms::populate_set(_states, std::istream_iterator<std::string>{states_stream},
                     std::istream_iterator<std::string>());

   // initial state
   _initial_state = tms::get_next_line(input);
   if (_initial_state.empty())
      throw std::runtime_error("Cannot get the initial state from the file.");

   // final states
   auto final_states = tms::get_next_line(input);
   if (final_states.empty())
      throw std::runtime_error("Cannot get the final states from the file.");
   std::istringstream final_states_stream(final_states);
   tms::populate_set(_final_states, std::istream_iterator<std::string>{final_states_stream},
                     std::istream_iterator<std::string>());

   // mappings
   std::vector<std::string> split_line{};
   for (auto entry = tms::get_next_line(input); !entry.empty(); entry = tms::get_next_line(input))
   {
      split_line.clear();
      std::istringstream entry_stream(entry);
      tms::populate_vector(split_line, std::istream_iterator<std::string>{entry_stream},
                           std::istream_iterator<std::string>());

      if (split_line.size() != 5)
         throw std::runtime_error("Wrong file format. One or more table entry is not of the correct size.");

      auto keySymbol = split_line[1][0];
      if (keySymbol == '#')
         keySymbol = char(0);

      auto entrySymbol = split_line[3][0];
      if (entrySymbol == '#')
         entrySymbol = char(0);

      tms::TableKey<> tableKey{keySymbol, split_line[0]};
      tms::TableEntry<> tableEntry{entrySymbol, split_line[2], tms::direction_from_string(split_line[4])};

      _map.insert(std::pair<tms::TableKey<>, tms::TableEntry<>>(tableKey, tableEntry));
   }

   input.close();
}

std::set<char> tms::io::table_rw::alphabet() { return _alphabet; }
std::set<std::string> tms::io::table_rw::states() { return _states; }
std::set<std::string> tms::io::table_rw::final_states() { return _final_states; }
std::string tms::io::table_rw::initial_state() { return _initial_state; }

void tms::io::table_rw::fill_table(Table<> &table)
{
   for (auto begin = _map.begin(); begin != _map.end(); ++begin)
   {
      auto pair = *begin;
      table.add(pair.first, pair.second);
   }
}
