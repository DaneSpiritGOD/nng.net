using NanomsgPlus.Core;
using NanomsgPlus;
using NanomsgPlus.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NanomsgPlus.Core
{
    public class SurveyorSocketPlus : NanomsgSocketBase, IConnectSocket, IBindSocket, ISendSocket, IReceiveSocket
    {
        public SurveyorSocketOptions SurveyorOptions { get; private set; }

        public SurveyorSocketPlus()
            : base(Domain.SP, (Protocol)Survey.NN_SURVEYOR)
        {
            if (SocketID >= 0)
            {
                SurveyorOptions = new SurveyorSocketOptions(SocketID);
            }
        }

        public NanomsgEndpoint Connect(string address)
        {
            return ConnectImpl(address);
        }

        public NanomsgEndpoint Connect(IPAddress address, int port)
        {
            return ConnectImpl(address, port);
        }

        public NanomsgEndpoint Bind(string address)
        {
            return BindImpl(address);
        }

        public void Send(byte[] buffer)
        {
            SendImpl(buffer);
        }

        public bool SendImmediate(byte[] buffer)
        {
            return SendImmediateImpl(buffer);
        }

        public NanomsgWriteStream CreateSendStream()
        {
            return CreateSendStreamImpl();
        }

        public void SendStream(NanomsgWriteStream stream)
        {
            SendStreamImpl(stream);
        }

        public bool SendStreamImmediate(NanomsgWriteStream stream)
        {
            return SendStreamImmediateImpl(stream);
        }

        public byte[] Receive()
        {
            return ReceiveImpl();
        }

        public byte[] ReceiveImmediate()
        {
            return ReceiveImmediateImpl();
        }

        public NanomsgReadStream ReceiveStream()
        {
            return ReceiveStreamImpl();
        }

        public NanomsgReadStream ReceiveStreamImmediate()
        {
            return ReceiveStreamImmediateImpl();
        }
    }

}
