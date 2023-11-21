using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CommTest
{
    internal class LogMessage
    {
        public delegate void DebugMessageHandler(string msg);

        public event DebugMessageHandler DebugMessageEvent;

        /// <summary>
        /// log 파일 저장 폴더
        /// </summary>
        private string logDir = "";

        /// <summary>
        /// Log Message Queue - 파일 저장용
        /// </summary>
        private Queue<string> LogMsgQue = new Queue<string>();
        /// <summary>
        /// Log Message Queue - 화면 출력용
        /// </summary>
        private Queue<string> LogMsgQueOut = new Queue<string>();

        /// <summary>
        /// Log Message 파일 저장 여부
        /// </summary>
        public bool IsWiteFile = true;

        /// <summary>
        /// Log Message 처리 Thread
        /// </summary>
        Thread tLogMessge;

        public LogMessage()
        {
            // 현재 폴더 위치를 가져옴
            string strLocal = System.IO.Directory.GetCurrentDirectory();

            // 로그 폴더가 없으면 생성 
            logDir = strLocal + "/Log";
            if (!System.IO.Directory.Exists(logDir))
            {
                System.IO.Directory.CreateDirectory(logDir);
            }

            tLogMessge = new Thread(new ThreadStart(thread_ProcessLogMsg));
            tLogMessge.Start();
        }

        /// <summary>
        /// 파일 저장용 로그 메시지 처리 Thread
        /// </summary>
        private void thread_ProcessLogMsg()
        {
            while (true)
            {
                if (LogMsgQue.Count > 0)
                {
                    WriteFile(LogMsgQue.Dequeue());

                    if (LogMsgQueOut.Count > 0)
                    {
                        DebugMessageEvent?.Invoke(Read());
                    }
                }
            }
        }

        ~LogMessage()
        {
            tLogMessge.Abort();
            tLogMessge.Join();
        }

        /// <summary>
        /// 파일에 로그 메시지 쓰기
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool WriteFile(string strMsg)
        {
            if (!IsWiteFile) return false;

            string logFileName = DateTime.Now.ToString("yyyyMMdd", CultureInfo.CurrentCulture) + ".txt";
            string logFile = Path.Combine(logDir, logFileName);

            try
            {
                System.IO.StreamWriter FileWriter = new System.IO.StreamWriter(logFile, true, Encoding.UTF8);
                //FileWriter.Write(DateTime.Now.ToString("[hh:mm:ss.fff] ") + strMsg + "\r\n");
                FileWriter.Write(strMsg);
                FileWriter.Flush();
                FileWriter.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 로그 메시지 쓰기
        /// </summary>
        /// <param name="msg">Log 내용</param>
        /// <param name="IsShowMsg">메시지 출력 여부. </param>
        public void Write(string msg, bool IsShowMsg)
        {
            LogMsgQue.Enqueue(msg);

            if (IsShowMsg)
            {
                LogMsgQueOut.Enqueue(msg);
            }
        }

        /// <summary>
        /// 로그 메시지 전체 읽기 - 화면 출력용
        /// Write() 함수에서 IsShowMsg = true 인 경우만 읽을 수 있음
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            string returnStaring = "";
            while (LogMsgQueOut.Count > 0)
            {
                returnStaring += LogMsgQueOut.Dequeue();
            }
            return returnStaring;
        }

        /// <summary>
        /// 로그 메시지 1라인 읽기 - 화면 출력용
        /// Write() 함수에서 IsShowMsg = true 인 경우만 읽을 수 있음
        /// </summary>
        /// <returns></returns>
        public string ReadLine()
        {
            if (LogMsgQueOut.Count > 0)
            {
                return LogMsgQueOut.Dequeue();
            }
            return "";
        }

        /// <summary>
        /// 로그 메시지 개수
        /// </summary>
        public int Count
        {
            get
            {
                return LogMsgQueOut.Count;
            }
        }

        /// <summary>
        /// 종료
        /// </summary>
        public void Close()
        {
            tLogMessge.Abort();
            tLogMessge.Join();
        }
    }

    /// <summary>
    /// 윈폼의 텍스트 박스를 연결하여 로그 메시지를 출력하는 클래스.
    /// 폼 Laod 이벤트에서 LogTextBox 객체를 생성하여 사용.
    /// 종료시 Close() 함수 호출.
    /// Write() 함수를 통해 로그 메시지를 출력(로그는 파일로 자동 저장되며 화면 출력 선택 가능).
    /// 텍스트 박스에 출력만 할 경우 IsPrintDebugMsg = false 로 설정.
    /// </summary>
    public partial class LogTextBox : MaterialForm
    {
        /// <summary>
        /// 텍스트 박스
        /// </summary>
        public MaterialMultiLineTextBox tbDebugText;

        /// <summary>
        /// 텍스트 박스의 최대 라인 수. 0이면 무제한
        /// </summary>
        public int nLimitLines { set; get; } = 1000;

        /// <summary>
        /// 사용자가 화면 출력을 일시 정지 선택, true 출력, false 정지
        /// </summary>
        public bool IsPausePrint { set; get; } = true;

        private LogMessage debugMessage = new LogMessage();
        delegate void CrossThreadSafetyText(string text);

        public bool IsWiteFile
        {
            set => debugMessage.IsWiteFile = value;
            get => debugMessage.IsWiteFile;
        }

        /// <summary>
        /// 윈폼의 텍스트 박스 연결
        /// </summary>
        /// <param name="textBox"></param>
        public LogTextBox(MaterialMultiLineTextBox textBox)
        {
            tbDebugText = textBox;
            debugMessage.DebugMessageEvent += new LogMessage.DebugMessageHandler(PrintDebug);

            // tPrintDebugMessge = new Thread(new ThreadStart(thread_PrintDebugMessage));
            // tPrintDebugMessge.Start();
        }

        /// <summary>
        /// 텍스트 박스에 출력 및 파일 쓰기
        /// </summary>
        /// <param name="message">로그 메시지</param>
        /// <param name="IsVisible">텍스트 박스에 출력 여부</param>
        public void Write(string message, bool IsVisible)
        {
            string msg = DateTime.Now.ToString("[HH:mm:ss.fff] ") + message + "\r\n";
            if (IsPausePrint) debugMessage.Write(msg, IsVisible);
        }

        /// <summary>
        /// 텍스트 박스에 출력
        /// </summary>
        /// <param name="message">로그 메시지</param>
        public void PrintDebug(string message)
        {
            if (tbDebugText.InvokeRequired)
            {
                try
                {
                    tbDebugText.Invoke(new CrossThreadSafetyText(PrintDebug), message);
                }
                catch { }
            }
            else if (nLimitLines == 0)
            {
                try
                {
                    tbDebugText.AppendText(message);
                }
                finally { }
            }
            else
            {
                try
                {
                    tbDebugText.AppendText(message);

                    if (tbDebugText.Lines.Length > nLimitLines)
                    {
                        LinkedList<string> tempLines = new LinkedList<string>(tbDebugText.Lines);

                        while ((tempLines.Count - nLimitLines) > 0)
                        {
                            tempLines.RemoveFirst();
                        }

                        tbDebugText.Lines = tempLines.ToArray();
                    }
                    tbDebugText.Select(tbDebugText.Text.Length, 0);
                    tbDebugText.ScrollToCaret();
                }
                finally { }
            }
        }

        /// <summary>
        /// 종료
        /// </summary>
        public new void Close()
        {
            base.Close();
            debugMessage.Close();
        }

        /// <summary>
        /// textbox 출력을 위한 Thread
        /// </summary>
        Thread tPrintDebugMessge;

        /// <summary>
        /// 텍스트 박스 출력 쓰레드
        /// </summary>
        private void thread_PrintDebugMessage()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            long lastTime = sw.ElapsedMilliseconds;
            long currentTime = sw.ElapsedMilliseconds;
            const int interval = 10;

            while (true)
            {
                while ((currentTime - lastTime) < interval)
                {
                    Thread.Sleep(1);
                    currentTime = sw.ElapsedMilliseconds;
                }
                lastTime += interval;
                Write("thread_PrintDebugMessage", false);

                while (debugMessage.Count > 0)
                {
                    if (tbDebugText.InvokeRequired)
                    {
                        tbDebugText.Invoke(new Action(() =>
                        {
                            tbDebugText.AppendText(debugMessage.Read());
                        }));
                    }
                    else
                    {
                        tbDebugText.AppendText(debugMessage.Read());
                    }
                }
            }
        }
    }
}
