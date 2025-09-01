using System;

// Token: 0x0200028E RID: 654
[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class OutputPort : Attribute
{
	// Token: 0x06001931 RID: 6449 RVA: 0x0009D23C File Offset: 0x0009B43C
	public OutputPort(Type AcceptedType, bool AcceptsMultiple = false, string titleOverride = null, PortLocation portLocation = PortLocation.Default)
	{
		this.AcceptedType = AcceptedType;
		this.AcceptsMultiple = AcceptsMultiple;
		this.titleOverride = titleOverride;
		this.portLocation = portLocation;
	}

	// Token: 0x0400194D RID: 6477
	public Type AcceptedType;

	// Token: 0x0400194E RID: 6478
	public bool AcceptsMultiple;

	// Token: 0x0400194F RID: 6479
	public string titleOverride;

	// Token: 0x04001950 RID: 6480
	public PortLocation portLocation;
}
