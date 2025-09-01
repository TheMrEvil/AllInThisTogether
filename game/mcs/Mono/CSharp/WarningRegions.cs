using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000290 RID: 656
	public class WarningRegions
	{
		// Token: 0x06001FC6 RID: 8134 RVA: 0x0009BDC4 File Offset: 0x00099FC4
		public void WarningDisable(int line)
		{
			this.regions.Add(new WarningRegions.DisableAll(line));
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0009BDD7 File Offset: 0x00099FD7
		public void WarningDisable(Location location, int code, Report Report)
		{
			if (Report.CheckWarningCode(code, location))
			{
				this.regions.Add(new WarningRegions.Disable(location.Row, code));
			}
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0009BDFB File Offset: 0x00099FFB
		public void WarningEnable(int line)
		{
			this.regions.Add(new WarningRegions.EnableAll(line));
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0009BE10 File Offset: 0x0009A010
		public void WarningEnable(Location location, int code, CompilerContext context)
		{
			if (!context.Report.CheckWarningCode(code, location))
			{
				return;
			}
			if (context.Settings.IsWarningDisabledGlobally(code))
			{
				context.Report.Warning(1635, 1, location, "Cannot restore warning `CS{0:0000}' because it was disabled globally", new object[]
				{
					code
				});
			}
			this.regions.Add(new WarningRegions.Enable(location.Row, code));
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x0009BE7C File Offset: 0x0009A07C
		public bool IsWarningEnabled(int code, int src_line)
		{
			bool flag = true;
			foreach (WarningRegions.PragmaCmd pragmaCmd in this.regions)
			{
				if (src_line < pragmaCmd.Line)
				{
					break;
				}
				flag = pragmaCmd.IsEnabled(code, flag);
			}
			return flag;
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x0009BEE0 File Offset: 0x0009A0E0
		public WarningRegions()
		{
		}

		// Token: 0x04000B9F RID: 2975
		private List<WarningRegions.PragmaCmd> regions = new List<WarningRegions.PragmaCmd>();

		// Token: 0x020003E8 RID: 1000
		private abstract class PragmaCmd
		{
			// Token: 0x060027CB RID: 10187 RVA: 0x000BD0EF File Offset: 0x000BB2EF
			protected PragmaCmd(int line)
			{
				this.Line = line;
			}

			// Token: 0x060027CC RID: 10188
			public abstract bool IsEnabled(int code, bool previous);

			// Token: 0x04001122 RID: 4386
			public int Line;
		}

		// Token: 0x020003E9 RID: 1001
		private class Disable : WarningRegions.PragmaCmd
		{
			// Token: 0x060027CD RID: 10189 RVA: 0x000BD0FE File Offset: 0x000BB2FE
			public Disable(int line, int code) : base(line)
			{
				this.code = code;
			}

			// Token: 0x060027CE RID: 10190 RVA: 0x000BD10E File Offset: 0x000BB30E
			public override bool IsEnabled(int code, bool previous)
			{
				return this.code != code && previous;
			}

			// Token: 0x04001123 RID: 4387
			private int code;
		}

		// Token: 0x020003EA RID: 1002
		private class DisableAll : WarningRegions.PragmaCmd
		{
			// Token: 0x060027CF RID: 10191 RVA: 0x000BD11E File Offset: 0x000BB31E
			public DisableAll(int line) : base(line)
			{
			}

			// Token: 0x060027D0 RID: 10192 RVA: 0x000022F4 File Offset: 0x000004F4
			public override bool IsEnabled(int code, bool previous)
			{
				return false;
			}
		}

		// Token: 0x020003EB RID: 1003
		private class Enable : WarningRegions.PragmaCmd
		{
			// Token: 0x060027D1 RID: 10193 RVA: 0x000BD127 File Offset: 0x000BB327
			public Enable(int line, int code) : base(line)
			{
				this.code = code;
			}

			// Token: 0x060027D2 RID: 10194 RVA: 0x000BD137 File Offset: 0x000BB337
			public override bool IsEnabled(int code, bool previous)
			{
				return this.code == code || previous;
			}

			// Token: 0x04001124 RID: 4388
			private int code;
		}

		// Token: 0x020003EC RID: 1004
		private class EnableAll : WarningRegions.PragmaCmd
		{
			// Token: 0x060027D3 RID: 10195 RVA: 0x000BD11E File Offset: 0x000BB31E
			public EnableAll(int line) : base(line)
			{
			}

			// Token: 0x060027D4 RID: 10196 RVA: 0x0000212D File Offset: 0x0000032D
			public override bool IsEnabled(int code, bool previous)
			{
				return true;
			}
		}
	}
}
