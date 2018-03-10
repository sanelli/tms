'use strict';

const Tape = require('./bidirectionalInfiniteList');
const Table = require('./statefulTable');

class TuringMachine {
   constructor(table, tape) {
      Object.defineProperty(this, 'table', { value: table, writable: false, enumerable: true });
      Object.defineProperty(this, 'tape', { value: tape, writable: false, enumerable: true });
      Object.defineProperty(this, 'terminated', { get: () => this.table.onTerminalState, enumerable: true });
   }

   step() {
      if (this.terminated)
         throw Error("Cannot step because turing machine is already on a terminal status.")
      let symbol = this.tape.get();
      let action = this.table.step(symbol);
      this.tape.set(action.symbol)
      this.tape.shift(action.direction)
   }

   run() {
      while (!this.terminated)
         this.step()
   }

}

module.exports = TuringMachine;