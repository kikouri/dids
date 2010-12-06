using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using CommModule.Messages;


namespace CommModule
{
    class ObjectSerialization
    {
        public static byte[] SerializeObject(Object obj){
            XmlSerializer messageSerializer = new XmlSerializer(obj.GetType());
            TextWriter messageWriter = new StringWriter();
            messageSerializer.Serialize(messageWriter, obj);
            String message = messageWriter.ToString();
            messageWriter.Close();

            GenericMessage genericMessage = new GenericMessage(obj.GetType().ToString(), message);
            XmlSerializer genericMessageSerializer = new XmlSerializer(typeof(GenericMessage));
            TextWriter genericMessageWriter = new StringWriter();
            genericMessageSerializer.Serialize(genericMessageWriter, genericMessage);
            String genericMessageString = genericMessageWriter.ToString();
            genericMessageWriter.Close();

            return Encoding.Unicode.GetBytes(genericMessageString);

        }

        /*
         *In order to support more message types is only needed to add them to the if clause
         *at the end of DeserializeObject Function with the same content, only making the
         *needed adaptations to the specific class.
         * 
         */
        public static Object DeserializeObject(byte[] objBytes){
            String genericMessageString = Encoding.Unicode.GetString(objBytes);
            XmlSerializer genericMessageDeserializer = new XmlSerializer(typeof(GenericMessage));
            TextReader genericMessageReader = new StringReader(genericMessageString);
            GenericMessage genericMessage = (GenericMessage)genericMessageDeserializer.Deserialize(genericMessageReader);

            if (genericMessage.ObjectType == "CommModule.Messages.TestMessage")
            {
                XmlSerializer testMessageDeserializer = new XmlSerializer(typeof(TestMessage));
                TextReader testMessageReader = new StringReader(genericMessage.ObjectString);
                TestMessage testMessage = (TestMessage)testMessageDeserializer.Deserialize(testMessageReader);
                return testMessage;
            }
            else if (genericMessage.ObjectType == "CommModule.Messages.TrackerRequestMessage")
            {
                XmlSerializer trackerRequestMessageDeserializer = new XmlSerializer(typeof(TrackerRequestMessage));
                TextReader trackerRequestMessageReader = new StringReader(genericMessage.ObjectString);
                TrackerRequestMessage trackerRequestMessage = (TrackerRequestMessage)trackerRequestMessageDeserializer.Deserialize(trackerRequestMessageReader);
                return trackerRequestMessage;
            }
            else if (genericMessage.ObjectType == "CommModule.Messages.TrackerAnswerMessage")
            {
                XmlSerializer trackerAnswerMessageDeserializer = new XmlSerializer(typeof(TrackerAnswerMessage));
                TextReader trackerAnswerMessageReader = new StringReader(genericMessage.ObjectString);
                TrackerAnswerMessage trackerAnswerMessage = (TrackerAnswerMessage)trackerAnswerMessageDeserializer.Deserialize(trackerAnswerMessageReader);
                return trackerAnswerMessage;
            }

            return genericMessage;
        }

        public static string EncodeTo64(string encode)
        {
            byte[] encodeBytes = ASCIIEncoding.ASCII.GetBytes(encode);
            return System.Convert.ToBase64String(encodeBytes);
        }

        public static string DecodeFrom64(string base64encoded)
        {
            byte[] encodedBytes = System.Convert.FromBase64String(base64encoded);
            return ASCIIEncoding.ASCII.GetString(encodedBytes);
        }
    }
}
