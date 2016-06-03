using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Common
{
    public class Security
    {
        public static string CreateVerification(int length)
        {
            char[] _verification = new char[length];
            char[] _dictionary =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
                'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
                'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };
            Random _random = new Random();
            for(int i=0; i < length; i++)
            {
                _verification[i] = _dictionary[_random.Next(_dictionary.Length - 1)];
            }
            return new string(_verification);
        }

        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="text">验证码字符串</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片长度</param>
        /// <returns></returns>
        public static Bitmap CreateVerificationImage(string text, int width, int height)
        {
            Pen _pen = new Pen(Color.Black);
            Font _font = new Font("Arial",14,FontStyle.Bold);
            Brush _brush = null;
            Bitmap _bitmap = new Bitmap(width,height);
            Graphics _g = Graphics.FromImage(_bitmap);
            SizeF _totalSizeF = _g.MeasureString(text, _font);
            SizeF _curCharSizeF;
            PointF _startPointF = new PointF((width-_totalSizeF.Width)/2,(height-_totalSizeF.Height)/2);

            Random _random = new Random();
            _g.Clear(Color.White);
            for (int i = 0; i < text.Length; i++)
            {
                _brush = new LinearGradientBrush(new Point(0,0),new Point(1,1),Color.FromArgb(_random.Next(255),_random.Next(255), _random.Next(255)), Color.FromArgb(_random.Next(255), _random.Next(255), _random.Next(255)));
                _g.DrawString(text[i].ToString(),_font,_brush,_startPointF);
                _curCharSizeF = _g.MeasureString(text[i].ToString(), _font);
                _startPointF.X += _curCharSizeF.Width;
            }

            _g.Dispose();
            return _bitmap;
        }

        /// <summary>
        /// 256位散列加密
        /// </summary>
        /// <param name="text">明文</param>
        /// <returns>密文</returns>
        public static string Sha256(string text)
        {
            SHA256Managed _sha256 = new SHA256Managed();
            byte[] _text = _sha256.ComputeHash(Encoding.Default.GetBytes(text));
            return Convert.ToBase64String(_text);
        }
    }
}
