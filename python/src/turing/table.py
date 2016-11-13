import collections
import copy
import xml.etree.ElementTree as ET
from turing.direction import dir2str, str2dir
from utils.tmxml import TmXmlContsants

TableEntryKey = collections.namedtuple("TableEntryKey", ["state", "symbol"])
TableEntryKey.__str__ = lambda self : "{" + "state={state}, symbol={symbol}".format(state=self.state, symbol=self.symbol) + "}"

TableEntryValue = collections.namedtuple("TableEntryValue",["state", "symbol", "direction"])
TableEntryValue.__str__ = lambda self : "{" + "state={state}, symbol={symbol}, direction={direction}".format(state=self.state, symbol=self.symbol, direction=self.direction) + "}"

class Table(object):
    """
    Represent a table that for each (state, symbol) tuple associate
    a new tuple containing (state, symbol, direction)
    """
    def __init__(self, states, symbols):
        self._table = {}
        self._symbols = copy.deepcopy(symbols)
        self._states = copy.deepcopy(states)
        pass

    def __setitem__(self, key, value):
        if key.state not in self._states:
            raise ValueError("key.state does not contain a valid state")
        if key.symbol not in self._symbols:
            raise ValueError("key.symbol does not contain a valid symbol")
        if value.state not in self._states:
             raise ValueError("value.state does not contain a valid state")
        if value.symbol not in self._symbols:
             raise ValueError("value.symbol does not contain a valid symbol")
        self._table[key] = value

    def __getitem__(self, key):
        return self._table[key]
    
    def __len__(self):
        return len(self._table)

    def __iter__(self):
        self._table.iterkeys

    def __contains__(self, key):
        return key in self._table

    @property
    def allKeys(self):
        return (TableEntryKey(st,sy) for (st, sy) in itertools.product(self._states, self._symbols))
    
    @property
    def knownKeys(self):
        return set(self._table.keys)

    @property
    def missingKeys(self):
        return self.allKeys - self.knownKeys

    def __str__(self):
        kv = []
        for key in self.knownKeys:
            kv.append("{key}={value}".format(key=key, value=self[key]))
        return "{" + (", ".join(kv)) + "}"
    
