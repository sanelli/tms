'use strict';

const TableKey = require('./tableKey');

class TableEntry extends TableKey {
   constructor(state, symbol, direction) {
      super(state, symbol);
      Object.defineProperty(this, 'direction', { value: direction, writable: false, enumerable: true });
   }

   equals(o) {
      super.equals(o) && this.direction === o.direction;
   }

   toString() {
      return `<${this.state},${this.symbol},${this.direction.key}>`;
   }
}

module.exports = TableEntry;