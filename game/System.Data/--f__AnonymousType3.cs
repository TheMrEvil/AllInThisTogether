using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000005 RID: 5
[CompilerGenerated]
internal sealed class <>f__AnonymousType3<<OperationId>j__TPar, <Operation>j__TPar, <Connection>j__TPar, <Timestamp>j__TPar>
{
	// Token: 0x17000011 RID: 17
	// (get) Token: 0x0600001D RID: 29 RVA: 0x000027BC File Offset: 0x000009BC
	public <OperationId>j__TPar OperationId
	{
		get
		{
			return this.<OperationId>i__Field;
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x0600001E RID: 30 RVA: 0x000027C4 File Offset: 0x000009C4
	public <Operation>j__TPar Operation
	{
		get
		{
			return this.<Operation>i__Field;
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x0600001F RID: 31 RVA: 0x000027CC File Offset: 0x000009CC
	public <Connection>j__TPar Connection
	{
		get
		{
			return this.<Connection>i__Field;
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000020 RID: 32 RVA: 0x000027D4 File Offset: 0x000009D4
	public <Timestamp>j__TPar Timestamp
	{
		get
		{
			return this.<Timestamp>i__Field;
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000027DC File Offset: 0x000009DC
	[DebuggerHidden]
	public <>f__AnonymousType3(<OperationId>j__TPar OperationId, <Operation>j__TPar Operation, <Connection>j__TPar Connection, <Timestamp>j__TPar Timestamp)
	{
		this.<OperationId>i__Field = OperationId;
		this.<Operation>i__Field = Operation;
		this.<Connection>i__Field = Connection;
		this.<Timestamp>i__Field = Timestamp;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002804 File Offset: 0x00000A04
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType3<<OperationId>j__TPar, <Operation>j__TPar, <Connection>j__TPar, <Timestamp>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<OperationId>j__TPar>.Default.Equals(this.<OperationId>i__Field, <>f__AnonymousType.<OperationId>i__Field) && EqualityComparer<<Operation>j__TPar>.Default.Equals(this.<Operation>i__Field, <>f__AnonymousType.<Operation>i__Field) && EqualityComparer<<Connection>j__TPar>.Default.Equals(this.<Connection>i__Field, <>f__AnonymousType.<Connection>i__Field) && EqualityComparer<<Timestamp>j__TPar>.Default.Equals(this.<Timestamp>i__Field, <>f__AnonymousType.<Timestamp>i__Field);
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000287C File Offset: 0x00000A7C
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (((1824925885 * -1521134295 + EqualityComparer<<OperationId>j__TPar>.Default.GetHashCode(this.<OperationId>i__Field)) * -1521134295 + EqualityComparer<<Operation>j__TPar>.Default.GetHashCode(this.<Operation>i__Field)) * -1521134295 + EqualityComparer<<Connection>j__TPar>.Default.GetHashCode(this.<Connection>i__Field)) * -1521134295 + EqualityComparer<<Timestamp>j__TPar>.Default.GetHashCode(this.<Timestamp>i__Field);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000028EC File Offset: 0x00000AEC
	[DebuggerHidden]
	[return: Nullable(1)]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ OperationId = {0}, Operation = {1}, Connection = {2}, Timestamp = {3} }}";
		object[] array = new object[4];
		int num = 0;
		<OperationId>j__TPar <OperationId>j__TPar = this.<OperationId>i__Field;
		array[num] = ((<OperationId>j__TPar != null) ? <OperationId>j__TPar.ToString() : null);
		int num2 = 1;
		<Operation>j__TPar <Operation>j__TPar = this.<Operation>i__Field;
		array[num2] = ((<Operation>j__TPar != null) ? <Operation>j__TPar.ToString() : null);
		int num3 = 2;
		<Connection>j__TPar <Connection>j__TPar = this.<Connection>i__Field;
		array[num3] = ((<Connection>j__TPar != null) ? <Connection>j__TPar.ToString() : null);
		int num4 = 3;
		<Timestamp>j__TPar <Timestamp>j__TPar = this.<Timestamp>i__Field;
		array[num4] = ((<Timestamp>j__TPar != null) ? <Timestamp>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x04000011 RID: 17
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <OperationId>j__TPar <OperationId>i__Field;

	// Token: 0x04000012 RID: 18
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Operation>j__TPar <Operation>i__Field;

	// Token: 0x04000013 RID: 19
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Connection>j__TPar <Connection>i__Field;

	// Token: 0x04000014 RID: 20
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Timestamp>j__TPar <Timestamp>i__Field;
}
