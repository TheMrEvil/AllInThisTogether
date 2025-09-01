using System;
using System.IO;

namespace Mono.CSharp
{
	// Token: 0x0200028C RID: 652
	public class ConsoleReportPrinter : StreamReportPrinter
	{
		// Token: 0x06001FB2 RID: 8114 RVA: 0x0009B870 File Offset: 0x00099A70
		static ConsoleReportPrinter()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("TERM");
			bool flag = false;
			if (!(environmentVariable == "xterm") && !(environmentVariable == "rxvt") && !(environmentVariable == "rxvt-unicode"))
			{
				if (environmentVariable == "xterm-color" || environmentVariable == "xterm-256color")
				{
					flag = true;
				}
			}
			else if (Environment.GetEnvironmentVariable("COLORTERM") != null)
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			if (!UnixUtils.isatty(1) || !UnixUtils.isatty(2))
			{
				return;
			}
			string text = Environment.GetEnvironmentVariable("MCS_COLORS");
			if (text == null)
			{
				text = "errors=red";
			}
			if (text == "disable")
			{
				return;
			}
			if (!text.StartsWith("errors="))
			{
				return;
			}
			text = text.Substring(7);
			int num = text.IndexOf(",");
			if (num == -1)
			{
				ConsoleReportPrinter.prefix = ConsoleReportPrinter.GetForeground(text);
			}
			else
			{
				ConsoleReportPrinter.prefix = ConsoleReportPrinter.GetBackground(text.Substring(num + 1)) + ConsoleReportPrinter.GetForeground(text.Substring(0, num));
			}
			ConsoleReportPrinter.postfix = "\u001b[0m";
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x0009B978 File Offset: 0x00099B78
		public ConsoleReportPrinter() : base(Console.Error)
		{
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x0009B985 File Offset: 0x00099B85
		public ConsoleReportPrinter(TextWriter writer) : base(writer)
		{
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x0009B990 File Offset: 0x00099B90
		private static int NameToCode(string s)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(s);
			if (num <= 1231115066U)
			{
				if (num <= 96429129U)
				{
					if (num != 18738364U)
					{
						if (num == 96429129U)
						{
							if (s == "yellow")
							{
								return 3;
							}
						}
					}
					else if (s == "green")
					{
						return 2;
					}
				}
				else if (num != 1089765596U)
				{
					if (num == 1231115066U)
					{
						if (s == "cyan")
						{
							return 6;
						}
					}
				}
				else if (s == "red")
				{
					return 1;
				}
			}
			else if (num <= 1676028392U)
			{
				if (num != 1452231588U)
				{
					if (num == 1676028392U)
					{
						if (s == "magenta")
						{
							return 5;
						}
					}
				}
				else if (s == "black")
				{
					return 0;
				}
			}
			else
			{
				if (num != 2197550541U)
				{
					if (num != 2995788198U)
					{
						if (num != 3724674918U)
						{
							return 7;
						}
						if (!(s == "white"))
						{
							return 7;
						}
					}
					else if (!(s == "grey"))
					{
						return 7;
					}
					return 7;
				}
				if (s == "blue")
				{
					return 4;
				}
			}
			return 7;
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0009BAC0 File Offset: 0x00099CC0
		private static string GetForeground(string s)
		{
			string str;
			if (s.StartsWith("bright"))
			{
				str = "1;";
				s = s.Substring(6);
			}
			else
			{
				str = "";
			}
			return "\u001b[" + str + (30 + ConsoleReportPrinter.NameToCode(s)).ToString() + "m";
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0009BB14 File Offset: 0x00099D14
		private static string GetBackground(string s)
		{
			return "\u001b[" + (40 + ConsoleReportPrinter.NameToCode(s)).ToString() + "m";
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0009BB41 File Offset: 0x00099D41
		protected override string FormatText(string txt)
		{
			if (ConsoleReportPrinter.prefix != null)
			{
				return ConsoleReportPrinter.prefix + txt + ConsoleReportPrinter.postfix;
			}
			return txt;
		}

		// Token: 0x04000B9B RID: 2971
		private static readonly string prefix;

		// Token: 0x04000B9C RID: 2972
		private static readonly string postfix;
	}
}
