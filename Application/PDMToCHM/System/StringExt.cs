using System;

namespace System
{
	public static class StringExt
	{
		public static string FormatString(this string s, params object[] args)
		{
			return string.Format(s, args);
		}
	}
}
