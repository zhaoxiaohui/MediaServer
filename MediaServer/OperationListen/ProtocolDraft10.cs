/*
  >>>------ Copyright (c) 2012 zformular ----> 
 |                                            |
 |            Author: zformular               |
 |        E-mail: zformular@163.com           |
 |             Date: 10.18.2012               |
 |                                            |
 ╰==========================================╯
 
*/

using System;
using System.Text;
using System.Security.Cryptography;
using ValueWebSocket.Infrastructure;

namespace ValueWebSocket.Protocol
{
    public class ProtocolDraft10 : IProtocol
    {
        private const String WebSocketKeyPattern = @"Sec\-WebSocket\-Key:\s+(?<key>.*)\r\n";
        private const String MagicKey = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
        private const Char charOne = '1';
        private const Char charZero = '0';

        #region Handshake

        public Byte[] ProduceResponse(string request)
        {
            String webSocketKey = Common.GetRegexValue(request, WebSocketKeyPattern)[0].Groups["key"].Value;
            String acceptKey = produceAcceptKey(webSocketKey);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(String.Concat("HTTP/1.1 101 Web Socket Protocol Handshake", Environment.NewLine));
            stringBuilder.Append(String.Concat("Upgrade: WebSocket", Environment.NewLine));
            stringBuilder.Append(String.Concat("Connection: Upgrade", Environment.NewLine));
            stringBuilder.Append(String.Concat("Sec-WebSocket-Accept: ", acceptKey, Environment.NewLine, Environment.NewLine));
            String asd = stringBuilder.ToString();
            return Encoding.UTF8.GetBytes(stringBuilder.ToString());
        }
        private String produceAcceptKey(String webSocketKey)
        {
            Byte[] acceptKey = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(webSocketKey + MagicKey));
            return Convert.ToBase64String(acceptKey);
        }

        #endregion

        #region Decode

        public Message Decode(Byte[] data)
        {
            Byte[] buffer = new Byte[14];
            if (data.Length >= 14)
                Buffer.BlockCopy(data, 0, buffer, 0, 14);
            else
                Buffer.BlockCopy(data, 0, buffer, 0, data.Length);
            MessageHeader header = analyseHead(buffer);
            Message msg = new Message();
            msg.header = header;

            Byte[] payload;
            if (header != null)
            {
                payload = new Byte[data.Length - header.PayloadDataStartIndex];
                Buffer.BlockCopy(data, header.PayloadDataStartIndex, payload, 0, payload.Length);
                if (header.MASK == charOne)
                {
                    for (int i = 0; i < payload.Length; i++)
                    {
                        payload[i] = (Byte)(payload[i] ^ header.Maskey[i % 4]);
                    }
                }
            }
            else
            {
                msg.Data = Encoding.UTF8.GetString(data);
                return msg;
            }

            if (header.Opcode == OperType.Text)
                msg.Data = Encoding.UTF8.GetString(payload);

            return msg;
        }
        private MessageHeader analyseHead(Byte[] buffer)
        {
            MessageHeader header = new MessageHeader();
            header.FIN = (buffer[0] & 0x80) == 0x80 ? charOne : charZero;
            header.RSV1 = (buffer[0] & 0x40) == 0x40 ? charOne : charZero;
            header.RSV2 = (buffer[0] & 0x20) == 0x20 ? charOne : charZero;
            header.RSV3 = (buffer[0] & 0x10) == 0x10 ? charOne : charZero;

            if ((buffer[0] & 0xA) == 0xA)
                header.Opcode = OperType.Pong;
            else if ((buffer[0] & 0x9) == 0x9)
                header.Opcode = OperType.Ping;
            else if ((buffer[0] & 0x8) == 0x8)
                header.Opcode = OperType.Close;
            else if ((buffer[0] & 0x2) == 0x2)
                header.Opcode = OperType.Binary;
            else if ((buffer[0] & 0x1) == 0x1)
                header.Opcode = OperType.Text;
            else if ((buffer[0] & 0x0) == 0x0)
                header.Opcode = OperType.Row;

            header.MASK = (buffer[1] & 0x80) == 0x80 ? charOne : charZero;
            Int32 len = buffer[1] & 0x7F;
            if (len == 126)
            {
                header.Payloadlen = (UInt16)(buffer[2] << 8 | buffer[3]);
                if (header.MASK == charOne)
                {
                    header.Maskey = new Byte[4];
                    Buffer.BlockCopy(buffer, 4, header.Maskey, 0, 4);
                    header.PayloadDataStartIndex = 8;
                }
                else
                    header.PayloadDataStartIndex = 4;
            }
            else if (len == 127)
            {
                Byte[] byteLen = new Byte[8];
                Buffer.BlockCopy(buffer, 4, byteLen, 0, 8);
                header.Payloadlen = BitConverter.ToUInt64(byteLen, 0);
                if (header.MASK == charOne)
                {
                    header.Maskey = new Byte[4];
                    Buffer.BlockCopy(buffer, 10, header.Maskey, 0, 4);
                    header.PayloadDataStartIndex = 14;
                }
                else
                    header.PayloadDataStartIndex = 10;
            }
            else
            {
                if (header.MASK == charOne)
                {
                    header.Maskey = new Byte[4];
                    Buffer.BlockCopy(buffer, 2, header.Maskey, 0, 4);
                    header.PayloadDataStartIndex = 6;
                }
                else
                    header.PayloadDataStartIndex = 2;
            }
            return header;
        }

