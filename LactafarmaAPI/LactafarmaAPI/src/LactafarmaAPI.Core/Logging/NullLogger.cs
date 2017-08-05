using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LactafarmaAPI.Core.Logging
{
    public sealed class NullLogger : ILogger
    {
        private static readonly Lazy<NullLogger> Lazy =
            new Lazy<NullLogger>(() => new NullLogger());

        private NullLogger()
        {
        }

        public static NullLogger Instance
        {
            get { return Lazy.Value; }
        }

        public void Information(string fmt, params object[] args)
        {
        }

        public void Information(Exception ex)
        {
        }

        public void Warning(string fmt, params object[] args)
        {
        }

        public void Warning(Exception ex)
        {
        }

        public void Error(string fmt, params object[] args)
        {
        }

        public void Error(Exception ex)
        {
        }

        public void Verbose(string fmt, params object[] args)
        {
        }

        public void Verbose(Exception ex)
        {
        }

        public IDisposable EnterLeaveVerbose(string fmt, params object[] args)
        {
            return null;
        }
    }
}
