'use strict';

const TableKey = require('./tableKey');
const TableEntry = require('./tableEntry');
const Direction = require('./direction');

class Table {
   constructor(states, symbols) {
      Object.defineProperty(this, 'states', { value: states, writable: false, enumerable: true });
      Object.defineProperty(this, 'symbols', { value: symbols, writable: false, enumerable: true });
      Object.defineProperty(this, '_table', { value: {}, writable: false, enumerable: true });
   }

   set(key, entry) {
      if(!(key instanceof TableKey)) throw new Error('Invalid key');
      if(!(entry instanceof TableEntry)) throw new Error('Invalid key');
      if(this.states.indexOf(key.state) < 0) throw new Error(`Invalid key state ${key.state}`);
      if(this.symbols.indexOf(key.symbol) < 0) throw new Error(`Invalid key symbol ${key.symbol}`);
      if(this.states.indexOf(entry.state) < 0) throw new Error(`Invalid entry state ${entry.state}`);
      if(this.symbols.indexOf(entry.symbol) < 0) throw new Error(`Invalid entry symbol ${entry.symbol}`);
      this._table[key] = entry;
   }

   get(key) {
      if(!(key instanceof TableKey)) throw new Error('Invalid key');
      if(this.states.indexOf(key.state) < 0) throw new Error(`Invalid key state ${key.state}`);
      if(this.symbols.indexOf(key.symbol) < 0) throw new Error(`Invalid key symbol ${key.symbol}`);
      return this._table[key];
   }

   toString(){
      let result = '';
      for(let key in this._table){
         result += `${key.toString()}${this._table[key].toString()}\n`
      }
      return result;
   }
}

module.exports = Table;