using System;
using Xunit;

using com.tms.datastruct;
using com.tms.turing;

namespace com.tms.test
{
    public class TestLoadXML
    {

private const string ADD_ONE_TM = @"<?xml version='1.0'?>
<turing-machine version='0.1'>

<!-- This Turing machine (TM) adds one to a whole number.
     For example, if the initial tape is '199', then
     the final tape will be '200'.

     The Turing machine assumes that the first (i.e., 
     leftmost) symbol on the tape is the leftmost digit 
     in the number.

     If the input tape is completely blank, then the final
     tape will be '1'. So, if the input tape is ' 43', 
     then the Turing machine will assume that tape does not 
     contain a number.
 
     This Turing Machine Markup Language (TMML) document complies
     with the DTD for TMML, which is available at 
     http://www.unidex.com/turing/tmml.dtd.

     This Turing machine can be executed by an XSLT stylesheet that is
     available at http://www.unidex.com/turing/utm.xsl. This stylesheet
     is a Universal Turing Machine.

     The following Instant Saxon command will execute the Turing machine
     described by this TMML document using the utm.xsl stylesheet:

        saxon add_one_tm.xml utm.xsl tape=199

     This TMML document is available at 
     http://www.unidex.com/turing/add_one_tm.xml.

     Developed by Bob Lyons of Unidex, Inc.

     Please email any comments about this TMML document to 
     boblyons@unidex.com.
-->

<!-- COPYRIGHT NOTICE and LICENSE.

     Copyright (c) 2001 Unidex, Inc. All rights reserved.

     Unidex, Inc. grants you permission to copy, modify, distribute,
     and/or use the TMML document provided that you agree to the
     following conditions:

     1. You must include this COPYRIGHT NOTICE and LICENSE
        in all copies or substantial portions of the TMML document.

     2. The TMML document is licensed to the user on an 'AS IS' basis.
        Unidex Inc. makes no warranties, either express or implied,
        with respect to the TMML document including but not limited to any
        warranty of merchantability or fitness for any particular
        purpose. Unidex Inc. does not warrant that the operation
        of the TMML document will be uninterrupted or error-free,
        or that defects in the TMML document will be corrected.
        You the user are solely responsible for determining the 
        appropriateness of the TMML document for your use and accept
        full responsibility for all risks associated with its use. 
        Unidex Inc. is not and will not be liable for any
        direct, indirect, special, incidental or other damages 
        of any kind (including loss of profits or interruption of business)
        however caused even if Unidex Inc. has been advised of the 
        possibility of such damages.
-->


    <!-- The symbols are '0' through '9'.
    -->
    <symbols>0123456789</symbols>

    <states>
	<!-- In the go_right state, the Turing machine moves the
	     tape head to the blank symbol to the right of the 
	     number.
	-->
        <state start='yes'>go_right</state>

	<!-- In the increment state, the Turing machine keeps moving
	     left and changing 9's to 0's until it finds a digit
	     other than '9' or a blank symbol. When it finds a digit
             other than '9' (or a blank symbol, which it treats as
             the digit '0'), it increments the digit (e.g., replaces
	     '3' with '4').
	-->
        <state>increment</state>

        <state halt='yes'>stop</state>
    </states>

