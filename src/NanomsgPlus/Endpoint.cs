using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NanomsgPlus
{
    public static class Endpoint
    {
        public static string ToTcp(string ip, int port)
        {
            if (!port.IsValidPort()) throw new ArgumentOutOfRangeException($"Illegal Port:{port}");
            if (!ip.IsValidIP()) throw new ArgumentOutOfRangeException($"Illegal ip:{ip}");

            return $"tcp://{ip.AutoAdjust()}:{port}";
        }

        public static string ToAnyTcp(int port)
        {
            return ToTcp("*", port);
        }

        public static string AutoAdjust(this string addr)
        {
            return addr.Trim().Replace(" ", "");
        }

        public static bool IsValid(this string addr)
        {
            if (string.IsNullOrEmpty(addr)) return false;

            var addr_ = addr.AutoAdjust();
            var parts = addr_.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            switch (parts[0])
            {
                case "tcp":
                    return isTcpStructure(parts);
                case "ipc":
                    return isIPCStructure(parts);
                case "inproc":
                    throw new NotImplementedException("I have no time to implement.");
                case "ws":
                    throw new NotImplementedException("I have no time to implement.");
                default:
                    throw new NotImplementedException(" Unknown Protocol.");
            }
        }

        private static bool isTcpStructure(params string[] parts)
        {
            return parts.Length == 3 &&
                (parts[0] == "tcp") &&
                (parts[1].StartsWith("//")) &&
                (parts[1].Substring(2).IsValidIP()) &&
                (int.TryParse(parts[2], out int port)) &&
                port.IsValidPort();
        }

        private static bool isIPCStructure(params string[] parts)
        {
            return parts.Length == 2 &&
                (parts[0] == "ipc") &&
                (parts[1].StartsWith("//"));
        }
    }
}
