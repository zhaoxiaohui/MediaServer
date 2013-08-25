using System;

namespace ValueWebSocket.Protocol
{
    public interface IProtocol
    {
        Byte[] ProduceResponse(String request);

        Byte[] Encode(String msg);

        Message Decode(Byte[] data);

        Byte[] Send(String msg);
    }
}
