using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;

namespace ResourceReadout.Patches
{
#if DEBUG
	[HarmonyPatch(typeof(TreeNode))]
	public static class Verse_TreeNode
	{
		[HarmonyPatch(nameof(TreeNode.SetOpen))]
		[HarmonyPostfix]
		public static void Postfix(int mask, bool val, int __openBits)
		{
			Log.Message("TreeNode.SetOpen_Postfix (start)");
			Log.Message($"Value of mask parameter: {mask}");
			Log.Message($"Value of val parameter: {val}");
			Log.Message($"Value of openBits field: {__openBits}");
			Log.Message("TreeNode.SetOpen_Postfix (end)");
		}
	}
#endif
}
