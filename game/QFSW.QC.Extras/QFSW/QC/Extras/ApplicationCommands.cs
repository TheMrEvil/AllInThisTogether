using System;
using UnityEngine;

namespace QFSW.QC.Extras
{
	// Token: 0x02000008 RID: 8
	public static class ApplicationCommands
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002E21 File Offset: 0x00001021
		[Command("quit", "Quits the player application", Platform.AllPlatforms, MonoTargetType.Single)]
		[CommandPlatform(~(Platform.OSXEditor | Platform.WindowsEditor | Platform.LinuxEditor | Platform.WebGLPlayer))]
		private static void Quit()
		{
			Application.Quit();
		}
	}
}
