using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000003 RID: 3
[CompilerGenerated]
internal sealed class <>f__AnonymousType1<<pair>j__TPar, <valueString>j__TPar>
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000005 RID: 5 RVA: 0x0000206D File Offset: 0x0000026D
	public <pair>j__TPar pair
	{
		get
		{
			return this.<pair>i__Field;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000006 RID: 6 RVA: 0x00002075 File Offset: 0x00000275
	public <valueString>j__TPar valueString
	{
		get
		{
			return this.<valueString>i__Field;
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000207D File Offset: 0x0000027D
	[DebuggerHidden]
	public <>f__AnonymousType1(<pair>j__TPar pair, <valueString>j__TPar valueString)
	{
		this.<pair>i__Field = pair;
		this.<valueString>i__Field = valueString;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002094 File Offset: 0x00000294
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType1<<pair>j__TPar, <valueString>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<pair>j__TPar>.Default.Equals(this.<pair>i__Field, <>f__AnonymousType.<pair>i__Field) && EqualityComparer<<valueString>j__TPar>.Default.Equals(this.<valueString>i__Field, <>f__AnonymousType.<valueString>i__Field);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000020DB File Offset: 0x000002DB
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (163789914 * -1521134295 + EqualityComparer<<pair>j__TPar>.Default.GetHashCode(this.<pair>i__Field)) * -1521134295 + EqualityComparer<<valueString>j__TPar>.Default.GetHashCode(this.<valueString>i__Field);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002110 File Offset: 0x00000310
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ pair = {0}, valueString = {1} }}";
		object[] array = new object[2];
		int num = 0;
		<pair>j__TPar <pair>j__TPar = this.<pair>i__Field;
		array[num] = ((<pair>j__TPar != null) ? <pair>j__TPar.ToString() : null);
		int num2 = 1;
		<valueString>j__TPar <valueString>j__TPar = this.<valueString>i__Field;
		array[num2] = ((<valueString>j__TPar != null) ? <valueString>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x04000001 RID: 1
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <pair>j__TPar <pair>i__Field;

	// Token: 0x04000002 RID: 2
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <valueString>j__TPar <valueString>i__Field;
}
