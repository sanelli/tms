'use strict';

const BidirectioninfiniteList = require('./bidirectionalInfiniteList');
const Direction = require('./direction');

class Tape {
   constructor(initialValue, nullValue) {
      const tape = new BidirectioninfiniteList(nullValue);
      Object.defineProperty(this, '_tape', { value: tape, writable: false, enumerable: false });
      Object.defineProperty(this, '_cursor', { value: 0, writable: true, enumerable: false });

      for (let index = 0; index < initialValue.length; index++)
         this._tape.set(index, initialValue[index]);
   }

   set(item) {
      this._tape.set(this._cursor, item);
   }

   get() {
      return this._tape.get(this._cursor);
   }

   shift(direction) {
      if (direction === Direction.LEFT)
         this._cursor = this._cursor - 1;
      else if (direction === Direction.RIGHT)
         this._cursor = this._cursor + 1;
      else if (direction === Direction.NONE) { }
      else throw new Error('Unexpetcted direciton')
   }

   trim() {
      this._tape.trim();
   }

   toString(separator = '|') {
      return this._tape.toString(separator);
   }

   static from(str, nullValue, separator = '|') {
      const values = str.split("|").join('').split('');
      return new Tape(values, nullValue);
   }
}

module.exports = Tape;