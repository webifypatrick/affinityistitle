using System;

namespace Com.VerySimple.Phreeze
{
    public enum PreparerEscapeType
    {
        Sql = 0,
        Serialization = 1
    }

    /// <summary>
    /// Prepares various strings for sql inserts and/or serialization
    /// </summary>
    public static class Preparer
    {
		/// <summary>
		/// Encodes a string for inserting into log4j, removes line breaks and encodes html brackets
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string Encode4Log(string val)
		{
			return val.Replace("\r", "").Replace("\n", "").Replace("<", "&lt;").Replace(">", "&gt;");
		}

        /// <summary>
        /// returns a string suitable for sql
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Escape(string val, PreparerEscapeType escapeType)
        {
            if (val == null)
            {
                val = "";
            }

            if (escapeType == PreparerEscapeType.Sql)
            {
                // for sql we can't have single quotes
                val = val.Replace("'", "''");
            }
            else
            {
                // for serialization we can't have vertical bar
                val.Replace("|", "!");
            }

            return val;
        }

		public static string Escape(string val)
		{
			return Escape(val, PreparerEscapeType.Sql);
		}

		public static string Escape(bool val)
		{
			return Escape(val ? "1" : "0", PreparerEscapeType.Sql);
		}

		/// <summary>
        /// returns an int as string suitable for sql
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Escape(int val, PreparerEscapeType escapeType)
        {
            return Escape(val.ToString(), escapeType);
        }

        public static string Escape(int val)
        {
            return Escape(val, PreparerEscapeType.Sql);
        }

        /// <summary>
        /// returns a decimal as string suitable for sql
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Escape(decimal val, PreparerEscapeType escapeType)
        {
            return Escape(val.ToString(), escapeType);
        }

        public static string Escape(decimal val)
        {
            return Escape(val, PreparerEscapeType.Sql);
        }

        /// <summary>
        /// returns DateTime as a string suitable for sql
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Escape(System.DateTime val, PreparerEscapeType escapeType)
        {
            return Escape(val.Year + "-" + val.Month + "-" + val.Day + " " + val.Hour + ":" + val.Minute + ":" + val.Second, escapeType);
        }

        public static string Escape(System.DateTime val)
        {
            return Escape(val, PreparerEscapeType.Sql);
        }


        /// <summary>
        /// Returns a string value, if null returns an empty string
        /// </summary>
        /// <param name="val"></param>
        /// <returns>(string)val or ""</returns>
        public static string SafeString(object val)
        {
            return (val != null && val != DBNull.Value) ? Convert.ToString(val) : "";
        }

		/// <summary>
		/// Returns a string value, if null returns an empty string
		/// </summary>
		/// <param name="val"></param>
		/// <returns>(string)val or ""</returns>
		public static bool SafeBool(object val)
		{
			return (val != null && val != DBNull.Value && (Convert.ToString(val).Equals("1") || Convert.ToString(val).ToLower().Equals("true")));
		}

        /// <summary>
        /// returns an int value, if null returns 0
        /// </summary>
        /// <param name="val">value to convert</param>
        /// <returns>(int)val or 0</returns>
        public static int SafeInt(object val)
        {
            return (val != null && val != DBNull.Value && (!val.Equals(""))) ? (int)Convert.ToInt32(val) : 0;
        }

        /// <summary>
        /// returns a decimal value, if null returns 0
        /// </summary>
        /// <param name="val">value to convert</param>
        /// <returns>(decimal)val or 0</returns>
        public static decimal SafeDecimal(object val)
        {
            return (val != null && val != DBNull.Value && (!val.Equals(""))) ? Convert.ToDecimal(val) : 0;
        }

        /// <summary>
        /// returns a DateTime value, if null returns 9/9/1999
        /// </summary>
        /// <param name="val">value to convert</param>
        /// <returns>(DateTime)val or 9/19/1999</returns>
        public static System.DateTime SafeDateTime(object val)
        {
            return (val != null && val != DBNull.Value && (!val.Equals(""))) ? Convert.ToDateTime(val) : System.Convert.ToDateTime("9/9/1999");
        }

        /// <summary>
        /// returns a bool value, if null returns false
        /// </summary>
        /// <param name="val">value to convert</param>
        /// <returns>(decimal)val or false</returns>
        public static bool SafeBoolean(object val)
        {
            return (val != null && val != DBNull.Value && (!val.Equals(""))) ? Convert.ToBoolean(val) : false;
        }


        /// <summary>
        /// Returns string that is safe to insert into xml
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string EscapeXml(string val)
        {
            return val.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }
    }
}