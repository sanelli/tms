from enum import Enum
from utils.datastructure import InfiniteBidirectionalList
from turing.direction import Direction
from copy import copy, deepcopy

class Tape(object):
    def __init__(self, nullValue = None):
        self._tape = InfiniteBidirectionalList(nullValue)
        self._cursor = 0

    def set(self, item):
        self._tape[self._cursor] = item

    def get(self):
        return self._tape[self._cursor]

    def shift(self, direction):
        if direction == Direction.LEFT:
            self.shiftLeft()
        elif direction == Direction.RIGHT:
            self.shiftRight()
        elif direction == Direction.DONT_MOVE:
            pass
        else:
            raise ValueError("Unknown direction")

    def shiftRight(self, shift = 1):
        if shift < 1:
            raise ValueError("Invalid shift value")
        self._cursor += shift

    def shiftLeft(self, shift = 1):
        if shift < 1:
            raise 
        self._cursor -= shift

    def reset(self):
        self._cursor = 0

    def strip(self):
        self._tape.strip()

    def __str__(self):
        upper = "".join(self._tape)
        cursorIndex = self._tape.getZeroBasedIndex(self._cursor)
        lower = " " * (cursorIndex - 1) + "^"
        return "\n".join([upper, lower])

    def tapeAsStr(self, serializeSymbol = str):
        return  "|{}|".format("|".join([serializeSymbol(symbol) for symbol self._tape]))

    def __copy__(self):
        other = Tape()
        other._cursor = self._cursor
        other._tape = copy(self._tape)
        return other

    def __deepcopy__(self, memo=None):
        other = Tape()
        other._cursor = self._cursor
        other._tape = deepcopy(self._tape, memo)
        return other

    @staticmethod
    def fromString(string, nullValue = ' '):
        return Tape.fromIterable(string, nullValue)
    
    @staticmethod
    def fromIterable(it, nullValue = None):
        tape = Tape(nullValue)
        for i in range(len(it)):
            tape._tape[i] = it[i]
        return tape