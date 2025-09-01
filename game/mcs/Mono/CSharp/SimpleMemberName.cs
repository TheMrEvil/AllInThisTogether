using System;

namespace Mono.CSharp
{
	// Token: 0x0200018C RID: 396
	public class SimpleMemberName
	{
		// Token: 0x0600155A RID: 5466 RVA: 0x000669FF File Offset: 0x00064BFF
		public SimpleMemberName(string name, Location loc)
		{
			this.Value = name;
			this.Location = loc;
		}

		// Token: 0x04000905 RID: 2309
		public string Value;

		// Token: 0x04000906 RID: 2310
		public Location Location;
	}
}
