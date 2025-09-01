using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020001D5 RID: 469
	internal class PropertyPatternMember
	{
		// Token: 0x06001897 RID: 6295 RVA: 0x00077184 File Offset: 0x00075384
		public PropertyPatternMember(string name, Expression expr, Location loc)
		{
			this.Name = name;
			this.Expr = expr;
			this.Location = loc;
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x000771A1 File Offset: 0x000753A1
		// (set) Token: 0x06001899 RID: 6297 RVA: 0x000771A9 File Offset: 0x000753A9
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x000771B2 File Offset: 0x000753B2
		// (set) Token: 0x0600189B RID: 6299 RVA: 0x000771BA File Offset: 0x000753BA
		public Expression Expr
		{
			[CompilerGenerated]
			get
			{
				return this.<Expr>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Expr>k__BackingField = value;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x000771C3 File Offset: 0x000753C3
		// (set) Token: 0x0600189D RID: 6301 RVA: 0x000771CB File Offset: 0x000753CB
		public Location Location
		{
			[CompilerGenerated]
			get
			{
				return this.<Location>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Location>k__BackingField = value;
			}
		}

		// Token: 0x040009AC RID: 2476
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x040009AD RID: 2477
		[CompilerGenerated]
		private Expression <Expr>k__BackingField;

		// Token: 0x040009AE RID: 2478
		[CompilerGenerated]
		private Location <Location>k__BackingField;
	}
}
