import unittest
import sys
import os.path

sys.path.append("../src")

from turing.machine import TuringMachine
from turing.tape import Tape
from turing.table import StatefulTable

class TestTM(unittest.TestCase):

    def getFile(self, filename):
        return os.path.abspath(filename)

    def test_rot13(self):
        tape = Tape.fromString("abcdefgh$")
        filename = self.getFile("../../test/xml/rot13_tm.xml")
        table = StatefulTable.fromXMLFile(filename)
        machine = TuringMachine(table, tape)
        machine.run()
        self.assertEqual("nopqrstu$", machine.tape.asPlainString().strip())

    def test_add_one(self):
        tape = Tape.fromString("459")
        filename = self.getFile("../../test/xml/add_one_tm.xml")
        table = StatefulTable.fromXMLFile(filename)
        machine = TuringMachine(table, tape)
        machine.run()
        self.assertEqual("460", machine.tape.asPlainString().strip())

    def test_palindrome(self):
        tape = Tape.fromString("01210")
        filename = self.getFile("../../test/xml/palindrome_tm.xml")
        table = StatefulTable.fromXMLFile(filename)
        machine = TuringMachine(table, tape)
        machine.run()
        self.assertEqual("", machine.tape.asPlainString().strip())

    def test_string_length(self):
        tape = Tape.fromString("abaabba")
        filename = self.getFile("../../test/xml/string_length_tm.xml")
        table = StatefulTable.fromXMLFile(filename)
        machine = TuringMachine(table, tape)
        machine.run()
        self.assertEqual("7", machine.tape.asPlainString().strip())

if __name__ == "__main__":
    unittest.main()