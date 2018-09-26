using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanomsgPlus.Core
{
    public static class Survey
    {
        public const int NN_PROTO_SURVEY = 6;
        public const int NN_SURVEYOR = (NN_PROTO_SURVEY * 16 + 2);
        public const int NN_RESPONDENT = (NN_PROTO_SURVEY * 16 + 3);

        //Option
        public const int NN_SURVEYOR_DEADLINE = 1;
    }
}