    <!-- The transition function for the TM.
    -->
    <transition-function>
        <mapping>
	    <!-- Move to the right of the number. -->
            <from current-state='go_right' current-symbol='0'/>
            <to next-state='go_right' next-symbol='0' movement='right'/>
        </mapping>
        <mapping>
	    <!-- Move to the right of the number. -->
            <from current-state='go_right' current-symbol='1'/>
            <to next-state='go_right' next-symbol='1' movement='right'/>
        </mapping>
        <mapping>
	    <!-- Move to the right of the number. -->
            <from current-state='go_right' current-symbol='2'/>
            <to next-state='go_right' next-symbol='2' movement='right'/>
        </mapping>
        <mapping>
	    <!-- Move to the right of the number. -->
            <from current-state='go_right' current-symbol='3'/>
            <to next-state='go_right' next-symbol='3' movement='right'/>
        </mapping>
        <mapping>
	    <!-- Move to the right of the number. -->
            <from current-state='go_right' current-symbol='4'/>
            <to next-state='go_right' next-symbol='4' movement='right'/>
        </mapping>
        <mapping>
	    <!-- Move to the right of the number. -->
            <from current-state='go_right' current-symbol='5'/>
            <to next-state='go_right' next-symbol='5' movement='right'/>
        </mapping>
        <mapping>
	    <!-- Move to the right of the number. -->
            <from current-state='go_right' current-symbol='6'/>
            <to next-state='go_right' next-symbol='6' movement='right'/>
        </mapping>
        <mapping>
	    <!-- Move to the right of the number. -->
            <from current-state='go_right' current-symbol='7'/>
            <to next-state='go_right' next-symbol='7' movement='right'/>
        </mapping>
        <mapping>
	    <!-- Move to the right of the number. -->
            <from current-state='go_right' current-symbol='8'/>
            <to next-state='go_right' next-symbol='8' movement='right'/>
        </mapping>
        <mapping>
	    <!-- Move to the right of the number. -->
            <from current-state='go_right' current-symbol='9'/>
            <to next-state='go_right' next-symbol='9' movement='right'/>
        </mapping>
        <mapping>
	    <!-- Found the blank that follows the number. 
		 Change to the increment state and start moving left. -->
            <from current-state='go_right' current-symbol=' '/>
            <to next-state='increment' next-symbol=' ' movement='left'/>
        </mapping>
        <mapping>
	    <!-- Change the 9 to a 0 and move left. -->
            <from current-state='increment' current-symbol='9'/>
            <to next-state='increment' next-symbol='0' movement='left'/>
        </mapping>
        <mapping>
	    <!-- Change the 0 to a 1. We're done. -->
            <from current-state='increment' current-symbol='0'/>
            <to next-state='stop' next-symbol='1' movement='left'/>
        </mapping>
        <mapping>
	    <!-- Change the blank to a 1. We're done. -->
            <from current-state='increment' current-symbol=' '/>
            <to next-state='stop' next-symbol='1' movement='none'/>
        </mapping>
        <mapping>
	    <!-- Change the 1 to a 2. We're done. -->
            <from current-state='increment' current-symbol='1'/>
            <to next-state='stop' next-symbol='2' movement='left'/>
        </mapping>
        <mapping>
	    <!-- Change the 2 to a 3. We're done. -->
            <from current-state='increment' current-symbol='2'/>
            <to next-state='stop' next-symbol='3' movement='left'/>
        </mapping>
        <mapping>
	    <!-- Change the 3 to a 4. We're done. -->
            <from current-state='increment' current-symbol='3'/>
            <to next-state='stop' next-symbol='4' movement='left'/>
        </mapping>
        <mapping>
	    <!-- Change the 4 to a 5. We're done. -->
            <from current-state='increment' current-symbol='4'/>
            <to next-state='stop' next-symbol='5' movement='left'/>
        </mapping>
        <mapping>
	    <!-- Change the 5 to a 6. We're done. -->
            <from current-state='increment' current-symbol='5'/>
            <to next-state='stop' next-symbol='6' movement='left'/>
        </mapping>
        <mapping>
	    <!-- Change the 6 to a 7. We're done. -->
            <from current-state='increment' current-symbol='6'/>
            <to next-state='stop' next-symbol='7' movement='left'/>
        </mapping>
        <mapping>
	    <!-- Change the 7 to a 8. We're done. -->
            <from current-state='increment' current-symbol='7'/>
            <to next-state='stop' next-symbol='8' movement='left'/>
        </mapping>
        <mapping>
	    <!-- Change the 8 to a 9. We're done. -->
            <from current-state='increment' current-symbol='8'/>
            <to next-state='stop' next-symbol='9' movement='left'/>
        </mapping>
    </transition-function>
</turing-machine>";

        [Fact]
        public void test_add_one_from_string()
        {
            const char nullValue = ' ';
            var table = StatefulTableXMLParser.LoadFromString<string, char>(ADD_ONE_TM, nullValue,
                new StringStateSerializer(), new CharSymbolSerializer());

            var tape = new Tape<char>(new CharSymbolSerializer(), nullValue);
            tape.FillFromString("|4|5|9|");
            var machine = new TuringMachine<string,char>(table, tape);
            machine.Run();
            Assert.Equal("|4|6|0| |", machine.Tape.ToString());
        }

