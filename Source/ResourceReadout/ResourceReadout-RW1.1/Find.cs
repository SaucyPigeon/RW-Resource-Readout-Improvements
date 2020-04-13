using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;
using System.Reflection;

namespace ResourceReadout
{
	public static class Find
	{
		private static FieldInfo fi_ResourceReadout = AccessTools.Field(typeof(MapInterface), "resourceReadout");
		private static FieldInfo fi_RootThingCategories = AccessTools.Field(typeof(RimWorld.ResourceReadout), "RootThingCategories");

		public static RimWorld.ResourceReadout ResourceReadout
		{
			get
			{
				if (Verse.Find.MapUI == null)
					throw new ArgumentException(nameof(Verse.Find.MapUI));
				if (fi_ResourceReadout == null)
					throw new ArgumentException(nameof(fi_ResourceReadout));

				return (RimWorld.ResourceReadout)fi_ResourceReadout.GetValue(Verse.Find.MapUI);
			}
		}

		public static List<ThingCategoryDef> GetRootThingCategories(this RimWorld.ResourceReadout resourceReadout)
		{
			return (List<ThingCategoryDef>)fi_RootThingCategories.GetValue(resourceReadout);
		}
	}
}
