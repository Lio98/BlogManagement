using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BlogManagement.Core
{
    public class CommonHelper
    {
        #region 计时器Stopwatch
        /// <summary>
        /// 计时器开始
        /// </summary>
        /// <returns></returns>
        public static Stopwatch TimerStart()
        {
            Stopwatch watch = new Stopwatch();
            watch.Reset();
            watch.Start();
            return watch;
        }
        /// <summary>
        /// 计时器结束
        /// </summary>
        /// <param name="watch"></param>
        /// <returns></returns>
        public static string TimerEnd(Stopwatch watch)
        {
            watch.Stop();
            double costtime = watch.ElapsedMilliseconds;
            return costtime.ToString();
        }
        #endregion

        #region 自动生成排序
        /// <summary>
        /// 自动生成排序编码  191209113730593
        /// </summary>
        /// <returns></returns>
        public static long SortCode
        {
            get { return long.Parse(DateTime.Now.ToString("yyMMddHHmmssfff")); }
        }
        #endregion

        #region 自动生成Guid
        /// <summary>
        /// 自动生成Guid
        /// </summary>
        public static string Guid
        {
            get
            {
                return System.Guid.NewGuid().ToString().Replace("-", "").ToLower();
            }
        }
        /// <summary>
        /// 生成一个新的有序的长整形“GUID”，在一秒内，重复概率低于 万分之一，速度较快，线程安全，
        /// 测试分布式ID：快速（每毫秒一万个不重复ID）模式 455630735488320001,1455630735498320002,1455630735498320003,1455630735498320004,1455630735498320005
        /// </summary>
        /// <returns></returns>
        public static long SequenceId
        {
            get
            {
                return InnerNewSequenceGUID(DateTime.Now, false);
            }
        }
        static int MachineID = 0;
        static int SeqNum;
        static readonly DateTime baseDate = new DateTime(2017, 3, 1);
        /// <summary>
        /// 获取一个新的有序GUID整数
        /// </summary>
        /// <param name="dt">当前时间</param>
        /// <param name="haveMs">是否包含毫秒，生成更加有序的数字，但这会增加重复率</param>
        /// <returns></returns>
        protected internal static long InnerNewSequenceGUID(DateTime dt, bool haveMs)
        {
            //日期以 2017.3.1日为基准，计算当前日期距离基准日期相差的天数，可以使用20年。
            //日期部分使用4位数字表示
            int days = (int)dt.Subtract(baseDate).TotalDays;
            //时间部分表示一天中所有的秒数，最大为 86400秒,共5位
            //日期时间总位数= 4（日期）+5（时间）+3（毫秒）=12
            int times = dt.Second + dt.Minute * 60 + dt.Hour * 3600;
            //long 类型最大值 9223 3720 3685 4775 807
            //可用随机位数= 19-12=7
            long datePart = ((long)days + 1000) * 1000 * 1000 * 1000 * 100;
            long timePart = (long)times * 1000 * 1000;
            long msPart = (long)dt.Millisecond * 1000;
            long dateTiePart = (datePart + timePart + msPart) * 10000;

            int mid = MachineID * 10000;
            //得到总数= 4（日期）+5（时间）+3（毫秒）+7(GUID)
            long seq = dateTiePart + mid;

            //线程安全的自增并且不超过最大值10000
            int startValue = System.Threading.Interlocked.Increment(ref SeqNum);
            while (startValue >= 10000)
            {
                SeqNum = 0;
                startValue = 0;
                //可能此时别的线程再次更改了 SeqNum
                while (startValue != SeqNum)
                {
                    startValue = System.Threading.Interlocked.Increment(ref SeqNum);
                }
            }
            seq = seq + startValue;
            return seq;
        }
        #endregion

        #region 生成时间戳
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp
        {
            get
            {
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return Convert.ToInt64(ts.TotalMilliseconds).ToString();
            }
        }

        /// <summary>
        /// 时间转时间戳
        /// </summary>
        public static long TimeToTimeStamp(DateTime dateTime)
        {
            TimeSpan ts = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }

        /// <summary>  
        /// 时间戳Timestamp转换成日期  
        /// </summary>  
        /// <param name="timeStamp"></param>  
        /// <returns></returns>  
        public static DateTime GetDateTime(string timeStamp)
        {
            try
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(timeStamp + "0000");
                TimeSpan toNow = new TimeSpan(lTime);
                DateTime targetDt = dtStart.Add(toNow);
                return targetDt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 生成0-9随机数
        /// <summary>
        /// 生成0-9随机数
        /// </summary>
        /// <param name="codeNum">生成长度</param>
        /// <returns></returns>
        public static string RndNum(int codeNum)
        {
            StringBuilder sb = new StringBuilder(codeNum);
            Random rand = new Random();
            for (int i = 1; i < codeNum + 1; i++)
            {
                int t = rand.Next(9);
                sb.AppendFormat("{0}", t);
            }
            return sb.ToString();

        }
        #endregion

        #region 判断是否为图片
        /// <summary>
        /// 判断是否为图片
        /// </summary>
        /// <param name="ext">扩展名</param>
        /// <returns>返回Bool值，是则返回true</returns>
        public static bool IsImage(string ext)
        {
            ext = ext.ToLower();
            if (ext == ".gif" || ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".bmp")
            {
                return true;
            }
            else return false;
        }
        #endregion

        #region 删除最后一个字符之后的字符
        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }
        /// <summary>
        /// 删除最后结尾的长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string DelLastLength(string str, int Length)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            str = str.Substring(0, str.Length - Length);
            return str;
        }
        #endregion

        #region 系统表单时间格式转换
        public static void SysTableDateCon(Dictionary<string, object> dic)
        {
            var keyList = new List<string>();
            foreach (var item in dic)
            {
                keyList.Add(item.Key);
            }
            foreach (var item in keyList)
            {
                if (dic[item] != null && RegularHelper.Check(dic[item].ToString(), RegularHelper.Type.时间戳))
                {
                    dic[item] = GetDateTime(dic[item].ToString());
                }
            }
        }
        #endregion
    }
}
