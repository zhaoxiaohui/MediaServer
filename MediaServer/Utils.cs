using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaServer
{
    class UISync
    {
        private static ISynchronizeInvoke Sync;

        public static void Init(ISynchronizeInvoke sync)
        {
            Sync = sync;
        }

        public static void Execute(Action action)
        {
            Sync.BeginInvoke(action, null);
        }
    }
}
