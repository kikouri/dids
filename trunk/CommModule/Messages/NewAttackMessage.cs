using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommModule.Messages
{
    [Serializable()]
    public class NewAttackMessage
    {
        private String _sourceIdsAddress;
        private int _sourceIdsPort;
        private String _sourceIdsId;
        private String _attackId;
        private String _attackName;
        private String _attackDescription;

        public String SourceIdsAddress
        {
            get { return _sourceIdsAddress; }
            set { _sourceIdsAddress = value; }
        }

        public int SourceIdsPort
        {
            get { return _sourceIdsPort; }
            set { _sourceIdsPort = value; }
        }

        public String SourceIdsId
        {
            get { return _sourceIdsId; }
            set { _sourceIdsId = value; }
        }

        public String AttackName
        {
            get { return _attackName; }
            set { _attackName = value; }
        }

        public String AttackId
        {
            get { return _attackId; }
            set { _attackId = value; }
        }

        public String AttackDescription
        {
            get { return _attackDescription; }
            set { _attackDescription = value; }
        }

        public NewAttackMessage() { }
    }
}
