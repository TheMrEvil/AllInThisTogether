using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200010D RID: 269
	[UsedByNativeCode(Name = "ExposedReference")]
	[Serializable]
	public struct ExposedReference<T> where T : Object
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x00008D54 File Offset: 0x00006F54
		public T Resolve(IExposedPropertyTable resolver)
		{
			bool flag = resolver != null;
			if (flag)
			{
				bool flag2;
				Object referenceValue = resolver.GetReferenceValue(this.exposedName, out flag2);
				bool flag3 = flag2;
				if (flag3)
				{
					return referenceValue as T;
				}
			}
			return this.defaultValue as T;
		}

		// Token: 0x04000385 RID: 901
		[SerializeField]
		public PropertyName exposedName;

		// Token: 0x04000386 RID: 902
		[SerializeField]
		public Object defaultValue;
	}
}
