using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommModule.Messages
{
    [Serializable()]
    public class AttackSolutionMessage
    {        
        private String _healerAddress;
        private String _healerId;
        private String _attackId;
        private String _attackDesc;
        private String _fileName;
        private String _fileContent;

        public String HealerAddress
        {
            get { return _healerAddress; }
            set { _healerAddress = value; }
        }

        public String HealerId
        {
            get { return _healerId; }
            set { _healerId = value; }
        }

        public String AttackId
        {
            get { return _attackId; }
            set { _attackId = value; }
        }

        public String AttackDesc
        {
            get { return _attackDesc; }
            set { _attackDesc = value; }
        }

        public String FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public String FileContent
        {
            get { return _fileContent; }
            set { _fileContent = value; }
        }

        public AttackSolutionMessage() { }

    }
}
