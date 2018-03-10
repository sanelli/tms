'use strict';

const should = require('should');

const Table = require('../tms/statefulTable');
const Tape = require('../tms/tape');
const Machine = require('../tms/machine');

describe('Turing machine', () => {

   it('add 1', () => {
      const table = Table.fromFile(__dirname+'/../../test/xml/add_one_tm.xml');
      const tape = Tape.from('4|5|9', ' ');
      const machine = new Machine(table, tape);
      machine.run();

      tape.trim()
      tape.toString().should.equal('|4|6|0|');
   });

   it('palindrome', () => {
      const table = Table.fromFile(__dirname+'/../../test/xml/palindrome_tm.xml');
      const tape = Tape.from('01210', ' ');
      const machine = new Machine(table, tape);
      machine.run();

      tape.trim()
      tape.toString().should.equal('');
   });

   it('rot 13', () => {
      const table = Table.fromFile(__dirname+'/../../test/xml/rot13_tm.xml');
      const tape = Tape.from('abcdefgh$', ' ');
      const machine = new Machine(table, tape);
      machine.run();

      tape.trim()
      tape.toString().should.equal('|n|o|p|q|r|s|t|u|$|');
   });

   it('stirng length', () => {
      const table = Table.fromFile(__dirname+'/../../test/xml/string_length_tm.xml');
      const tape = Tape.from('abaabba', ' ');
      const machine = new Machine(table, tape);
      machine.run();

      tape.trim()
      tape.toString().replace(/[|]/g,'').trim().should.equal('7');
   });
})