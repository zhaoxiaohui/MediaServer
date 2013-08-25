using System;

namespace ValueWebSocket.Protocol
{
    public class MessageHeader
    {
        public Char FIN;
        public Char RSV1;
        public Char RSV2;
        public Char RSV3;
        public OperType Opcode;
        public Char MASK;
        public UInt64 Payloadlen;
        public Byte[] Maskey;
        public Int32 PayloadDataStartIndex;
    }
}
