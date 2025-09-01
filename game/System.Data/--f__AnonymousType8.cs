using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000A RID: 10
[CompilerGenerated]
internal sealed class <>f__AnonymousType8<<OperationId>j__TPar, <Operation>j__TPar, <IsolationLevel>j__TPar, <Connection>j__TPar, <Exception>j__TPar, <Timestamp>j__TPar>
{
	// Token: 0x1700002D RID: 45
	// (get) Token: 0x0600004D RID: 77 RVA: 0x000034AB File Offset: 0x000016AB
	public <OperationId>j__TPar OperationId
	{
		get
		{
			return this.<OperationId>i__Field;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x0600004E RID: 78 RVA: 0x000034B3 File Offset: 0x000016B3
	public <Operation>j__TPar Operation
	{
		get
		{
			return this.<Operation>i__Field;
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x0600004F RID: 79 RVA: 0x000034BB File Offset: 0x000016BB
	public <IsolationLevel>j__TPar IsolationLevel
	{
		get
		{
			return this.<IsolationLevel>i__Field;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x06000050 RID: 80 RVA: 0x000034C3 File Offset: 0x000016C3
	public <Connection>j__TPar Connection
	{
		get
		{
			return this.<Connection>i__Field;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x06000051 RID: 81 RVA: 0x000034CB File Offset: 0x000016CB
	public <Exception>j__TPar Exception
	{
		get
		{
			return this.<Exception>i__Field;
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x06000052 RID: 82 RVA: 0x000034D3 File Offset: 0x000016D3
	public <Timestamp>j__TPar Timestamp
	{
		get
		{
			return this.<Timestamp>i__Field;
		}
	}

	// Token: 0x06000053 RID: 83 RVA: 0x000034DB File Offset: 0x000016DB
	[DebuggerHidden]
	public <>f__AnonymousType8(<OperationId>j__TPar OperationId, <Operation>j__TPar Operation, <IsolationLevel>j__TPar IsolationLevel, <Connection>j__TPar Connection, <Exception>j__TPar Exception, <Timestamp>j__TPar Timestamp)
	{
		this.<OperationId>i__Field = OperationId;
		this.<Operation>i__Field = Operation;
		this.<IsolationLevel>i__Field = IsolationLevel;
		this.<Connection>i__Field = Connection;
		this.<Exception>i__Field = Exception;
		this.<Timestamp>i__Field = Timestamp;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00003510 File Offset: 0x00001710
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType8<<OperationId>j__TPar, <Operation>j__TPar, <IsolationLevel>j__TPar, <Connection>j__TPar, <Exception>j__TPar, <Timestamp>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<OperationId>j__TPar>.Default.Equals(this.<OperationId>i__Field, <>f__AnonymousType.<OperationId>i__Field) && EqualityComparer<<Operation>j__TPar>.Default.Equals(this.<Operation>i__Field, <>f__AnonymousType.<Operation>i__Field) && EqualityComparer<<IsolationLevel>j__TPar>.Default.Equals(this.<IsolationLevel>i__Field, <>f__AnonymousType.<IsolationLevel>i__Field) && EqualityComparer<<Connection>j__TPar>.Default.Equals(this.<Connection>i__Field, <>f__AnonymousType.<Connection>i__Field) && EqualityComparer<<Exception>j__TPar>.Default.Equals(this.<Exception>i__Field, <>f__AnonymousType.<Exception>i__Field) && EqualityComparer<<Timestamp>j__TPar>.Default.Equals(this.<Timestamp>i__Field, <>f__AnonymousType.<Timestamp>i__Field);
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000035BC File Offset: 0x000017BC
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (((((1749298144 * -1521134295 + EqualityComparer<<OperationId>j__TPar>.Default.GetHashCode(this.<OperationId>i__Field)) * -1521134295 + EqualityComparer<<Operation>j__TPar>.Default.GetHashCode(this.<Operation>i__Field)) * -1521134295 + EqualityComparer<<IsolationLevel>j__TPar>.Default.GetHashCode(this.<IsolationLevel>i__Field)) * -1521134295 + EqualityComparer<<Connection>j__TPar>.Default.GetHashCode(this.<Connection>i__Field)) * -1521134295 + EqualityComparer<<Exception>j__TPar>.Default.GetHashCode(this.<Exception>i__Field)) * -1521134295 + EqualityComparer<<Timestamp>j__TPar>.Default.GetHashCode(this.<Timestamp>i__Field);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00003658 File Offset: 0x00001858
	[DebuggerHidden]
	[return: Nullable(1)]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ OperationId = {0}, Operation = {1}, IsolationLevel = {2}, Connection = {3}, Exception = {4}, Timestamp = {5} }}";
		object[] array = new object[6];
		int num = 0;
		<OperationId>j__TPar <OperationId>j__TPar = this.<OperationId>i__Field;
		array[num] = ((<OperationId>j__TPar != null) ? <OperationId>j__TPar.ToString() : null);
		int num2 = 1;
		<Operation>j__TPar <Operation>j__TPar = this.<Operation>i__Field;
		array[num2] = ((<Operation>j__TPar != null) ? <Operation>j__TPar.ToString() : null);
		int num3 = 2;
		<IsolationLevel>j__TPar <IsolationLevel>j__TPar = this.<IsolationLevel>i__Field;
		array[num3] = ((<IsolationLevel>j__TPar != null) ? <IsolationLevel>j__TPar.ToString() : null);
		int num4 = 3;
		<Connection>j__TPar <Connection>j__TPar = this.<Connection>i__Field;
		array[num4] = ((<Connection>j__TPar != null) ? <Connection>j__TPar.ToString() : null);
		int num5 = 4;
		<Exception>j__TPar <Exception>j__TPar = this.<Exception>i__Field;
		array[num5] = ((<Exception>j__TPar != null) ? <Exception>j__TPar.ToString() : null);
		int num6 = 5;
		<Timestamp>j__TPar <Timestamp>j__TPar = this.<Timestamp>i__Field;
		array[num6] = ((<Timestamp>j__TPar != null) ? <Timestamp>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x0400002D RID: 45
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <OperationId>j__TPar <OperationId>i__Field;

	// Token: 0x0400002E RID: 46
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Operation>j__TPar <Operation>i__Field;

	// Token: 0x0400002F RID: 47
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <IsolationLevel>j__TPar <IsolationLevel>i__Field;

	// Token: 0x04000030 RID: 48
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Connection>j__TPar <Connection>i__Field;

	// Token: 0x04000031 RID: 49
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Exception>j__TPar <Exception>i__Field;

	// Token: 0x04000032 RID: 50
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Timestamp>j__TPar <Timestamp>i__Field;
}
