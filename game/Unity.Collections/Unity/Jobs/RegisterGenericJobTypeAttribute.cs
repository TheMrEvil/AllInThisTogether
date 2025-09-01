using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.Jobs
{
	// Token: 0x0200001B RID: 27
	[MovedFrom(true, "Unity.Entities", "Unity.Entities", null)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class RegisterGenericJobTypeAttribute : Attribute
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002843 File Offset: 0x00000A43
		public RegisterGenericJobTypeAttribute(Type type)
		{
			this.ConcreteType = type;
		}

		// Token: 0x0400000A RID: 10
		public Type ConcreteType;
	}
}
