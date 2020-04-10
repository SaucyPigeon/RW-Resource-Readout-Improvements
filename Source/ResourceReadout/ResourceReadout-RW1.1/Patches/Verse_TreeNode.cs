using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;

namespace ResourceReadout.Patches
{
//#if DEBUG
//	[HarmonyPatch(typeof(TreeNode))]
//	public static class Verse_TreeNode
//	{
//		[HarmonyPatch(nameof(TreeNode.SetOpen))]
//		[HarmonyPrefix]
//		public static void Prefix(int mask, bool val, int ___openBits)
//		{
//			Log.Message("TreeNode.SetOpen_Prefix (start)");
//			Log.Message($"Value of mask parameter (b2): {Convert.ToString(mask, 2)}");
//			Log.Message($"Value of val parameter: {val}");
//			Log.Message($"Value of openBits field (b2): {Convert.ToString(___openBits, 2)}");
//			Log.Message("TreeNode.SetOpen_Prefix (end)");
//		}

//		[HarmonyPatch(nameof(TreeNode.SetOpen))]
//		[HarmonyPostfix]
//		public static void Postfix(int mask, bool val, int ___openBits)
//		{
//			Log.Message("TreeNode.SetOpen_Postfix (start)");
//			Log.Message($"Value of mask parameter (b2): {Convert.ToString(mask, 2)}");
//			Log.Message($"Value of val parameter: {val}");
//			Log.Message($"Value of openBits field (b2): {Convert.ToString(___openBits, 2)}");
//			Log.Message("TreeNode.SetOpen_Postfix (end)");
//		}
//	}
//#endif
}
