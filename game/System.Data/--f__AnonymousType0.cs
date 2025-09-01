using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000002 RID: 2
[CompilerGenerated]
internal sealed class <>f__AnonymousType0<<OperationId>j__TPar, <Operation>j__TPar, <ConnectionId>j__TPar, <Command>j__TPar>
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public <OperationId>j__TPar OperationId
	{
		get
		{
			return this.<OperationId>i__Field;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	public <Operation>j__TPar Operation
	{
		get
		{
			return this.<Operation>i__Field;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
	public <ConnectionId>j__TPar ConnectionId
	{
		get
		{
			return this.<ConnectionId>i__Field;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
	public <Command>j__TPar Command
	{
		get
		{
			return this.<Command>i__Field;
		}
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
	[DebuggerHidden]
	public <>f__AnonymousType0(<OperationId>j__TPar OperationId, <Operation>j__TPar Operation, <ConnectionId>j__TPar ConnectionId, <Command>j__TPar Command)
	{
		this.<OperationId>i__Field = OperationId;
		this.<Operation>i__Field = Operation;
		this.<ConnectionId>i__Field = ConnectionId;
		this.<Command>i__Field = Command;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002098 File Offset: 0x00000298
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType0<<OperationId>j__TPar, <Operation>j__TPar, <ConnectionId>j__TPar, <Command>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<OperationId>j__TPar>.Default.Equals(this.<OperationId>i__Field, <>f__AnonymousType.<OperationId>i__Field) && EqualityComparer<<Operation>j__TPar>.Default.Equals(this.<Operation>i__Field, <>f__AnonymousType.<Operation>i__Field) && EqualityComparer<<ConnectionId>j__TPar>.Default.Equals(this.<ConnectionId>i__Field, <>f__AnonymousType.<ConnectionId>i__Field) && EqualityComparer<<Command>j__TPar>.Default.Equals(this.<Command>i__Field, <>f__AnonymousType.<Command>i__Field);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002110 File Offset: 0x00000310
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (((1443849695 * -1521134295 + EqualityComparer<<OperationId>j__TPar>.Default.GetHashCode(this.<OperationId>i__Field)) * -1521134295 + EqualityComparer<<Operation>j__TPar>.Default.GetHashCode(this.<Operation>i__Field)) * -1521134295 + EqualityComparer<<ConnectionId>j__TPar>.Default.GetHashCode(this.<ConnectionId>i__Field)) * -1521134295 + EqualityComparer<<Command>j__TPar>.Default.GetHashCode(this.<Command>i__Field);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002180 File Offset: 0x00000380
	[DebuggerHidden]
	[return: Nullable(1)]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ OperationId = {0}, Operation = {1}, ConnectionId = {2}, Command = {3} }}";
		object[] array = new object[4];
		int num = 0;
		<OperationId>j__TPar <OperationId>j__TPar = this.<OperationId>i__Field;
		array[num] = ((<OperationId>j__TPar != null) ? <OperationId>j__TPar.ToString() : null);
		int num2 = 1;
		<Operation>j__TPar <Operation>j__TPar = this.<Operation>i__Field;
		array[num2] = ((<Operation>j__TPar != null) ? <Operation>j__TPar.ToString() : null);
		int num3 = 2;
		<ConnectionId>j__TPar <ConnectionId>j__TPar = this.<ConnectionId>i__Field;
		array[num3] = ((<ConnectionId>j__TPar != null) ? <ConnectionId>j__TPar.ToString() : null);
		int num4 = 3;
		<Command>j__TPar <Command>j__TPar = this.<Command>i__Field;
		array[num4] = ((<Command>j__TPar != null) ? <Command>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x04000001 RID: 1
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <OperationId>j__TPar <OperationId>i__Field;

	// Token: 0x04000002 RID: 2
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Operation>j__TPar <Operation>i__Field;

	// Token: 0x04000003 RID: 3
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <ConnectionId>j__TPar <ConnectionId>i__Field;

	// Token: 0x04000004 RID: 4
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Command>j__TPar <Command>i__Field;
}
