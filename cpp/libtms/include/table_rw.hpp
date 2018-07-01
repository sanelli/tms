#pragma once

#include <string>
#include <set>
#include "table.hpp"

namespace tms
{
namespace io
{

class table_rw
{
   std::string _filename;
   std::set<char> _alphabet;
   std::set<std::string> _states;
   std::set<std::string> _final_states;
   std::string _initial_state;
   std::map<tms::TableKey<>, tms::TableEntry<>> _map;

 public:
   table_rw(std::string _filename);
   void load();

   std::set<char> alphabet();
   std::set<std::string> states();
   std::set<std::string> final_states();
   std::string initial_state();

   void fill_table(Table<> &table);
};

} // namespace io
} // namespace tms