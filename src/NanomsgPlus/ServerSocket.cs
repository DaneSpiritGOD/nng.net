using NanomsgPlus.Core;
using NanomsgPlus.Protocols;
using System;
using System.Collections.Generic;
using System.Text;

namespace NanomsgPlus
{
    public static class ServerSocket
    {
        public static NanomsgSocketBase CreatePipelineSocket(this string addr)
        {
            NotTrueException.Assert(addr.IsValid(), nameof(addr));

            var socket = new PullSocket();
            socket.Bind(addr);
            return socket;
        }

        public static NanomsgSocketBase CreateHugeBufferPipelineSocket(this string addr)
        {
            NotTrueException.Assert(addr.IsValid(), nameof(addr));

            var socket = new PullSocket();
            socket.Bind(addr);
            socket.SetInfiniteReceiveMaxSize();

            return socket;
        }

        public static SurveyorSocketPlus CreateSurveyorSocket(this string addr)
        {
            NotTrueException.Assert(addr.IsValid(), nameof(addr));

            var socket = new SurveyorSocketPlus();
            socket.Bind(addr);

            return socket;
        }
    }
}
