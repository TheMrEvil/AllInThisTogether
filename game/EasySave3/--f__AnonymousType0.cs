using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000002 RID: 2
[CompilerGenerated]
internal sealed class <>f__AnonymousType0<<assembly>j__TPar, <type>j__TPar>
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public <assembly>j__TPar assembly
	{
		get
		{
			return this.<assembly>i__Field;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	public <type>j__TPar type
	{
		get
		{
			return this.<type>i__Field;
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
	[DebuggerHidden]
	public <>f__AnonymousType0(<assembly>j__TPar assembly, <type>j__TPar type)
	{
		this.<assembly>i__Field = assembly;
		this.<type>i__Field = type;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002078 File Offset: 0x00000278
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType0<<assembly>j__TPar, <type>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<assembly>j__TPar>.Default.Equals(this.<assembly>i__Field, <>f__AnonymousType.<assembly>i__Field) && EqualityComparer<<type>j__TPar>.Default.Equals(this.<type>i__Field, <>f__AnonymousType.<type>i__Field);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020BF File Offset: 0x000002BF
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (237921948 * -1521134295 + EqualityComparer<<assembly>j__TPar>.Default.GetHashCode(this.<assembly>i__Field)) * -1521134295 + EqualityComparer<<type>j__TPar>.Default.GetHashCode(this.<type>i__Field);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000020F4 File Offset: 0x000002F4
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ assembly = {0}, type = {1} }}";
		object[] array = new object[2];
		int num = 0;
		<assembly>j__TPar <assembly>j__TPar = this.<assembly>i__Field;
		array[num] = ((<assembly>j__TPar != null) ? <assembly>j__TPar.ToString() : null);
		int num2 = 1;
		<type>j__TPar <type>j__TPar = this.<type>i__Field;
		array[num2] = ((<type>j__TPar != null) ? <type>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x04000001 RID: 1
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <assembly>j__TPar <assembly>i__Field;

	// Token: 0x04000002 RID: 2
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <type>j__TPar <type>i__Field;
}
