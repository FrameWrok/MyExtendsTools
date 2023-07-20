using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace SmartJSFrameWork.WebSite.App_Code
{
    public class RequestFilter : Stream
    {
        //Temporary buffer to accumulate page reads
        private MemoryStream ms;

        //Handle to original output stream pipeline
        private Stream _stream;

        //Encoding of the response
        private Encoding _encoding;

        //The callback function
        private Func<string, string> _callback;

        /// <summary>
        /// String based callback filter
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <param name="callback"></param>
        public RequestFilter(Stream stream, Encoding encoding, Func<string, string> callback)
        {
            _stream = stream;
            _encoding = encoding;
            _callback = callback;
        }

        public override void Flush()
        {
            ms.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return ms.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            ms.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            /*
            if (ms == null)
            {
                //I should read stream, execute callback, fill ms.
                var sr = new StreamReader(_stream, _encoding);
                string content = sr.ReadToEnd();

                //Perform the content replacement routine
                content = _callback(content);

                Byte[] bytes = _encoding.GetBytes(content);
                ms = new MemoryStream();
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
            }
            else
            {
                //just curious
                var sr = new StreamReader(_stream, _encoding);
                string content = sr.ReadToEnd();
            }

            return ms.Read(buffer, offset, count);
            */

            int len = _stream.Read(buffer, offset, count);
            if (len == 0)
            {
                Array.Clear(buffer, 0, count);
                return len;
            }
            System.Text.Encoding curEncoding = HttpContext.Current.Request.ContentEncoding;
            string strBuff = curEncoding.GetString(buffer);             //__VIEWSTATE 和 __EVENTVALIDATION 由 ASP.NET 使用，它们分别置于头部和尾部，所以不用考虑。
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"&/w+=([/w@/-.*+]+)&");
            strBuff = re.Replace(strBuff, new System.Text.RegularExpressions.MatchEvaluator(ReplaceMatch));
            Array.Clear(buffer, 0, count);
            byte[] newBuff = curEncoding.GetBytes(strBuff);
            newBuff.CopyTo(buffer, 0);
            return len;
        }

        private string ReplaceMatch(System.Text.RegularExpressions.Match match)
        {
            string prefixStr = match.ToString().Split('=')[0];
            string subMatchStr = HttpContext.Current.Server.UrlDecode(match.Groups[1].ToString());
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"string");
            subMatchStr = HttpContext.Current.Server.UrlEncode(re.Replace(subMatchStr, "ABCDEF"));//注意：替换前后的字符数编码后必须保持一致
            return prefixStr + "=" + subMatchStr + "&";
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { return ms.Length; }
        }

        public override long Position
        {
            get { return ms.Position; }
            set { throw new NotSupportedException(); }
        }
    }
}