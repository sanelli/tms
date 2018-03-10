'use strict';

class TableKey {
   constructor(state, symbol) {
      Object.defineProperty(this, 'state', { value: state, writable: false, enumerable: true });
      Object.defineProperty(this, 'symbol', { value: symbol, writable: false, enumerable: true });
   }

   equals(o) {
      if (o === undefined) return false;
      if (o === null) return false;
      return this.state === o.state && this.symbol === o.symbol;
   }

   toString() {
      return `<${this.state},${this.symbol}>`;
   }
}

module.exports = TableKey;