class StatefulTable(Table):
    def __init__(self, states, symbols, initialState, terminalStates, nullValue = None):

        if initialState not in states:
            raise ValueError("Initial state is not a valid state")

        for st in terminalStates:
            if st not in states:
                raise ValueError("One of the final states is not a valide state")

        if len(terminalStates) <= 0:
            raise ValueError("At least one terminal state must be provided")

        super().__init__(self, states, symbols)
        self._initiState = initialState
        self._currentState = copy.deepcopy(initialState)
        self._terminalStates = copy.deepcopy(terminalStates)
        self._nullValue = nullValue 
    
    def __lshift__(self, symbol):
        key = TableEntryKey(self._currentState, symbol)
        value = self[key]
        self._currentState = value.state
        return value

    def reset(self):
        self._currentState = copy.deepcopy(self._initiState)

    @property
    def currentState(self):
        return self._currentState

    @property
    def isOnTerminalState(self):
        return self._currentState in self._terminalStates

    def __str__(self):
        result = "{"
        result += "initialState={}".format(self._initiState)
        result += ", currentState={}".format(self._currentState)
        result += ", terminalStates=[{}]".format(", ".join([ str(s) for s in self._terminalStates ]))
        result += ", table={}".format(super().__str__(self))
        result = "}"
        return result

    def toXML(self, serializeState = str, serializeSymbol = str):
        """
        See: 
            http://www.unidex.com/turing/tmml.htm 
            http://www.unidex.com/turing/tmml.dtd.htm
        """
        def writeSymbols(self, root):
            symbolsTag = ET.SubElement(root, TmXmlContsants.TAG_SYMBOLS)
            if self._nullValue != None:
                symbolsTag.set(TmXmlContsants.ATTRIBUTE_NULL_VALUE, self._nullValue)
            symbolsTag.text = "".join([serializeSymbol(symbol) for symbol in self._symbols])
        
        def writeStates(self, root):
            statesTag = ET.SubElement(root, TmXmlContsants.TAG_STATES)
            for state in self._states:
                stateTag = ET.SubElement(statesTag, TmXmlContsants.TAG_STATE)
                statesTag.text = serializeState(state)
                if state == self._initiState:
                    stateTag.set(TmXmlContsants.ATTRIBUTE_START, TmXmlContsants.VALUE_YES)
                if state in self._terminalStates:
                    stateTag.set(TmXmlContsants.ATTRIBUTE_HALT, TmXmlContsants.VALUE_YES)

        def writeTable(self, root):

            def writeTableEntry(self, tableTag, key, value):
                keyTag = ET.SubElement(tableTag, TmXmlContsants.TAG_KEY)
                keyTag.set(TmXmlContsants.ATTRIBUTE_KEY_STATE, serializeState(key.state))
                keyTag.set(TmXmlContsants.ATTRIBUTE_KEY_SYMBOL, serializeSymbol(key.symbol))

                valueTag = ET.SubElement(tableTag, TmXmlContsants.TAG_VALUE)
                valueTag.set(TmXmlContsants.ATTRIBUTE_VALUE_STATE, serializeState(value.state))
                valueTag.set(TmXmlContsants.ATTRIBUTE_VALUE_SYMBOL, serializeSymbol(value.symbol))
                valueTag.set(TmXmlContsants.ATTRIBUTE_VALUE_DIRECTION, dir2str(value.direction))

            tableTag = ET.SubElement(root, TmXmlContsants.TAG_TABLE)
            for key in self.knownKeys:
                writeTableEntry(self, tableTag, key, self[value])                

        root = ET.Element(TmXmlContsants.TAG_ROOT)
        writeSymbols(self, root)
        writeStates(self, root)
        writeTable(self, root)

    @staticmethod
    def fromXMLString(string, deserializeState = str, deserializeSymbol = str):
        root = ET.fromstring(country_data_as_string)
        return StatefulTable._parseRoot(root, deserializeState, deserializeSymbol)

    @staticmethod
    def fromXMLFile(filename, deserializeState = str, deserializeSymbol = str):
        tree = ET.parse(filename)
        root = tree.getroot()
        return StatefulTable._parseRoot(root, deserializeState, deserializeSymbol)
    
    @staticmethod
    def _parseRoot(root, deserializeState = str, deserializeSymbol = str):
        # Symbols
        symbolsTag = root.find(TmXmlContsants.TAG_SYMBOLS)
        nullValue = ' '
        if TmXmlContsants.ATTRIBUTE_NULL_VALUE in symbolsTag.attrib:
            nullValue = symbolsTag.get(TmXmlContsants.ATTRIBUTE_NULL_VALUE)
        symbols = [ deserializeSymbol(symbol) for symbol in symbolsTag.text ]

        # States
        initState = None
        terminalStates = []
        states = []
        statesTag = root.find(TmXmlContsants.TAG_STATES)
        for stateTag in statesTag.findall(TmXmlContsants.TAG_STATE):
            state = deserializeState(stateTag.text)

            if state in states:
                raise ValueError("Malformed XML: duplicated state")
            else:
                states.append(state)

            if TmXmlContsants.ATTRIBUTE_START in state.attrib:
                if initState != None:
                    raise ValueError("Malformed XML: duplicated initial state")
                else:
                    initState = state

            if TmXmlContsants.ATTRIBUTE_HALT in state.attrib:
                terminalStates.append(state)

        if initState == None:
            raise ValueError("Malformed XML: no initial state has been identified")

        table = StatefulTable(states, symbols, initState, terminalStates, nullValue)

        tagTable = root.find(TmXmlContsants.TAG_TABLE)
        for tableEntry in tagTable.findall(TmXmlContsants.TAG_ENTRY)
            keyTag = tagEntry.find(TmXmlContsants.TAG_KEY)
            key = TableEntryKey(state = keyTag.get(TmXmlContsants.ATTRIBUTE_KEY_STATE), 
                                symbol = keyTag.get(TmXmlContsants.ATTRIBUTE_KEY_SYMBOL))
            
            valueTag = tagEntry.find(TmXmlContsants.TAG_VALUE)
            value = TableEntryValue(state = keyTag.get(TmXmlContsants.ATTRIBUTE_VALUE_STATE),
                                    symbol = keyTag.get(TmXmlContsants.ATTRIBUTE_VALUE_SYMBOL),
                                    direction = str2dir(keyTag.get(TmXmlContsants.ATTRIBUTE_VALUE_DIRECTION)))

            table[key] = value

        return table