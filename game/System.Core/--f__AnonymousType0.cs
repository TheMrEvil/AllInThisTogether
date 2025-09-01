using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000002 RID: 2
[CompilerGenerated]
internal sealed class <>f__AnonymousType0<<Info>j__TPar, <GenType>j__TPar>
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public <Info>j__TPar Info
	{
		get
		{
			return this.<Info>i__Field;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	public <GenType>j__TPar GenType
	{
		get
		{
			return this.<GenType>i__Field;
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
	[DebuggerHidden]
	public <>f__AnonymousType0(<Info>j__TPar Info, <GenType>j__TPar GenType)
	{
		this.<Info>i__Field = Info;
		this.<GenType>i__Field = GenType;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002078 File Offset: 0x00000278
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType0<<Info>j__TPar, <GenType>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<Info>j__TPar>.Default.Equals(this.<Info>i__Field, <>f__AnonymousType.<Info>i__Field) && EqualityComparer<<GenType>j__TPar>.Default.Equals(this.<GenType>i__Field, <>f__AnonymousType.<GenType>i__Field);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020BF File Offset: 0x000002BF
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (1943597344 * -1521134295 + EqualityComparer<<Info>j__TPar>.Default.GetHashCode(this.<Info>i__Field)) * -1521134295 + EqualityComparer<<GenType>j__TPar>.Default.GetHashCode(this.<GenType>i__Field);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000020F4 File Offset: 0x000002F4
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ Info = {0}, GenType = {1} }}";
		object[] array = new object[2];
		int num = 0;
		<Info>j__TPar <Info>j__TPar = this.<Info>i__Field;
		array[num] = ((<Info>j__TPar != null) ? <Info>j__TPar.ToString() : null);
		int num2 = 1;
		<GenType>j__TPar <GenType>j__TPar = this.<GenType>i__Field;
		array[num2] = ((<GenType>j__TPar != null) ? <GenType>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x04000001 RID: 1
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Info>j__TPar <Info>i__Field;

	// Token: 0x04000002 RID: 2
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <GenType>j__TPar <GenType>i__Field;
}
