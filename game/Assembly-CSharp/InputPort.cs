using System;

// Token: 0x0200028D RID: 653
[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class InputPort : Attribute
{
	// Token: 0x06001930 RID: 6448 RVA: 0x0009D217 File Offset: 0x0009B417
	public InputPort(Type AcceptedType, bool AcceptsMultiple = false, string titleOverride = null, PortLocation portLocation = PortLocation.Default)
	{
		this.AcceptedType = AcceptedType;
		this.AcceptsMultiple = AcceptsMultiple;
		this.titleOverride = titleOverride;
		this.portLocation = portLocation;
	}

	// Token: 0x04001949 RID: 6473
	public Type AcceptedType;

	// Token: 0x0400194A RID: 6474
	public bool AcceptsMultiple;

	// Token: 0x0400194B RID: 6475
	public string titleOverride;

	// Token: 0x0400194C RID: 6476
	public PortLocation portLocation;
}
