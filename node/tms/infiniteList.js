'use strict';

function esternal2internal(list, eIndex) {
   let iIndex = undefined;
   if (list.increasingIndex)
      iIndex = eIndex - list.startIndex
   else
      iIndex = -eIndex + list.startIndex
   if (iIndex < 0)
      throw new Error(`Invalid external index ${eIndex}`);
   return iIndex
}

function internal2external(list, eIndex) {
   let eIndex = undefined;
   if (self.increasingIndex)
      eIndex = index + list.startIndex
   else
      eIndex = -index + list.startIndex
   return oIndex
}

function extend(list, size) {
   while (list.length < size)
      list._array.push(list.nullValue)
}

class InfiniteList {
   constructor(startIndex, nullValue, increasingIndex = true) {
      Object.defineProperty(this, 'startIndex', { value: startIndex, writable: false, enumerable: true });
      Object.defineProperty(this, 'nullValue', { value: nullValue, writable: false, enumerable: true });
      Object.defineProperty(this, 'increasingIndex', { value: increasingIndex, writable: false, enumerable: true });
      Object.defineProperty(this, '_array', { value: [], writable: true, enumerable: false });
      Object.defineProperty(this, 'length', { enumerable: true, get: () => this._array.length });
   }

   get(index) {
      const iIndex = esternal2internal(this, index)
      if (iIndex >= this.length)
         extend(this, iIndex + 1)
      return this._array[iIndex];
   }

   set(index, value) {
      const iIndex = esternal2internal(this, index);
      if (iIndex >= this.length)
         extend(this, iIndex + 1)
      this._array[iIndex] = value;
   }

   trim() {
      while (this.length > 0 && this._array[this.length - 1] === this.nullValue)
         this._array.pop();
   }

   toString(separator = '|') {
      return this._array.join(separator);
   }
}

module.exports = InfiniteList;
