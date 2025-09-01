using System;
using System.IO;
using Mono.CSharp.Linq;

namespace Mono.CSharp
{
	// Token: 0x0200019C RID: 412
	public class CompilerCallableEntryPoint : MarshalByRefObject
	{
		// Token: 0x0600161D RID: 5661 RVA: 0x0006AC8C File Offset: 0x00068E8C
		public static bool InvokeCompiler(string[] args, TextWriter error)
		{
			bool result;
			try
			{
				CompilerSettings compilerSettings = new CommandLineParser(error).ParseArguments(args);
				if (compilerSettings == null)
				{
					result = false;
				}
				else
				{
					result = new Driver(new CompilerContext(compilerSettings, new StreamReportPrinter(error))).Compile();
				}
			}
			finally
			{
				CompilerCallableEntryPoint.Reset();
			}
			return result;
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0006ACE0 File Offset: 0x00068EE0
		public static int[] AllWarningNumbers
		{
			get
			{
				return Report.AllWarnings;
			}
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0006ACE7 File Offset: 0x00068EE7
		public static void Reset()
		{
			CompilerCallableEntryPoint.Reset(true);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0006ACEF File Offset: 0x00068EEF
		public static void PartialReset()
		{
			CompilerCallableEntryPoint.Reset(false);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0006ACF7 File Offset: 0x00068EF7
		public static void Reset(bool full_flag)
		{
			Location.Reset();
			if (!full_flag)
			{
				return;
			}
			QueryBlock.TransparentParameter.Reset();
			TypeInfo.Reset();
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0006AD0C File Offset: 0x00068F0C
		public CompilerCallableEntryPoint()
		{
		}
	}
}
