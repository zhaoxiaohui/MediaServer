using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ValueWebSocket.Infrastructure;

namespace ValueWebSocket.Protocol
{
    public class ValueProtocol
    {
        private IProtocol protocol;
        private const String protocolVerPattern = @"Sec\-WebSocket\-Version:\s+(?<ver>\d+)\r\n";

        public Byte[] GetResponse(String request)
        {
            String version = getVersion(request);
            if (version == "13")
                protocol = new ProtocolDraft10();

            return protocol.ProduceResponse(request);
        }

        public Message Decode(Byte[] data)
        {
            return protocol.Decode(data);
        }

        public Byte[] Encode(String msg)
        {
            return protocol.Send(msg);
        }

        private String getVersion(String request)
        {
            Match match = Common.GetRegexValue(request, protocolVerPattern)[0];
            return match.Groups["ver"].Value;
        }
    }
}
