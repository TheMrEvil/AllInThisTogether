using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000010 RID: 16
	internal static class SerializationProtocolFactory
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00007788 File Offset: 0x00005988
		internal static IProtocol Create(SerializationProtocol serializationProtocol)
		{
			IProtocol result;
			if (serializationProtocol != SerializationProtocol.GpBinaryV18)
			{
				result = new Protocol16();
			}
			else
			{
				result = new Protocol18();
			}
			return result;
		}
	}
}
