using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x02000284 RID: 644
	public class Report
	{
		// Token: 0x06001F67 RID: 8039 RVA: 0x0009ABE8 File Offset: 0x00098DE8
		public Report(CompilerContext context, ReportPrinter printer)
		{
			if (context == null)
			{
				throw new ArgumentNullException("settings");
			}
			if (printer == null)
			{
				throw new ArgumentNullException("printer");
			}
			this.settings = context.Settings;
			this.printer = printer;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0009AC35 File Offset: 0x00098E35
		public void DisableReporting()
		{
			this.reporting_disabled++;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0009AC45 File Offset: 0x00098E45
		public void EnableReporting()
		{
			this.reporting_disabled--;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0009AC58 File Offset: 0x00098E58
		public void FeatureIsNotAvailable(CompilerContext compiler, Location loc, string feature)
		{
			string arg;
			switch (compiler.Settings.Version)
			{
			case LanguageVersion.ISO_1:
				arg = "1.0";
				break;
			case LanguageVersion.ISO_2:
				arg = "2.0";
				break;
			case LanguageVersion.V_3:
				arg = "3.0";
				break;
			case LanguageVersion.V_4:
				arg = "4.0";
				break;
			case LanguageVersion.V_5:
				arg = "5.0";
				break;
			case LanguageVersion.V_6:
				arg = "6.0";
				break;
			default:
				throw new InternalErrorException("Invalid feature version", new object[]
				{
					compiler.Settings.Version
				});
			}
			this.Error(1644, loc, "Feature `{0}' cannot be used because it is not part of the C# {1} language specification", feature, arg);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0009ACFA File Offset: 0x00098EFA
		public void FeatureIsNotSupported(Location loc, string feature)
		{
			this.Error(1644, loc, "Feature `{0}' is not supported in Mono mcs1 compiler. Consider using the `gmcs' compiler instead", feature);
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0009AD0E File Offset: 0x00098F0E
		public void RuntimeMissingSupport(Location loc, string feature)
		{
			this.Error(-88, loc, "Your .NET Runtime does not support `{0}'. Please use the latest Mono runtime instead.", feature);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0009AD1F File Offset: 0x00098F1F
		public void SymbolRelatedToPreviousError(Location loc, string symbol)
		{
			this.SymbolRelatedToPreviousError(loc.ToString());
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x0009AD34 File Offset: 0x00098F34
		public void SymbolRelatedToPreviousError(MemberSpec ms)
		{
			if (this.reporting_disabled > 0 || !this.printer.HasRelatedSymbolSupport)
			{
				return;
			}
			MemberCore memberCore = ms.MemberDefinition as MemberCore;
			while (ms is ElementTypeSpec)
			{
				ms = ((ElementTypeSpec)ms).Element;
				memberCore = (ms.MemberDefinition as MemberCore);
			}
			if (memberCore != null)
			{
				this.SymbolRelatedToPreviousError(memberCore);
				return;
			}
			if (ms.DeclaringType != null)
			{
				ms = ms.DeclaringType;
			}
			ImportedTypeDefinition importedTypeDefinition = ms.MemberDefinition as ImportedTypeDefinition;
			if (importedTypeDefinition != null)
			{
				ImportedAssemblyDefinition importedAssemblyDefinition = importedTypeDefinition.DeclaringAssembly as ImportedAssemblyDefinition;
				this.SymbolRelatedToPreviousError(importedAssemblyDefinition.Location);
			}
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0009ADC9 File Offset: 0x00098FC9
		public void SymbolRelatedToPreviousError(MemberCore mc)
		{
			this.SymbolRelatedToPreviousError(mc.Location, mc.GetSignatureForError());
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0009ADE0 File Offset: 0x00098FE0
		public void SymbolRelatedToPreviousError(string loc)
		{
			string item = string.Format("{0} (Location of the symbol related to previous ", loc);
			if (this.extra_information.Contains(item))
			{
				return;
			}
			this.extra_information.Add(item);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0009AE14 File Offset: 0x00099014
		public bool CheckWarningCode(int code, Location loc)
		{
			if (Report.AllWarningsHashSet == null)
			{
				Report.AllWarningsHashSet = new HashSet<int>(Report.AllWarnings);
			}
			if (Report.AllWarningsHashSet.Contains(code))
			{
				return true;
			}
			this.Warning(1691, 1, loc, "`{0}' is not a valid warning number", new object[]
			{
				code
			});
			return false;
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x0009AE68 File Offset: 0x00099068
		public void ExtraInformation(Location loc, string msg)
		{
			this.extra_information.Add(string.Format("{0} {1}", loc, msg));
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0009AE88 File Offset: 0x00099088
		public WarningRegions RegisterWarningRegion(Location location)
		{
			WarningRegions warningRegions;
			if (this.warning_regions_table == null)
			{
				warningRegions = null;
				this.warning_regions_table = new Dictionary<int, WarningRegions>();
			}
			else
			{
				this.warning_regions_table.TryGetValue(location.File, out warningRegions);
			}
			if (warningRegions == null)
			{
				warningRegions = new WarningRegions();
				this.warning_regions_table.Add(location.File, warningRegions);
			}
			return warningRegions;
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0009AEE0 File Offset: 0x000990E0
		public void Warning(int code, int level, Location loc, string message)
		{
			if (this.reporting_disabled > 0)
			{
				return;
			}
			if (!this.settings.IsWarningEnabled(code, level))
			{
				return;
			}
			WarningRegions warningRegions;
			if (this.warning_regions_table != null && !loc.IsNull && this.warning_regions_table.TryGetValue(loc.File, out warningRegions) && !warningRegions.IsWarningEnabled(code, loc.Row))
			{
				return;
			}
			AbstractMessage msg;
			if (this.settings.IsWarningAsError(code))
			{
				message = "Warning as Error: " + message;
				msg = new ErrorMessage(code, loc, message, this.extra_information);
			}
			else
			{
				msg = new WarningMessage(code, loc, message, this.extra_information);
			}
			this.extra_information.Clear();
			this.printer.Print(msg, this.settings.ShowFullPaths);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0009AF9F File Offset: 0x0009919F
		public void Warning(int code, int level, Location loc, string format, string arg)
		{
			this.Warning(code, level, loc, string.Format(format, arg));
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0009AFB3 File Offset: 0x000991B3
		public void Warning(int code, int level, Location loc, string format, string arg1, string arg2)
		{
			this.Warning(code, level, loc, string.Format(format, arg1, arg2));
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0009AFC9 File Offset: 0x000991C9
		public void Warning(int code, int level, Location loc, string format, params object[] args)
		{
			this.Warning(code, level, loc, string.Format(format, args));
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0009AFDD File Offset: 0x000991DD
		public void Warning(int code, int level, string message)
		{
			this.Warning(code, level, Location.Null, message);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0009AFED File Offset: 0x000991ED
		public void Warning(int code, int level, string format, string arg)
		{
			this.Warning(code, level, Location.Null, format, arg);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0009AFFF File Offset: 0x000991FF
		public void Warning(int code, int level, string format, string arg1, string arg2)
		{
			this.Warning(code, level, Location.Null, format, arg1, arg2);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0009B013 File Offset: 0x00099213
		public void Warning(int code, int level, string format, params string[] args)
		{
			this.Warning(code, level, Location.Null, string.Format(format, args));
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x0009B02A File Offset: 0x0009922A
		public int Warnings
		{
			get
			{
				return this.printer.WarningsCount;
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0009B038 File Offset: 0x00099238
		public void Error(int code, Location loc, string error)
		{
			if (this.reporting_disabled > 0)
			{
				return;
			}
			ErrorMessage errorMessage = new ErrorMessage(code, loc, error, this.extra_information);
			this.extra_information.Clear();
			this.printer.Print(errorMessage, this.settings.ShowFullPaths);
			if (this.settings.Stacktrace)
			{
				Console.WriteLine(Report.FriendlyStackTrace(new StackTrace(true)));
			}
			if (this.printer.ErrorsCount == this.settings.FatalCounter)
			{
				throw new FatalException(errorMessage.Text);
			}
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x0009B0C1 File Offset: 0x000992C1
		public void Error(int code, Location loc, string format, string arg)
		{
			this.Error(code, loc, string.Format(format, arg));
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0009B0D3 File Offset: 0x000992D3
		public void Error(int code, Location loc, string format, string arg1, string arg2)
		{
			this.Error(code, loc, string.Format(format, arg1, arg2));
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x0009B0E7 File Offset: 0x000992E7
		public void Error(int code, Location loc, string format, params string[] args)
		{
			this.Error(code, loc, string.Format(format, args));
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x0009B0F9 File Offset: 0x000992F9
		public void Error(int code, string error)
		{
			this.Error(code, Location.Null, error);
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x0009B108 File Offset: 0x00099308
		public void Error(int code, string format, string arg)
		{
			this.Error(code, Location.Null, format, arg);
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0009B118 File Offset: 0x00099318
		public void Error(int code, string format, string arg1, string arg2)
		{
			this.Error(code, Location.Null, format, arg1, arg2);
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x0009B12A File Offset: 0x0009932A
		public void Error(int code, string format, params string[] args)
		{
			this.Error(code, Location.Null, string.Format(format, args));
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x0009B13F File Offset: 0x0009933F
		public int Errors
		{
			get
			{
				return this.printer.ErrorsCount;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x0009B14C File Offset: 0x0009934C
		public bool IsDisabled
		{
			get
			{
				return this.reporting_disabled > 0;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x0009B157 File Offset: 0x00099357
		public ReportPrinter Printer
		{
			get
			{
				return this.printer;
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x0009B15F File Offset: 0x0009935F
		public ReportPrinter SetPrinter(ReportPrinter printer)
		{
			ReportPrinter result = this.printer;
			this.printer = printer;
			return result;
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0000AF70 File Offset: 0x00009170
		[Conditional("MCS_DEBUG")]
		public static void Debug(string message, params object[] args)
		{
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0009B170 File Offset: 0x00099370
		[Conditional("MCS_DEBUG")]
		public static void Debug(int category, string message, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder(message);
			if (args != null && args.Length != 0)
			{
				stringBuilder.Append(": ");
				bool flag = true;
				foreach (object obj in args)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					if (obj == null)
					{
						stringBuilder.Append("null");
					}
					else
					{
						stringBuilder.Append(obj);
					}
				}
			}
			Console.WriteLine(stringBuilder.ToString());
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0009B1E8 File Offset: 0x000993E8
		private static string FriendlyStackTrace(StackTrace t)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			for (int i = 0; i < t.FrameCount; i++)
			{
				StackFrame frame = t.GetFrame(i);
				MethodBase method = frame.GetMethod();
				if (flag || method.ReflectedType != typeof(Report))
				{
					flag = true;
					stringBuilder.Append("\tin ");
					if (frame.GetFileLineNumber() > 0)
					{
						stringBuilder.AppendFormat("(at {0}:{1}) ", frame.GetFileName(), frame.GetFileLineNumber());
					}
					stringBuilder.AppendFormat("{0}.{1} (", method.ReflectedType.Name, method.Name);
					bool flag2 = true;
					foreach (ParameterInfo parameterInfo in method.GetParameters())
					{
						if (!flag2)
						{
							stringBuilder.Append(", ");
						}
						flag2 = false;
						stringBuilder.Append(parameterInfo.ParameterType.FullName);
					}
					stringBuilder.Append(")\n");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0009B2EE File Offset: 0x000994EE
		// Note: this type is marked as 'beforefieldinit'.
		static Report()
		{
		}

		// Token: 0x04000B87 RID: 2951
		public const int RuntimeErrorId = 10000;

		// Token: 0x04000B88 RID: 2952
		private Dictionary<int, WarningRegions> warning_regions_table;

		// Token: 0x04000B89 RID: 2953
		private ReportPrinter printer;

		// Token: 0x04000B8A RID: 2954
		private int reporting_disabled;

		// Token: 0x04000B8B RID: 2955
		private readonly CompilerSettings settings;

		// Token: 0x04000B8C RID: 2956
		private List<string> extra_information = new List<string>();

		// Token: 0x04000B8D RID: 2957
		public static readonly int[] AllWarnings = new int[]
		{
			28,
			67,
			78,
			105,
			108,
			109,
			114,
			162,
			164,
			168,
			169,
			183,
			184,
			197,
			219,
			251,
			252,
			253,
			278,
			282,
			402,
			414,
			419,
			420,
			429,
			436,
			437,
			440,
			458,
			464,
			465,
			467,
			469,
			472,
			473,
			612,
			618,
			626,
			628,
			642,
			649,
			652,
			657,
			658,
			659,
			660,
			661,
			665,
			672,
			675,
			693,
			728,
			809,
			824,
			1030,
			1058,
			1060,
			1066,
			1522,
			1570,
			1571,
			1572,
			1573,
			1574,
			1580,
			1581,
			1584,
			1587,
			1589,
			1590,
			1591,
			1592,
			1607,
			1616,
			1633,
			1634,
			1635,
			1685,
			1690,
			1691,
			1692,
			1695,
			1696,
			1697,
			1699,
			1700,
			1701,
			1702,
			1709,
			1711,
			1717,
			1718,
			1720,
			1735,
			1901,
			1956,
			1981,
			1998,
			2002,
			2023,
			2029,
			3000,
			3001,
			3002,
			3003,
			3005,
			3006,
			3007,
			3008,
			3009,
			3010,
			3011,
			3012,
			3013,
			3014,
			3015,
			3016,
			3017,
			3018,
			3019,
			3021,
			3022,
			3023,
			3024,
			3026,
			3027,
			4014,
			4024,
			4025,
			4026,
			7035,
			7080,
			7081,
			7082,
			7095,
			8009,
			8094
		};

		// Token: 0x04000B8E RID: 2958
		private static HashSet<int> AllWarningsHashSet;
	}
}
