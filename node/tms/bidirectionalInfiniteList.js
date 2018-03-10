'use strict';

const InfiniteList = require('./infiniteList');

function selectList(biList, index) {
   return index >= 0 ? biList._positive : biList._negative;
}

class BidirectionalInfiniteList {
   constructor(nullValue) {
      Object.defineProperty(this, 'nullValue', { value: nullValue, writable: false, enumerable: true });
      const positive = new InfiniteList(0, nullValue, true);
      const negative = new InfiniteList(-1, nullValue, false);
      Object.defineProperty(this, '_positive', { value: positive, writable: false, enumerable: false });
      Object.defineProperty(this, '_negative', { value: negative, writable: false, enumerable: false });
      Object.defineProperty(this, 'length', { enumerable: true, get: () => this._positive.length + this._negative.length });
   }

   set(index, item) {
      selectList(this, index).set(index, item);
   }

   get(index) {
     return selectList(this, index).get(index);
   }

   trim() {
      this._positive.trim();
      this._negative.trim();
   }

   toString(separator = '|') {
      if (this.length > 0)
         return `${this._negative.length > 0 ? separator : ''}${this._negative.toString(separator)}${separator}${this._positive.toString(separator)}${this._positive.length > 0 ? separator : ''}`;
      return '';
   }
}


module.exports = BidirectionalInfiniteList;