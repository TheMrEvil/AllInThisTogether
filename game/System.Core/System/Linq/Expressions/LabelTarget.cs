using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Used to represent the target of a <see cref="T:System.Linq.Expressions.GotoExpression" />.</summary>
	// Token: 0x0200026A RID: 618
	public sealed class LabelTarget
	{
		// Token: 0x06001203 RID: 4611 RVA: 0x0003AE70 File Offset: 0x00039070
		internal LabelTarget(Type type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Gets the name of the label.</summary>
		/// <returns>The name of the label.</returns>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0003AE86 File Offset: 0x00039086
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		/// <summary>The type of value that is passed when jumping to the label (or <see cref="T:System.Void" /> if no value should be passed).</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the type of the value that is passed when jumping to the label or <see cref="T:System.Void" /> if no value should be passed</returns>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x0003AE8E File Offset: 0x0003908E
		public Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06001206 RID: 4614 RVA: 0x0003AE96 File Offset: 0x00039096
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			return "UnamedLabel";
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0000235B File Offset: 0x0000055B
		internal LabelTarget()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A0B RID: 2571
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;

		// Token: 0x04000A0C RID: 2572
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;
	}
}
