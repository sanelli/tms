import unittest
import sys

sys.path.append("../src")

from turing.machine import TuringMachine
from turing.tape import Tape


class TestTM(unittest.TestCase):
    def test_sum(self):
        tape = Tape.fromString("qwerty")
        print(tape)

if __name__ == "__main__":
    unittest.main()