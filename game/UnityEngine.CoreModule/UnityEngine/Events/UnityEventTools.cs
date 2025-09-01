using System;

namespace UnityEngine.Events
{
	// Token: 0x020002B6 RID: 694
	internal class UnityEventTools
	{
		// Token: 0x06001D1E RID: 7454 RVA: 0x0002E9E8 File Offset: 0x0002CBE8
		internal static string TidyAssemblyTypeName(string assemblyTypeName)
		{
			bool flag = string.IsNullOrEmpty(assemblyTypeName);
			string result;
			if (flag)
			{
				result = assemblyTypeName;
			}
			else
			{
				int num = int.MaxValue;
				int num2 = assemblyTypeName.IndexOf(", Version=");
				bool flag2 = num2 != -1;
				if (flag2)
				{
					num = Math.Min(num2, num);
				}
				num2 = assemblyTypeName.IndexOf(", Culture=");
				bool flag3 = num2 != -1;
				if (flag3)
				{
					num = Math.Min(num2, num);
				}
				num2 = assemblyTypeName.IndexOf(", PublicKeyToken=");
				bool flag4 = num2 != -1;
				if (flag4)
				{
					num = Math.Min(num2, num);
				}
				bool flag5 = num != int.MaxValue;
				if (flag5)
				{
					assemblyTypeName = assemblyTypeName.Substring(0, num);
				}
				num2 = assemblyTypeName.IndexOf(", UnityEngine.");
				bool flag6 = num2 != -1 && assemblyTypeName.EndsWith("Module");
				if (flag6)
				{
					assemblyTypeName = assemblyTypeName.Substring(0, num2) + ", UnityEngine";
				}
				result = assemblyTypeName;
			}
			return result;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x00002072 File Offset: 0x00000272
		public UnityEventTools()
		{
		}
	}
}
