using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class WorldNumberNode : NumberNode
{
	// Token: 0x06001D9E RID: 7582 RVA: 0x000B3E74 File Offset: 0x000B2074
	public override float Evaluate(EffectProperties props)
	{
		switch (this.Stat)
		{
		case WorldNumberNode.WorldNumStat.PlayerCount:
		{
			Room currentRoom = PhotonNetwork.CurrentRoom;
			return (float)((currentRoom != null) ? currentRoom.PlayerCount : 1);
		}
		case WorldNumberNode.WorldNumStat.ChapterNum:
			return (float)(WaveManager.CurrentWave + 1);
		case WorldNumberNode.WorldNumStat.EnemyCount:
			return (float)AIManager.AliveEnemies;
		case WorldNumberNode.WorldNumStat.BindingLevel:
			return (float)GameplayManager.BindingLevel;
		case WorldNumberNode.WorldNumStat.ElapsedTime:
		{
			GameplayManager instance = GameplayManager.instance;
			if (instance == null)
			{
				return 0f;
			}
			return instance.GameTime;
		}
		case WorldNumberNode.WorldNumStat.AppendixLevel:
		{
			WaveManager instance2 = WaveManager.instance;
			return (float)((instance2 != null) ? instance2.AppendixLevel : 0);
		}
		case WorldNumberNode.WorldNumStat.AppendixChapterNum:
		{
			WaveManager instance3 = WaveManager.instance;
			return (float)((instance3 != null) ? instance3.AppendixChapterNumber : 0);
		}
		case WorldNumberNode.WorldNumStat.MusicBarTime:
			return AudioManager.TimeToNextBar();
		case WorldNumberNode.WorldNumStat.MusicBeatLength:
			return AudioManager.CurrentBeatLength();
		case WorldNumberNode.WorldNumStat.CurrentEnemyScalingBase:
			return AIManager.instance.Waves.EnemyScaling.BaseScaling();
		case WorldNumberNode.WorldNumStat.CurrentBindingScaling:
			return AIManager.instance.Waves.EnemyScaling.BindingHPScaling();
		case WorldNumberNode.WorldNumStat.AlivePlayerCount:
		{
			int num = 0;
			using (List<PlayerControl>.Enumerator enumerator = PlayerControl.AllPlayers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsDead)
					{
						num++;
					}
				}
			}
			return (float)num;
		}
		default:
			return 0f;
		}
	}

	// Token: 0x06001D9F RID: 7583 RVA: 0x000B3FB0 File Offset: 0x000B21B0
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "World Value",
			MinInspectorSize = new Vector2(150f, 0f),
			MaxInspectorSize = new Vector2(150f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001DA0 RID: 7584 RVA: 0x000B3FFE File Offset: 0x000B21FE
	public WorldNumberNode()
	{
	}

	// Token: 0x04001E51 RID: 7761
	public WorldNumberNode.WorldNumStat Stat;

	// Token: 0x02000687 RID: 1671
	public enum WorldNumStat
	{
		// Token: 0x04002BF4 RID: 11252
		PlayerCount,
		// Token: 0x04002BF5 RID: 11253
		ChapterNum,
		// Token: 0x04002BF6 RID: 11254
		EnemyCount,
		// Token: 0x04002BF7 RID: 11255
		BindingLevel,
		// Token: 0x04002BF8 RID: 11256
		ElapsedTime,
		// Token: 0x04002BF9 RID: 11257
		AppendixLevel,
		// Token: 0x04002BFA RID: 11258
		AppendixChapterNum,
		// Token: 0x04002BFB RID: 11259
		MusicBarTime,
		// Token: 0x04002BFC RID: 11260
		MusicBeatLength,
		// Token: 0x04002BFD RID: 11261
		CurrentEnemyScalingBase,
		// Token: 0x04002BFE RID: 11262
		CurrentBindingScaling,
		// Token: 0x04002BFF RID: 11263
		AlivePlayerCount
	}
}
