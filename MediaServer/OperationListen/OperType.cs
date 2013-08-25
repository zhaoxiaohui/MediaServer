using System;

namespace ValueWebSocket.Protocol
{
    public enum OperType
    {
        Row = 0x0,
        Text = 0x1,
        Binary = 0x2,
        Close = 0x8,
        Ping = 0x9,
        Pong = 0xA,
        Unkown
    };
    public enum TokenState
    {
        Open = 0,
        Running = 1,
        Close = 2
    };
}
