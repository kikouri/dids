using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using CommModule.Messages;



namespace CommModule
{
    public static class ObjectSerialization
    {
        public static byte[] SerializeGenericMessage(GenericMessage gm)
        {
            XmlSerializer genericMessageSerializer = new XmlSerializer(typeof(GenericMessage));
            TextWriter genericMessageWriter = new StringWriter();
            genericMessageSerializer.Serialize(genericMessageWriter, gm);
            String genericMessageString = genericMessageWriter.ToString();
            genericMessageWriter.Close();
            return Encoding.Unicode.GetBytes(genericMessageString);
        }

        public static GenericMessage SerializeObjectToGenericMessage(Object obj)
        {
            XmlSerializer messageSerializer = new XmlSerializer(obj.GetType());
            TextWriter messageWriter = new StringWriter();
            messageSerializer.Serialize(messageWriter, obj);
            String message = messageWriter.ToString();
            messageWriter.Close();

            GenericMessage genericMessage = new GenericMessage(obj.GetType().ToString(), message);

            return genericMessage;
        }

        /*
         *In order to support more message types is only needed to add them to the if clause
         *at the end of DeserializeObject Function with the same content, only making the
         *needed adaptations to the specific class.
         */
        public static Object DeserializeGenericMessage(GenericMessage genericMessage)
        {
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
            else if (genericMessage.ObjectType == "CommModule.Messages.CRLMessage")
            {
                XmlSerializer CRLMessageDeserializer = new XmlSerializer(typeof(CRLMessage));
                TextReader CRLMessageReader = new StringReader(genericMessage.ObjectString);
                CRLMessage CRLMessage = (CRLMessage)CRLMessageDeserializer.Deserialize(CRLMessageReader);
                return CRLMessage;
            }
            else if (genericMessage.ObjectType == "CommModule.Messages.CertificateGenerationRequest")
            {
                XmlSerializer certificateGenerationRequestDeserializer = new XmlSerializer(typeof(CertificateGenerationRequest));
                TextReader certificateGenerationRequestReader = new StringReader(genericMessage.ObjectString);
                CertificateGenerationRequest certificateGenerationRequest = (CertificateGenerationRequest)certificateGenerationRequestDeserializer.Deserialize(certificateGenerationRequestReader);
                return certificateGenerationRequest;
            }
            else if (genericMessage.ObjectType == "CommModule.Messages.Certificate")
            {
                XmlSerializer certificateDeserializer = new XmlSerializer(typeof(Certificate));
                TextReader certificateReader = new StringReader(genericMessage.ObjectString);
                Certificate certificate = (Certificate)certificateDeserializer.Deserialize(certificateReader);
                return certificate;
            }
            else if (genericMessage.ObjectType == "CommModule.Messages.SessionKeyMessage")
            {
                XmlSerializer sessionKeyMessageDeserailizer = new XmlSerializer(typeof(SessionKeyMessage));
                TextReader sessionKeyMessageReader = new StringReader(genericMessage.ObjectString);
                SessionKeyMessage skm = (SessionKeyMessage)sessionKeyMessageDeserailizer.Deserialize(sessionKeyMessageReader);
                return skm;
            }
            else if (genericMessage.ObjectType == "CommModule.Messages.SessionKeyMessageACK")
            {
                XmlSerializer sessionKeyMessageACKDeserailizer = new XmlSerializer(typeof(SessionKeyMessageACK));
                TextReader sessionKeyMessageACKReader = new StringReader(genericMessage.ObjectString);
                SessionKeyMessageACK skma = (SessionKeyMessageACK)sessionKeyMessageACKDeserailizer.Deserialize(sessionKeyMessageACKReader);
                return skma;
            }
            else if (genericMessage.ObjectType == "CommModule.Messages.CertificateRequestMessage")
            {
                XmlSerializer certificateRequestMessageDeserailizer = new XmlSerializer(typeof(CertificateRequestMessage));
                TextReader certificateRequestMessageReader = new StringReader(genericMessage.ObjectString);
                CertificateRequestMessage crm = (CertificateRequestMessage)certificateRequestMessageDeserailizer.Deserialize(certificateRequestMessageReader);
                return crm;
            }

            return genericMessage;
        }

        public static GenericMessage DeserializeObjectToGenericMessage(byte[] objBytes)
        {
            String genericMessageString = Encoding.Unicode.GetString(objBytes);
            XmlSerializer genericMessageDeserializer = new XmlSerializer(typeof(GenericMessage));
            TextReader genericMessageReader = new StringReader(genericMessageString);
            GenericMessage genericMessage = (GenericMessage)genericMessageDeserializer.Deserialize(genericMessageReader);

            return genericMessage;
        }
    }
}
