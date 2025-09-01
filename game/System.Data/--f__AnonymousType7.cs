using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000009 RID: 9
[CompilerGenerated]
internal sealed class <>f__AnonymousType7<<OperationId>j__TPar, <Operation>j__TPar, <IsolationLevel>j__TPar, <Connection>j__TPar, <Timestamp>j__TPar>
{
	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000044 RID: 68 RVA: 0x00003255 File Offset: 0x00001455
	public <OperationId>j__TPar OperationId
	{
		get
		{
			return this.<OperationId>i__Field;
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000045 RID: 69 RVA: 0x0000325D File Offset: 0x0000145D
	public <Operation>j__TPar Operation
	{
		get
		{
			return this.<Operation>i__Field;
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000046 RID: 70 RVA: 0x00003265 File Offset: 0x00001465
	public <IsolationLevel>j__TPar IsolationLevel
	{
		get
		{
			return this.<IsolationLevel>i__Field;
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000047 RID: 71 RVA: 0x0000326D File Offset: 0x0000146D
	public <Connection>j__TPar Connection
	{
		get
		{
			return this.<Connection>i__Field;
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000048 RID: 72 RVA: 0x00003275 File Offset: 0x00001475
	public <Timestamp>j__TPar Timestamp
	{
		get
		{
			return this.<Timestamp>i__Field;
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x0000327D File Offset: 0x0000147D
	[DebuggerHidden]
	public <>f__AnonymousType7(<OperationId>j__TPar OperationId, <Operation>j__TPar Operation, <IsolationLevel>j__TPar IsolationLevel, <Connection>j__TPar Connection, <Timestamp>j__TPar Timestamp)
	{
		this.<OperationId>i__Field = OperationId;
		this.<Operation>i__Field = Operation;
		this.<IsolationLevel>i__Field = IsolationLevel;
		this.<Connection>i__Field = Connection;
		this.<Timestamp>i__Field = Timestamp;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000032AC File Offset: 0x000014AC
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType7<<OperationId>j__TPar, <Operation>j__TPar, <IsolationLevel>j__TPar, <Connection>j__TPar, <Timestamp>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<OperationId>j__TPar>.Default.Equals(this.<OperationId>i__Field, <>f__AnonymousType.<OperationId>i__Field) && EqualityComparer<<Operation>j__TPar>.Default.Equals(this.<Operation>i__Field, <>f__AnonymousType.<Operation>i__Field) && EqualityComparer<<IsolationLevel>j__TPar>.Default.Equals(this.<IsolationLevel>i__Field, <>f__AnonymousType.<IsolationLevel>i__Field) && EqualityComparer<<Connection>j__TPar>.Default.Equals(this.<Connection>i__Field, <>f__AnonymousType.<Connection>i__Field) && EqualityComparer<<Timestamp>j__TPar>.Default.Equals(this.<Timestamp>i__Field, <>f__AnonymousType.<Timestamp>i__Field);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x0000333C File Offset: 0x0000153C
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return ((((-1281697549 * -1521134295 + EqualityComparer<<OperationId>j__TPar>.Default.GetHashCode(this.<OperationId>i__Field)) * -1521134295 + EqualityComparer<<Operation>j__TPar>.Default.GetHashCode(this.<Operation>i__Field)) * -1521134295 + EqualityComparer<<IsolationLevel>j__TPar>.Default.GetHashCode(this.<IsolationLevel>i__Field)) * -1521134295 + EqualityComparer<<Connection>j__TPar>.Default.GetHashCode(this.<Connection>i__Field)) * -1521134295 + EqualityComparer<<Timestamp>j__TPar>.Default.GetHashCode(this.<Timestamp>i__Field);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000033C4 File Offset: 0x000015C4
	[DebuggerHidden]
	[return: Nullable(1)]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ OperationId = {0}, Operation = {1}, IsolationLevel = {2}, Connection = {3}, Timestamp = {4} }}";
		object[] array = new object[5];
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
		<Timestamp>j__TPar <Timestamp>j__TPar = this.<Timestamp>i__Field;
		array[num5] = ((<Timestamp>j__TPar != null) ? <Timestamp>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x04000028 RID: 40
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <OperationId>j__TPar <OperationId>i__Field;

	// Token: 0x04000029 RID: 41
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Operation>j__TPar <Operation>i__Field;

	// Token: 0x0400002A RID: 42
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <IsolationLevel>j__TPar <IsolationLevel>i__Field;

	// Token: 0x0400002B RID: 43
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Connection>j__TPar <Connection>i__Field;

	// Token: 0x0400002C RID: 44
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Timestamp>j__TPar <Timestamp>i__Field;
}
