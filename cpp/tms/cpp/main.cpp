#include <iostream>
#include <string>
#include <cstdlib>
#include <set>
#include <exception>

#include "tms.hpp"
#include "tape.hpp"
#include "table.hpp"
#include "table_rw.hpp"
#include "utility.hpp"
#include "direction.hpp"

int main(int argc, char *argv[])
{
   std::cout << ">> TMS - by Stefano Anelli <stefano.anellil@gmail.com>." << std::endl;
   std::cout << ">> Using libtms " << tms::version() << "." << std::endl;

   if (argc < 3)
   {
      std::cerr << "!! Usage: " << argv[0] << " TABLE_FILE_NAME INITIAL_TAPE" << std::endl;
      return EXIT_FAILURE;
   }

   try
   {
      tms::io::table_rw table_rw(argv[1]);
      table_rw.load();
      auto alphabet = table_rw.alphabet();
      auto states = table_rw.states();
      auto initial_state = table_rw.initial_state();
      auto final_states = table_rw.final_states();

      tms::Table<> table(alphabet, states);
      table_rw.fill_table(table);

      std::cout << ">> Alphabet: " << tms::to_string(alphabet.begin(), alphabet.end()) << std::endl;
      std::cout << ">> States: " << tms::to_string(states.begin(), states.end()) << std::endl;
      std::cout << ">> Initial state: " << initial_state << std::endl;
      std::cout << ">> Final states: " << tms::to_string(final_states.begin(), final_states.end()) << std::endl;

      std::cout << ">> Table: " << std::endl;
      for (auto it = table.begin(); it != table.end(); ++it)
      {
         std::cout << ">>\t"
                   << "[" << (*it).first.state() << ", " << tms::convert_null_char_into_space((*it).first.symbol()) << "]"
                   << " -> "
                   << "[" << (*it).second.state() << ", " << tms::convert_null_char_into_space((*it).second.symbol()) << ", " << tms::to_string((*it).second.direction()) << "]"
                   << std::endl;
      }

      tms::Tape<> tape;
      std::string initial_tape(argv[2]);
      tms::contains_validator<char, std::set<char>> contains_validator(alphabet);
      tms::null_converter_with_validator<char, tms::contains_validator<char, std::set<char>>> converter(contains_validator);
      tms::init_tape(tape, initial_tape.begin(), initial_tape.end(), converter);

      std::cout << ">> Initial tape: " << tape.to_string(tms::convert_null_char_into_space) << std::endl;

      tms::machine<> machine(tape, table, initial_state, final_states);
      machine.run();

      std::cout << ">> Final tape: " <<  machine.get_tape_to_string(tms::convert_null_char_into_space) << std::endl;
     ;

      return EXIT_SUCCESS;
   }
   catch (std::runtime_error runtime_error)
   {
      std::cout << "!! An error occured: " << runtime_error.what() << std::endl;
      return EXIT_FAILURE;
   }
   catch (...)
   {
      std::cout << "!! An unknown error occured. No further details are available." << std::endl;
      return EXIT_FAILURE;
   }
}
