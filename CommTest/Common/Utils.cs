using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace CommTest
{
    class Utils
    {
        public Utils()
        {

        }

        public System.DateTime Get_BuildDateTime(System.Version version = null)
        {
            // 주.부.빌드.수정
            // 주 버전    Major Number
            // 부 버전    Minor Number
            // 빌드 번호  Build Number
            // 수정 버전  Revision NUmber

            //매개 변수가 존재할 경우
            if (version == null)
            {
                version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }

            //세번째 값(Build Number)은 2000년 1월 1일부터
            //Build된 날짜까지의 총 일(Days) 수 이다.
            int day = version.Build;
            System.DateTime dtBuild = new System.DateTime(2000, 1, 1).AddDays(day);

            //네번째 값(Revision NUmber)은 자정으로부터 Build된
            //시간까지의 지나간 초(Second) 값 이다.
            int intSeconds = version.Revision;
            intSeconds *= 2;
            dtBuild = dtBuild.AddSeconds(intSeconds);


            //시차 보정
            System.Globalization.DaylightTime daylingTime = System.TimeZone.CurrentTimeZone
                    .GetDaylightChanges(dtBuild.Year);
            if (System.TimeZone.IsDaylightSavingTime(dtBuild, daylingTime))
            {
                dtBuild = dtBuild.Add(daylingTime.Delta);
            }

            return dtBuild;
        }

        public byte CalChecksumByte(byte[] data)
        {
            byte checksum = 0;

            if (data != null)
            {
                for (int i = 1; i < (data.Length - 2); i++)
                {
                    checksum ^= data[i];
                }
            }
            return checksum;
        }

        public UInt16 CalChecksum(byte[] data)
        {
            UInt16 checksum = 0;
            UInt16[] dataUInt16Array = new UInt16[data.Length / 2];
            Buffer.BlockCopy(data, 0, dataUInt16Array, 0, data.Length);

            if (data != null)
            {
                for (int i = 0; i < (dataUInt16Array.Length - 1); i++)
                {
                    checksum ^= dataUInt16Array[i];
                }
            }
            return checksum;
        }

        public UInt16 CalChecksum(UInt16[] data)
        {
            UInt16 checksum = 0;

            if (data != null)
            {
                for (int i = 0; i < (data.Length - 1); i++)
                {
                    checksum ^= data[i];
                }
            }
            return checksum;
        }

        public UInt32 CalChecksum(object obj)
        {
            byte[] arr = StructureToByteArray(obj);
            byte[] arrChecksum = new byte[4];

            for (int i = 0; i < ((arr.Length / 4) - 1); i++)
            {
                arrChecksum[0] ^= arr[0 + (i * 4)];
                arrChecksum[1] ^= arr[1 + (i * 4)];
                arrChecksum[2] ^= arr[2 + (i * 4)];
                arrChecksum[3] ^= arr[3 + (i * 4)];
            }

            return BitConverter.ToUInt32(arrChecksum, 0);
        }

        /// <summary>
        /// 48MHz 기준으로  클럭을 시간으로 변환. 1clk -> 1/48us.
        /// </summary>
        /// <param name="clock"></param>
        /// <returns></returns>
        public string ConvertClockToTime(UInt32 clock)
        {
            double time_ns = clock * (1000.0 / 48.0);
            string timeString;

            if (time_ns < 1000)
            {
                timeString = time_ns.ToString("0") + " ns";
            }
            else
            {
                timeString = time_ns < 1000000
                    ? string.Format("{0:0.00}", (double)time_ns / 1000) + " us"
                    : string.Format("{0:0.00}", (double)time_ns / 1000000) + " ms";
            }

            return timeString;
        }

        /// <summary>
        /// 10kHz 기준으로  클럭을 시간으로 변환. 1clk -> 100us.
        /// </summary>
        /// <param name="clock"></param>
        /// <returns></returns>
        public string ConvertClock10kToTime(UInt16 clock)
        {
            double time_ms = clock * 0.1;
            string timeString = clock < 10000 ? string.Format("{0:0.0}", time_ms) + " ms" : string.Format("{0:0.00}", time_ms / 1000) + " s";
            return timeString;
        }

        public byte[] ConvertUInt16ArrayToByteArray(UInt16[] packetData)
        {
            byte[] bytePakcet = new byte[packetData.Length * 2];
            Buffer.BlockCopy(packetData, 0, bytePakcet, 0, bytePakcet.Length);

            return bytePakcet;
        }

        public UInt16[] ConvertHex16bitStringToUInt16Array(string StringPacket)
        {
            UInt16[] tmpPacket = new UInt16[300];


            StringPacket = StringPacket.ToUpper().Replace("0X", ""); //16진수 표시 0x 제거

            string[] hexValuesSplit = StringPacket.Split(' ');

            int dataLength = 0;
            foreach (string hex in hexValuesSplit)
            {
                try
                {
                    tmpPacket[dataLength++] = (UInt16)Convert.ToInt32(hex, 16);
                }
                catch
                {
                    dataLength--;
                }
            }
            Array.Resize(ref tmpPacket, dataLength);
            Utils util = new Utils();
            tmpPacket[tmpPacket.Length - 1] = util.CalChecksum(tmpPacket);

            return tmpPacket;
        }

        public byte[] ConvertHexWordStringToByteArray(string StringPacket)
        {
            UInt16[] wordPacket = ConvertHex16bitStringToUInt16Array(StringPacket);
            byte[] bytePakcet = ConvertUInt16ArrayToByteArray(wordPacket);

            return bytePakcet;
        }

        public static UInt32 stringToUInt32(string strData)
        {
            UInt32 returnValue = 0;
            try
            {
                int index = strData.IndexOf("0x");
                if ((index != -1) && (strData.Length <= 10))
                {
                    return Convert.ToUInt32(strData.Replace("0x", ""), 16);
                }

                index = strData.IndexOf("0b");
                if (index != -1)
                {
                    return Convert.ToUInt32(strData.Replace("0b", ""), 2);
                }

                if (strData.Length <= 10)
                {
                    returnValue = Convert.ToUInt32(System.Text.RegularExpressions.Regex.Replace(strData, @"[^0-9]", ""));
                }
                else
                {
                    // PrintDebugMsg(strData + " UInt32 범위 내 값이 아닙니다.\r\n");
                }
            }
            catch
            {
                // PrintDebugMsg(strData + " 정상적인 데이터가 아닙니다.\r\n");
            }

            return returnValue;
        }

        public static Int32 stringToInt32(string strData)
        {
            Int32 returnValue = 0;
            try
            {
                // 16진수
                if ((strData.IndexOf("0x") != -1) && (strData.Length <= 0x0f))
                {
                    // 16진수 값을 제외한 문자열은 제거
                    string sHex = Regex.Replace(strData, @"[^0-9a-fA-F]", "");
                    // 제거한 문자열에 남은 값이 있으면 변환
                    if (sHex != "")
                    {
                        //return Int32.Parse(sHex, NumberStyles.HexNumber);
                        return Convert.ToInt32(sHex.ToString(), 16);
                    }
                }

                // 2진수
                if (strData.IndexOf("0b") != -1)
                {
                    // 2진수 값을 제외한 문자열은 제거
                    string sBin = Regex.Replace(strData, @"[^0-1]", "");
                    // 제거한 문자열에 남은 값이 있으면 변환
                    if (sBin != "")
                    {
                        return Convert.ToInt32(sBin.ToString(), 2);
                    }
                }

                // 10진수
                if (strData.Length <= 0x0f)
                {
                    returnValue = Convert.ToInt32(Regex.Replace(strData, @"[^0-9,-]", ""));
                }
                else
                {
                    // PrintDebugMsg(strData + " UInt32 범위 내 값이 아닙니다.\r\n");
                }
            }
            catch
            {
                // PrintDebugMsg(strData + " 정상적인 데이터가 아닙니다.\r\n");
            }

            return returnValue;
        }

        /// <summary>
        /// 구조체를 바이트배열로 변환
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public byte[] StructureToByteArray(object obj)
        {
            int len = Marshal.SizeOf(obj);
            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        /// <summary>
        /// 로그파일 텍스트로 저장
        /// </summary>
        /// <param name="logString"></param>
        public void saveLogFile(string logString)
        {
            bool existFile = false;
            string saveLogDir = "log";
            string saveLogFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            if (existFile == false)
            {
                saveLogFileName = System.IO.Path.Combine(saveLogDir, saveLogFileName);

                if (File.Exists(saveLogFileName))
                {
                    existFile = true;
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(saveLogDir);
                    if (di.Exists == false)
                    {
                        di.Create();
                    }

                    StreamWriter swLog = new StreamWriter(saveLogFileName, false);
                    swLog.Close();
                    existFile = true;
                }
            }

            string saveText = DateTime.Now.ToString("\r\n[HH:mm:ss] ") + logString;
            System.IO.File.AppendAllText(saveLogFileName, saveText, Encoding.Default);
        }

        /// <summary>
        /// IP가 적합한지 확인
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public bool IsValidIp(string addr)
        {
            bool valid = !string.IsNullOrEmpty(addr) && IPAddress.TryParse(addr, out _);
            return valid;
        }

        public byte[] ConvertHexStringToByteArray(string hexString)
        {
            hexString = hexString.Replace(" ", ""); // 공백 제거

            if (hexString.Length % 2 != 0)
                throw new ArgumentException("유효하지 않은 16진수 문자열입니다.");

            byte[] byteArray = new byte[hexString.Length / 2];
            for (int i = 0; i < byteArray.Length; i++)
            {
                string byteString = hexString.Substring(i * 2, 2);
                byteArray[i] = Convert.ToByte(byteString, 16);
            }

            return byteArray;
        }
    }
}
