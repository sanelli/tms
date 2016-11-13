class TmXmlContsants(object):
    CURRENT_VERSION = "1.0"
    
    TAG_ROOT = "turing-machine"
    ATTRIBUTE_VERSION = "version"

    TAG_SYMBOLS = "symbols"
    ATTRIBUTE_NULL_VALUE = "blank-symbol"
    
    TAG_STATES = "states"
    TAG_STATE = "state"
    ATTRIBUTE_START = "start"
    ATTRIBUTE_HALT = "halt"
    VALUE_YES = "yes"
    VALUE_NO = "no"

    TAG_TABLE = "transition-function"
    TAG_ENTRY = "mapping"
    TAG_KEY = "from"
    ATTRIBUTE_KEY_STATE = "current-state"
    ATTRIBUTE_KEY_SYMBOL = "current-symbol"
    TAG_VALUE = "to"
    ATTRIBUTE_VALUE_STATE = "next-state"
    ATTRIBUTE_VALUE_SYMBOL = "next-symbol"
    ATTRIBUTE_VALUE_DIRECTION = "movement"
    DIRECTION_LEFT = "left"
    DIRECTION_RIGHT = "right"
    DIRECTION_DONT_MOVE = "none"
