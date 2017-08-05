using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LactafarmaAPI.Core.Logging
{
    public interface ILogger
    {
        void Information(string fmt, params object[] args);
        void Information(Exception ex);
        void Warning(string fmt, params object[] args);
        void Warning(Exception ex);
        void Error(string fmt, params object[] args);
        void Error(Exception ex);
        void Verbose(string fmt, params object[] args);
        void Verbose(Exception ex);
        IDisposable EnterLeaveVerbose(string fmt, params object[] args);
    }
}
