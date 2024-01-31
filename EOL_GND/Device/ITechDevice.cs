using EOL_GND.Common;
using EOL_GND.Model;
using EOL_GND.Properties;
using Ivi.Visa;
using Keysight.Visa;
using System;
using System.Threading;

namespace EOL_GND.Device
{
    public class ITechDevice : IDisposable
    {
        public bool Connected => mbSession != null;

        private MessageBasedSession mbSession = null;
        public VisaDeviceSetting Setting { get; private set; }

        private const string NotConnectedMessage = "VISA 디바이스에 연결되지 않았습니다.";
        public void Connect(VisaDeviceSetting setting, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            // Debugging.
            Logger.LogVerbose($"Connecting to VISA device {setting.DeviceName}({setting.Address})");
            if (Connected)
            {
                return;
            }

            if (string.IsNullOrEmpty(setting.Address))
            {
                throw new Exception("VISA Address가 비어 있습니다.");
            }

            using(var resManager = new ResourceManager())
            {
                var registration = token.Register(() => resManager.Dispose());

                Logger.LogVerbose($"Connecting to ITech device { setting.DeviceName} ({ setting.Address})");
                mbSession?.Dispose();
                mbSession = null;
                var session = resManager.Open(setting.Address);

                Logger.LogInfo($"Connected to {session.ResourceName}");
                registration.Dispose();

                if(session is MessageBasedSession mbs)
                {
                    mbs.TimeoutMilliseconds = setting.IOTimeout;
                    mbSession = mbs;
                    Setting = setting;
                    
                    if(mbs.SessionType == SessionType.TcpipSocketSession)
                    {
                        mbs.TerminationCharacterEnabled = true;
                    }
                    else
                    {
                        session?.Dispose();
                        throw new Exception($"{setting.Address}는 지원되지 않는 IT디바이스 입니다.");
                    }
                }
            }
        }
        public void Dispose()
        {
        }










        public void Disconnect()
        {
            if (mbSession != null)
            {
                mbSession.Dispose();
                mbSession = null;
            }
        }
        public void WriteLine(string command, CancellationToken token, bool showLog = true)
        {
            if (!Connected && Setting != null)
            {
                // 설정이 비어있지 않으면 연결 시도.
                Connect(Setting, CancellationToken.None);
            }

            if (!Connected)
            {
                throw new Exception(NotConnectedMessage);
            }

            token.ThrowIfCancellationRequested();
            var registration = token.Register(Disconnect);

            try
            {
                if (showLog)
                {
                    Logger.LogCommMessage(Setting.DeviceName, command, true);
                }
                mbSession.FormattedIO.WriteLine(command);
            }
            catch (NativeVisaException nativeVisaEx)
            {
                if (nativeVisaEx.ErrorCode == NativeErrorCode.ConnectionLost && Setting != null)
                {
                    // 연결이 끊어진 경우 다시 시도.
                    Disconnect();
                    Connect(Setting, token);
                    WriteLine(command, token, showLog);
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                registration.Dispose();
            }
        }

        public string ReadLine(CancellationToken token, bool showLog = true)
        {
            if (!Connected)
            {
                throw new Exception(NotConnectedMessage);
            }

            token.ThrowIfCancellationRequested();
            var registration = token.Register(Disconnect);

            try
            {
                var response = mbSession.FormattedIO.ReadLine();
                if (showLog)
                {
                    Logger.LogCommMessage(Setting.DeviceName, response, false);
                }
                return response;
            }
            finally
            {
                registration.Dispose();
            }
        }
    }
}