'use strict';

const Enum = require('enum');

const Direction = new Enum(['NONE', 'LEFT', 'RIGHT'], {freez: true})

module.exports = Direction;