        [Fact]
        public void test_add_one_from_string_2()
        {
            const char nullValue = ' ';
            var table = StatefulTableXMLParser.LoadFromString<string, char>(ADD_ONE_TM, nullValue,
                new StringStateSerializer(), new CharSymbolSerializer());

            var tape = new Tape<char>(new CharSymbolSerializer(), nullValue);
            tape.FillFromString("|4|5|9|");
            var machine = new TuringMachine<string,char>(table, tape);
            machine.Run();
            Assert.Equal("460", machine.Tape.ToPlainString().Trim());
        }

        [Fact]
        public void test_add_one_from_file()
        {
            const char nullValue = ' ';
            var table = StatefulTableXMLParser.LoadFromFile<string, char>("../../../../../test/xml/add_one_tm.xml", nullValue,
                new StringStateSerializer(), new CharSymbolSerializer());

            var tape = new Tape<char>(new CharSymbolSerializer(), nullValue);
            tape.FillFromString("|4|5|9|");
            var machine = new TuringMachine<string,char>(table, tape);
            machine.Run();
            Assert.Equal("460", machine.Tape.ToPlainString().Trim());
         }

        [Fact]
        public void test_palindrome()
        {
            const char nullValue = ' ';
            var table = StatefulTableXMLParser.LoadFromFile<string, char>("../../../../../test/xml/palindrome_tm.xml", nullValue,
                new StringStateSerializer(), new CharSymbolSerializer());

            var tape = new Tape<char>(new CharSymbolSerializer(), nullValue);
            tape.FillFromString("|0|1|2|1|0|");
            var machine = new TuringMachine<string,char>(table, tape);
            machine.Run();
            Assert.Equal(string.Empty, machine.Tape.ToPlainString().Trim());
        }

        [Fact]
        public void test_palindrome_2()
        {
            const char nullValue = ' ';
            var table = StatefulTableXMLParser.LoadFromFile<string, char>("../../../../../test/xml/palindrome_tm.xml", nullValue,
                new StringStateSerializer(), new CharSymbolSerializer());

            var tape = new Tape<char>(new CharSymbolSerializer(), nullValue);
            tape.FillFromString("|0|1|2|2|0|");
            var machine = new TuringMachine<string,char>(table, tape);
            machine.Run();
            Assert.Equal("22", machine.Tape.ToPlainString().Trim());
        }

        [Fact]
        public void test_rot13()
        {
            const char nullValue = ' ';
            var table = StatefulTableXMLParser.LoadFromFile<string, char>("../../../../../test/xml/rot13_tm.xml", nullValue,
                new StringStateSerializer(), new CharSymbolSerializer());

            var tape = new Tape<char>(new CharSymbolSerializer(), nullValue);
            tape.FillFromString("|a|b|c|d|e|f|g|h|$|");
            var machine = new TuringMachine<string,char>(table, tape);
            machine.Run();
            Assert.Equal("nopqrstu$", machine.Tape.ToPlainString().Trim());
        }

        [Fact]
        public void test_string_length()
        {
            const char nullValue = ' ';
            var table = StatefulTableXMLParser.LoadFromFile<string, char>("../../../../../test/xml/string_length_tm.xml", nullValue,
                new StringStateSerializer(), new CharSymbolSerializer());

            var tape = new Tape<char>(new CharSymbolSerializer(), nullValue);
            tape.FillFromString("|a|b|a|a|b|b|a|");
            var machine = new TuringMachine<string,char>(table, tape);
            machine.Run();
            Assert.Equal("7", machine.Tape.ToPlainString().Trim());
        }

        [Fact]
        public void test_string_length_2()
        {
            const char nullValue = ' ';
            var table = StatefulTableXMLParser.LoadFromFile<string, char>("../../../../../test/xml/string_length_tm.xml", nullValue,
                new StringStateSerializer(), new CharSymbolSerializer());

            var tape = new Tape<char>(new CharSymbolSerializer(), nullValue);
            tape.FillFromString("|a|b|a|a|b|b|a|b|a|b|b|");
            var machine = new TuringMachine<string,char>(table, tape);
            machine.Run();
            Assert.Equal("11", machine.Tape.ToPlainString().Trim());
        }
    }
}
