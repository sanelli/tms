from turing.tape import Tape
from turing.direction import Direction
from turing.table import StatefulTable
from copy import deepcopy

class TuringMachine(object):
    def __init__(self, table, tape):
        self._tape  = deepcopy(tape)
        self._initialTape = tape
        self._table = table

    def reset(self):
        self._table.reset()
        self._tape = deepcopy(self._initialTape)

    def step(self):
        if self.terminated:
            raise RuntimeError("Cannot step because turing machine is already on a terminal status.")
        symbol = self._tape.get()
        action = self._table << symbol
        self._tape.set(action.symbol)
        self._tape.shift(action.direction)

    def run(self):
        while not self.terminated:
            self.step()

    @property
    def tape(self):
        return self._tape

    @property
    def terminated(self):
        return self._table.isOnTerminalState