        #endregion

        #region Encode

        public Byte[] Encode(String msg)
        {
            Byte[] data = Encoding.UTF8.GetBytes(msg);
            Int32 dataLength = data.Length;

            Byte[] head = packetHeader(OperType.Text, dataLength);
            if (mask)
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = (Byte)(data[i] ^ maskKey[i % 4]);
                }

            Byte[] result = new Byte[head.Length + dataLength];
            Buffer.BlockCopy(head, 0, result, 0, head.Length);
            Buffer.BlockCopy(data, 0, result, head.Length, dataLength);
            return result;
        }
        private const Byte byte80 = 0x80;
        private Byte[] maskKey = new Byte[] { 113, 105, 97, 110 };
        private bool mask = false;
        private Byte[] packetHeader(OperType operType, Int32 length)
        {
            Byte byteHead = (Byte)(byte80 | (Byte)operType);
            Byte[] byteLen;
            if (length < 126)
            {
                byteLen = new Byte[1];
                byteLen[0] = (Byte)(byte80 | (Byte)length);
            }
            else if (length < 65535)
            {
                byteLen = new Byte[3];
                byteLen[0] = (Byte)(byte80 | (Byte)126);
                for (int i = 1; i < 3; i++)
                    byteLen[i] = (Byte)(length >> (8 * (2 - i)));
            }
            else
            {
                byteLen = new Byte[9];
                byteLen[0] = (Byte)(byte80 | (Byte)127);
                for (int i = 1; i < 9; i++)
                    byteLen[i] = (Byte)(length >> (8 * (8 - i)));
            }
            Byte[] packet;
            if (mask)
                packet = new Byte[1 + byteLen.Length + maskKey.Length];
            else packet = new Byte[1 + byteLen.Length];
            packet[0] = byteHead;
            Buffer.BlockCopy(byteLen, 0, packet, 1, byteLen.Length);
            if (mask)
                Buffer.BlockCopy(maskKey, 0, packet, 1 + byteLen.Length, maskKey.Length);
            return packet;
        }

        #endregion
        public byte[] Send(String msg)
        {
            Byte[] binary = Encoding.UTF8.GetBytes(msg);
            try
            {
                ulong headerLength = 2;
                byte[] data = binary;

                bool mask = false;
                byte[] maskKeys = null;

                if (mask)
                {
                    headerLength += 4;
                    data = (byte[])data.Clone();

                    Random random = new Random(Environment.TickCount);
                    maskKeys = new byte[4];
                    for (int i = 0; i < 4; ++i)
                    {
                        maskKeys[i] = (byte)random.Next(byte.MinValue, byte.MaxValue);
                    }

                    for (int i = 0; i < data.Length; ++i)
                    {
                        data[i] = (byte)(data[i] ^ maskKeys[i % 4]);
                    }
                }

                byte payload;
                if (data.Length >= 65536)
                {
                    headerLength += 8;
                    payload = 127;
                }
                else if (data.Length >= 126)
                {
                    headerLength += 2;
                    payload = 126;
                }
                else
                {
                    payload = (byte)data.Length;
                }

                byte[] header = new byte[headerLength];

                header[0] = 0x80 | 0x1;
                if (mask)
                {
                    header[1] = 0x80;
                }
                header[1] = (byte)(header[1] | payload & 0x40 | payload & 0x20 | payload & 0x10 | payload & 0x8 | payload & 0x4 | payload & 0x2 | payload & 0x1);

                if (payload == 126)
                {
                    byte[] lengthBytes = BitConverter.GetBytes((ushort)data.Length);
                    Array.Reverse(lengthBytes);
                    header[2] = lengthBytes[0];
                    header[3] = lengthBytes[1];

                    if (mask)
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            header[i + 4] = maskKeys[i];
                        }
                    }
                }
                else if (payload == 127)
                {
                    byte[] lengthBytes = BitConverter.GetBytes((ulong)data.Length);
                    Array.Reverse(lengthBytes);
                    for (int i = 0; i < 8; ++i)
                    {
                        header[i + 2] = lengthBytes[i];
                    }
                    if (mask)
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            header[i + 10] = maskKeys[i];
                        }
                    }
                }
                Byte[] packet = new Byte[header.Length + data.Length];
                Buffer.BlockCopy(header, 0, packet, 0, header.Length);
                Buffer.BlockCopy(data, 0, packet, header.Length, data.Length);
                return packet;

            }
            catch (Exception ex)
            {
                Console.WriteLine("websocket transport protocol Send exception: " + ex.ToString());
            }

            return null;
        }
    }
}
