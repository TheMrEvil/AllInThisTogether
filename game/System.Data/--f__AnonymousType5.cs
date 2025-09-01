using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000007 RID: 7
[CompilerGenerated]
internal sealed class <>f__AnonymousType5<<OperationId>j__TPar, <Operation>j__TPar, <ConnectionId>j__TPar, <Connection>j__TPar, <Exception>j__TPar, <Timestamp>j__TPar>
{
	// Token: 0x1700001B RID: 27
	// (get) Token: 0x0600002F RID: 47 RVA: 0x00002C68 File Offset: 0x00000E68
	public <OperationId>j__TPar OperationId
	{
		get
		{
			return this.<OperationId>i__Field;
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000030 RID: 48 RVA: 0x00002C70 File Offset: 0x00000E70
	public <Operation>j__TPar Operation
	{
		get
		{
			return this.<Operation>i__Field;
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000031 RID: 49 RVA: 0x00002C78 File Offset: 0x00000E78
	public <ConnectionId>j__TPar ConnectionId
	{
		get
		{
			return this.<ConnectionId>i__Field;
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x06000032 RID: 50 RVA: 0x00002C80 File Offset: 0x00000E80
	public <Connection>j__TPar Connection
	{
		get
		{
			return this.<Connection>i__Field;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x06000033 RID: 51 RVA: 0x00002C88 File Offset: 0x00000E88
	public <Exception>j__TPar Exception
	{
		get
		{
			return this.<Exception>i__Field;
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000034 RID: 52 RVA: 0x00002C90 File Offset: 0x00000E90
	public <Timestamp>j__TPar Timestamp
	{
		get
		{
			return this.<Timestamp>i__Field;
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002C98 File Offset: 0x00000E98
	[DebuggerHidden]
	public <>f__AnonymousType5(<OperationId>j__TPar OperationId, <Operation>j__TPar Operation, <ConnectionId>j__TPar ConnectionId, <Connection>j__TPar Connection, <Exception>j__TPar Exception, <Timestamp>j__TPar Timestamp)
	{
		this.<OperationId>i__Field = OperationId;
		this.<Operation>i__Field = Operation;
		this.<ConnectionId>i__Field = ConnectionId;
		this.<Connection>i__Field = Connection;
		this.<Exception>i__Field = Exception;
		this.<Timestamp>i__Field = Timestamp;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002CD0 File Offset: 0x00000ED0
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType5<<OperationId>j__TPar, <Operation>j__TPar, <ConnectionId>j__TPar, <Connection>j__TPar, <Exception>j__TPar, <Timestamp>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<OperationId>j__TPar>.Default.Equals(this.<OperationId>i__Field, <>f__AnonymousType.<OperationId>i__Field) && EqualityComparer<<Operation>j__TPar>.Default.Equals(this.<Operation>i__Field, <>f__AnonymousType.<Operation>i__Field) && EqualityComparer<<ConnectionId>j__TPar>.Default.Equals(this.<ConnectionId>i__Field, <>f__AnonymousType.<ConnectionId>i__Field) && EqualityComparer<<Connection>j__TPar>.Default.Equals(this.<Connection>i__Field, <>f__AnonymousType.<Connection>i__Field) && EqualityComparer<<Exception>j__TPar>.Default.Equals(this.<Exception>i__Field, <>f__AnonymousType.<Exception>i__Field) && EqualityComparer<<Timestamp>j__TPar>.Default.Equals(this.<Timestamp>i__Field, <>f__AnonymousType.<Timestamp>i__Field);
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002D7C File Offset: 0x00000F7C
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (((((-183852027 * -1521134295 + EqualityComparer<<OperationId>j__TPar>.Default.GetHashCode(this.<OperationId>i__Field)) * -1521134295 + EqualityComparer<<Operation>j__TPar>.Default.GetHashCode(this.<Operation>i__Field)) * -1521134295 + EqualityComparer<<ConnectionId>j__TPar>.Default.GetHashCode(this.<ConnectionId>i__Field)) * -1521134295 + EqualityComparer<<Connection>j__TPar>.Default.GetHashCode(this.<Connection>i__Field)) * -1521134295 + EqualityComparer<<Exception>j__TPar>.Default.GetHashCode(this.<Exception>i__Field)) * -1521134295 + EqualityComparer<<Timestamp>j__TPar>.Default.GetHashCode(this.<Timestamp>i__Field);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002E18 File Offset: 0x00001018
	[DebuggerHidden]
	[return: Nullable(1)]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ OperationId = {0}, Operation = {1}, ConnectionId = {2}, Connection = {3}, Exception = {4}, Timestamp = {5} }}";
		object[] array = new object[6];
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

	// Token: 0x0400001B RID: 27
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <OperationId>j__TPar <OperationId>i__Field;

	// Token: 0x0400001C RID: 28
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Operation>j__TPar <Operation>i__Field;

	// Token: 0x0400001D RID: 29
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <ConnectionId>j__TPar <ConnectionId>i__Field;

	// Token: 0x0400001E RID: 30
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Connection>j__TPar <Connection>i__Field;

	// Token: 0x0400001F RID: 31
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Exception>j__TPar <Exception>i__Field;

	// Token: 0x04000020 RID: 32
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Timestamp>j__TPar <Timestamp>i__Field;
}
