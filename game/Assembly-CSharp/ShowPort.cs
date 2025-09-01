using System;

// Token: 0x0200028F RID: 655
[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class ShowPort : Attribute
{
	// Token: 0x06001932 RID: 6450 RVA: 0x0009D261 File Offset: 0x0009B461
	public ShowPort(string MethodName)
	{
		this.test = MethodName;
	}

	// Token: 0x04001951 RID: 6481
	public string test;
}
