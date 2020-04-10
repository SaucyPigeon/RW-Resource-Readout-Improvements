using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;
using RimWorld;

namespace ResourceReadout.Patches
{
	[HarmonyPatch(typeof(Listing_ResourceReadout))]
	public static class RimWorld_Listing_ResourceReadout
	{
#if DEBUG
		[HarmonyPatch(nameof(Listing_ResourceReadout.DoCategory))]
		[HarmonyPrefix]
		public static void Prefix()
		{
			Log.Message("RimWorld.Listing_ResourceReadout_Prefix");
		}
#endif

		[HarmonyPatch(nameof(Listing_ResourceReadout.DoCategory))]
		[HarmonyPostfix]
		public static void Postfix(TreeNode_ThingCategory node, int openMask)
		{
//#if DEBUG
//			Log.Message("RimWorld.Listing_ResourceReadout_Postfix (start)");
//#endif

			if (Loader.NodeOpenTracker == null)
			{
				Loader.NodeOpenTracker = new NodeOpenTracker();
			}

			Loader.NodeOpenTracker.SetNode(node, openMask);

//#if DEBUG
//			Log.Message("RimWorld.Listing_ResourceReadout_Postfix (finish)");
//#endif
		}
	}
}
