using System;

namespace UnityEngine.Playables
{
	// Token: 0x0200044C RID: 1100
	public static class ScriptPlayableBinding
	{
		// Token: 0x060026EA RID: 9962 RVA: 0x0004100C File Offset: 0x0003F20C
		public static PlayableBinding Create(string name, Object key, Type type)
		{
			return PlayableBinding.CreateInternal(name, key, type, new PlayableBinding.CreateOutputMethod(ScriptPlayableBinding.CreateScriptOutput));
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x00041034 File Offset: 0x0003F234
		private static PlayableOutput CreateScriptOutput(PlayableGraph graph, string name)
		{
			return ScriptPlayableOutput.Create(graph, name);
		}
	}
}
