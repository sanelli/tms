from enum import Enum

class Direction(Enum):
    """
    Enumeration representing the various directions
    the cursor can move on a tape
    """
    none = 0
    left = 1
    right = 2

def dir2str(direction):
    if Direction.left == direction:
        return "left"
    elif Direction.right == direction:
        return "right"
    elif Direction.none == direction:
        return "none"
    else:
        raise ValueError("invalid direction")

def str2dir(direction):
    if dir2str(Direction.left).casefold() == direction.casefold():
        return Direction.left
    elif dir2str(Direction.right).casefold() == direction.casefold():
        return Direction.right
    elif dir2str(Direction.none).casefold() == direction.casefold():
        return Direction.none
    else:
        raise ValueError("invalid direction")
