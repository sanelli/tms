'use strict';

var convert = require('xml-js');
var fs = require('fs');

const Table = require('./table');
const TableKey = require('./tableKey');
const TableEntry = require('./tableEntry');
const Direction = require('./direction');

function str2direction(str){
   if(str === 'left') return Direction.LEFT;
   if(str === 'right') return Direction.RIGHT;
   return Direction.NONE;
}

class StatefulTable extends Table {
   constructor(states, symbols, initialState, terminalStates) {
      super(states, symbols);
      const table = new Table(states, symbols);
      Object.defineProperty(this, 'initialState', { value: initialState, writable: false, enumerable: true });
      Object.defineProperty(this, 'terminalStates', { value: terminalStates, writable: false, enumerable: true });
      Object.defineProperty(this, '_currentState', { value: initialState, writable: true, enumerable: false });
      Object.defineProperty(this, 'currentState', { get: () => _currentState, enumerable: true });
      Object.defineProperty(this, 'onTerminalState', { get: () => this.terminalStates.indexOf(this._currentState) >=0 , enumerable: true });
   }

   step(symbol) {
      const key = new TableKey(this._currentState, symbol);
      const value = this.get(key);
      this._currentState = value.state
      return value
   }

   static fromFile(filename, encoding = 'utf8') {
      let xml = fs.readFileSync(filename, encoding).toString();
      var json = convert.xml2json(xml, { compact: true, spaces: 4 });
      var obj = JSON.parse(json);
      const turingMachine = obj['turing-machine'];
      const symbols = turingMachine.symbols._text.split('');
      const jStates = turingMachine['states'].state;
      const states = [];
      const terminalStates = [];
      let initialState = undefined;
      for (let stateIndex = 0; stateIndex < jStates.length; stateIndex++) {
         const jState = jStates[stateIndex];
         const stateName = jState._text;
         if (jState._attributes) {
            if (jState._attributes.start === 'yes')
               initialState = stateName;
            if (jState._attributes.halt === 'yes')
               terminalStates.push(stateName);
         }
         states.push(stateName);
      }
      const nullSymbol = ' ';
      symbols.push(nullSymbol);

      const table = new StatefulTable(states, symbols, initialState, terminalStates);

      const jMappings = turingMachine['transition-function'].mapping;
      for (let mappingIndex = 0; mappingIndex < jMappings.length; mappingIndex++) {
         const mapping = jMappings[mappingIndex];
         const key = new TableKey(mapping.from._attributes['current-state'], mapping.from._attributes['current-symbol']);
         const entry = new TableEntry(mapping.to._attributes['next-state'], mapping.to._attributes['next-symbol'], str2direction(mapping.to._attributes.movement));
         table.set(key, entry);
      }

      return table;
   }
}

module.exports = StatefulTable;