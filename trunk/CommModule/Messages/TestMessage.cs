using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommModule.Messages
{
    public class TestMessage
    {
        private String _idTest;
        private int _numTest;

        public TestMessage(String idTest, int numTest)
        {
            _idTest = idTest;
            _numTest = numTest;
        }

        public TestMessage()
        {
        }

        public String IdTest
        {
            get { return _idTest; }
            set { _idTest = value; }
        }

        public int NumTest
        {
            get { return _numTest; }
            set { _numTest = value; }
        }
    }
}
