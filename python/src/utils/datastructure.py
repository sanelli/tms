from copy import copy, deepcopy

class InfiniteList(object):
    def __init__(self, startIndex = 0, nullValue = None, increasingIndex = True, infinite = True):
        self._list = []
        self._startIndex = startIndex
        self._increasingIndex = increasingIndex
        self._infinite = infinite
        self._nullValue = nullValue

    def _extToInt(self, index):
        if self._increasingIndex:
            iIndex = index - self._startIndex
        else:
            iIndex = -index + self._startIndex
        if (iIndex < 0) or (not self._infinite and iIndex >= len(self)):
            raise IndexError("Invalid index {index}".format(index=index))
        return iIndex

    def _int2Ext(self, index):
        if self._increasingIndex:
            oIndex = index + self._startIndex
        else:
            oIndex = -index + self._startIndex
        return oIndex

    def _extend(self, size):
        while len(self) < size:
            self.append(self._nullValue)

    def __getitem__(self, index):
        iIndex = self._extToInt(index)
        if iIndex >= len(self):
            self._extend(iIndex + 1)
        return self._list[iIndex]

    def __setitem__(self, index, value):
        iIndex = self._extToInt(index)
        if iIndex >= len(self):
            self._extend(iIndex + 1)
        self._list[iIndex] = value

    def __delitem__(self, index):
        iIndex = self._extToInt(index)
        if iIndex >= len(self):
            self._extend(iIndex + 1)
        del(self._list[iIndex])

    def __len__(self):
        return len(self._list)
    
    def __iter__(self):
        for item in self._list:
            yield item

    def append(self, item):
        self._list.append(item)

    def strip(self):
        while len(self._list) > 0 and self._list[len(self._list)-1] == self._nullValue:
            self._list.pop()

    @property
    def minIndex(self):
        if self._increasingIndex:
            return self._int2Ext(0)
        else:
            return self._int2Ext(len(self)-1)

    @property
    def maxIndex(self):
        if not self._increasingIndex:
            return self._int2Ext(0)
        else:
            return self._int2Ext(len(self)-1)

    @property
    def nullValue(self):
        return self._nullValue

    def __copy__(self):
        other = InfiniteList()
        other._list = copy(self._list)
        other._nullValue = copy(self._nullValue)
        other._startIndex = self._startIndex
        other.__increasingIndex = self._increasingIndex
        other._infinite = self._infinite
        return other

    def __deepcopy__(self, memo=None):
        other = InfiniteList()
        other._list = deepcopy(self._list, memo)
        other._nullValue = deepcopy(self._nullValue, memo)
        other._startIndex = self._startIndex
        other.__increasingIndex = self._increasingIndex
        other._infinite = self._infinite
        return other

class InfiniteBidirectionalList(object):
    def __init__(self, nullValue = None):
        self._pos = InfiniteList( 0, nullValue, True)
        self._neg = InfiniteList(-1, nullValue, False)

    def _list(self, index):
        if index >= 0:
            return self._pos
        else:
            return self._neg

    def __setitem__(self, index, item):
        self._list(index)[index] = item

    def __getitem__(self, index):
        return self._list(index)[index]

    def __delitem__(self, index):
        del self._list(index)[index]

    def __len__(self):
        return len(self._neg) + len(self._pos)

    def __iter__(self):
        for item in self._neg:
            yield item
        for item in self._pos:
            yield item

    def strip(self):
        self._neg.strip()
        self._pos.strip()

    @property
    def minIndex(self):
        if len(self._neg) > 0:
            return self._neg.minIndex
        elif len(self._pos) > 0:
            return self._pos.maxIndex
        return 0

    @property
    def maxIndex(self):
        if len(self._pos) > 0:
            return self._pos.maxIndex
        elif len(self._neg) > 0:
            return self._neg.maxIndex
        else:
            return None

    @property
    def indices(self):
        return range(self.minIndex, self.maxIndex+1)

    def getZeroBasedIndex(self, index):
        return index - self.minIndex

    @property
    def nullValue(self):
        return self._pos.nullValue

    def __copy__(self):
        other = InfiniteBidirectionalList()
        other._pos = copy(self._pos)
        other._neg = copy(self._neg)
        return other

    def __deepcopy__(self, memo=None):
        other = InfiniteBidirectionalList()
        other._pos = deepcopy(self._pos, memo)
        other._neg = deepcopy(self._neg, memo)
        return other
