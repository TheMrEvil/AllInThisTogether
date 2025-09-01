using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x020002CF RID: 719
	public class UnitySurrogateSelector : ISurrogateSelector
	{
		// Token: 0x06001DD5 RID: 7637 RVA: 0x000308D4 File Offset: 0x0002EAD4
		public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			bool isGenericType = type.IsGenericType;
			if (isGenericType)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				bool flag = genericTypeDefinition == typeof(List<>);
				if (flag)
				{
					selector = this;
					return ListSerializationSurrogate.Default;
				}
				bool flag2 = genericTypeDefinition == typeof(Dictionary<, >);
				if (flag2)
				{
					selector = this;
					Type type2 = typeof(DictionarySerializationSurrogate<, >).MakeGenericType(type.GetGenericArguments());
					return (ISerializationSurrogate)Activator.CreateInstance(type2);
				}
			}
			selector = null;
			return null;
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00016174 File Offset: 0x00014374
		public void ChainSelector(ISurrogateSelector selector)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00016174 File Offset: 0x00014374
		public ISurrogateSelector GetNextSelector()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00002072 File Offset: 0x00000272
		public UnitySurrogateSelector()
		{
		}
	}
}
