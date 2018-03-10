'use strict'

const Table = require('./tms/statefulTable');
const Tape = require('./tms/tape');
const Machine = require('./tms/machine');

const table = Table.fromFile(process.argv[2]);
const tape = Tape.from(process.argv[3], ' ');
const machine = new Machine(table, tape);
machine.run();

tape.trim()
console.log(tape.toString());
