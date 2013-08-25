using Declarations;
using MediaServer.HCNetSDK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Web.Script.Serialization;
using ValueWebSocket.Protocol;

namespace MediaServer.OperationListen
{
    delegate void ProcessData(SocketAsyncEventArgs args);

    /// <summary>
    /// Token for use with SocketAsyncEventArgs.
    /// </summary>
    internal sealed class Token : IDisposable
    {
        private Socket connection;

        private StringBuilder sb;

        private Byte[] tmpMsg;

        private Int32 currentIndex;

        private HCNetSDKCMD sdkCMD;

        private ValueProtocol valueProtocol;



        public TokenState State;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="connection">Socket to accept incoming data.</param>
        /// <param name="bufferSize">Buffer size for accepted data.</param>
        internal Token(Socket connection, Int32 bufferSize)
        {
            this.connection = connection;
            this.sb = new StringBuilder(bufferSize);
            this.tmpMsg = new byte[bufferSize];
            sdkCMD = new HCNetSDKCMD();
            valueProtocol = new ValueProtocol();
            this.State = TokenState.Open;
        }

        /// <summary>
        /// Accept socket.
        /// </summary>
        internal Socket Connection
        {
            get { return this.connection; }
        }
        /// <summary>
        /// Process data received from the client.
        /// </summary>
        /// <param name="args">SocketAsyncEventArgs used in the operation.</param>
        /// <param name="mediaDir">the directory of the stored media</param>
        /// <param name="m_factory">the vlc factory for vlm</param>
        internal void ProcessData(SocketAsyncEventArgs args, String mediaDir)
        {

            // Get the message received from the client.
            String received = this.sb.ToString(0, this.sb.Length);
            //int nextPo = 1, prePo = 0;
            //bool end = false;
            //while (!end)
            //{    
            if (this.State == TokenState.Running)
            {
                //while (this.currentIndex >= 1 && nextPo < this.currentIndex && tmpMsg[nextPo] != 129)
                //{ nextPo++; }
                byte[] tmp = new Byte[this.currentIndex];
                Buffer.BlockCopy(tmpMsg, 0, tmp, 0, this.currentIndex);
                // prePo = nextPo;
                //if (prePo == this.currentIndex) end = true; ;
                Message message = valueProtocol.Decode(tmp);
                if (message.Data != null)
                {
                    received = message.Data.ToString();
                }
                if (message.header.Opcode == OperType.Close)
                {
                    this.State = TokenState.Close;
                }
            }
            Console.WriteLine(received);
            Byte[] sendBuffer = null;
            if (received.IndexOf("Connection") < 0)
            {
                //字符构成 cmd|content|ipaddress|portnumber|channel|username|password
                String[] options = received.Split('|');

                if (options.Length == 7)
                {
                    if ("vod".Equals(options[0]))
                    { //点播服务 即回放
                        List<String> mediaList = checkMedia(mediaDir, options[2] + "_554" + "_" + options[4] + "_" + options[1]); //options[1] yyyymmdd
                        if (mediaList.Count > 0)
                        {
                            var jsonSerialiser = new JavaScriptSerializer();
                            var json = jsonSerialiser.Serialize(mediaList);
                            sendBuffer = valueProtocol.Encode("{name:'vod',value:" + json.ToString() + "}");
                        }
                    }
                    else if ("ptz".Equals(options[0]))
                    { //云台控制
                        Int32 m_login = sdkCMD.Login(options[2], Int16.Parse(options[3]));
                        if (m_login >= 0)
                        {
                            if ("UPDown".Equals(options[1]))
                            {
                                sdkCMD.PTZ_UP_btnDown(Int32.Parse(options[4]));
                            }
                            else if ("UPUp".Equals(options[1]))
                            {
                                sdkCMD.PTZ_UP_btnUp(Int32.Parse(options[4]));
                            }
                            else if ("LEFTDown".Equals(options[1]))
                            {
                                sdkCMD.PTZ_LEFT_btnDown(Int32.Parse(options[4]));
                            }
                            else if ("LEFTUp".Equals(options[1]))
                            {
                                sdkCMD.PTZ_LEFT_btnUp(Int32.Parse(options[4]));
                            }
                            else if ("RIGHTDown".Equals(options[1]))
                            {
                                sdkCMD.PTZ_RIGHT_btnDown(Int32.Parse(options[4]));
                            }
                            else if ("RIGHTUp".Equals(options[1]))
                            {
                                sdkCMD.PTZ_RIGHT_btnUp(Int32.Parse(options[4]));
                            }
                            else if ("DOWNDown".Equals(options[1]))
                            {
                                sdkCMD.PTZ_DOWN_btnDown(Int32.Parse(options[4]));
                            }
                            else if ("DOWNUp".Equals(options[1]))
                            {
                                sdkCMD.PTZ_DOWN_btnUp(Int32.Parse(options[4]));
                            }
                            sendBuffer = valueProtocol.Encode("{name:'res',value:'ok'}");
                        }
                    }
                    else if ("gtc".Equals(options[0]))
                    {
                        Int32 m_login = sdkCMD.Login(options[2], Int16.Parse(options[3]));
                        if (m_login >= 0)
                        {
                            sendBuffer = valueProtocol.Encode("{name:'gtc',value:'" + sdkCMD.GetChannelNumber().ToString() + "'}");
                        }
                    }

                    //TODO Use message received to perform a specific operation.
                    Console.WriteLine("cmd:{0}, content:{1}", options[0], options[1]);
                }
            }
            else
            {
                sendBuffer = valueProtocol.GetResponse(received);
                this.State = TokenState.Running;
                // end = true;
            }
            //Byte[] sendBuffer = Encoding.ASCII.GetBytes(received);
            if (sendBuffer == null) sendBuffer = valueProtocol.Encode("{name:'res',value:'error'}");
            args.SetBuffer(sendBuffer, 0, sendBuffer.Length);
            // }
            // Clear StringBuffer, so it can receive more data from a keep-alive connection client.
            sb.Length = 0;
            this.currentIndex = 0;
        }
        internal List<String> checkMedia(String mediaDir, String datatime)
        {
            return FileOperation.getPatternFileList(mediaDir, datatime);
        }
        /// <summary>
        /// Set data received from the client.
        /// </summary>
        /// <param name="args">SocketAsyncEventArgs used in the operation.</param>
        internal void SetData(SocketAsyncEventArgs args)
        {
            Int32 count = args.BytesTransferred;

            if ((this.currentIndex + count) > this.sb.Capacity)
            {
                throw new ArgumentOutOfRangeException("count",
                    String.Format(CultureInfo.CurrentCulture, "Adding {0} bytes on buffer which has {1} bytes, the listener buffer will overflow.", count, this.currentIndex));
            }
            Buffer.BlockCopy(args.Buffer, args.Offset, tmpMsg, this.currentIndex, count);
            sb.Append(Encoding.UTF8.GetString(args.Buffer, args.Offset, count));
            this.currentIndex += count;
        }

        #region IDisposable Members

        /// <summary>
        /// Release instance.
        /// </summary>
        public void Dispose()
        {
            try
            {
                this.connection.Shutdown(SocketShutdown.Send);
            }
            catch (Exception)
            {
                // Throw if client has closed, so it is not necessary to catch.
            }
            finally
            {
                this.connection.Close();
            }
        }

        #endregion
    }
}
