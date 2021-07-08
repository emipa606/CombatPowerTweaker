using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RaidValueTweaker
{
    internal static class SettingsHelper
    {
        // Token: 0x06000027 RID: 39 RVA: 0x00003754 File Offset: 0x00001954
        public static void SliderLabeled(this Listing_Standard ls, string label, ref int val, string format,
            float min = 0f, float max = 100f, string tooltip = null)
        {
            float num = val;
            ls.SliderLabeled(label, ref num, format, min, max, tooltip);
            val = (int) num;
        }

        // Token: 0x06000028 RID: 40 RVA: 0x0000377C File Offset: 0x0000197C
        public static void SliderLabeled(this Listing_Standard ls, string label, ref float val, string format,
            float min = 0f, float max = 1f, string tooltip = null)
        {
            var rect = ls.GetRect(Text.LineHeight);
            var rect2 = rect.LeftPart(0.7f).Rounded();
            var rect3 = rect.RightPart(0.3f).Rounded().LeftPart(0.67f).Rounded();
            var rect4 = rect.RightPart(0.1f).Rounded();
            var anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleLeft;
            Widgets.Label(rect2, label);
            var num = Widgets.HorizontalSlider(rect3, val, min, max, true);
            val = num;
            Text.Anchor = TextAnchor.MiddleRight;
            Widgets.Label(rect4, string.Format(format, val));
            if (!tooltip.NullOrEmpty())
            {
                TooltipHandler.TipRegion(rect, tooltip);
            }

            Text.Anchor = anchor;
            ls.Gap(ls.verticalSpacing);
        }

        // Token: 0x06000029 RID: 41 RVA: 0x00003844 File Offset: 0x00001A44
        public static void FloatRange(this Listing_Standard ls, string label, ref FloatRange range, float min = 0f,
            float max = 1f, string tooltip = null, ToStringStyle valueStyle = ToStringStyle.FloatTwo)
        {
            var rect = ls.GetRect(Text.LineHeight);
            var rect2 = rect.LeftPart(0.7f).Rounded();
            var rect3 = rect.RightPart(0.3f).Rounded().LeftPart(0.9f).Rounded();
            rect3.yMin -= 5f;
            var anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleLeft;
            Widgets.Label(rect2, label);
            Text.Anchor = TextAnchor.MiddleRight;
            var hashCode = ls.CurHeight.GetHashCode();
            Widgets.FloatRange(rect3, hashCode, ref range, min, max, null, valueStyle);
            if (!tooltip.NullOrEmpty())
            {
                TooltipHandler.TipRegion(rect, tooltip);
            }

            Text.Anchor = anchor;
            ls.Gap(ls.verticalSpacing);
        }

        // Token: 0x0600002A RID: 42 RVA: 0x00003900 File Offset: 0x00001B00
        public static Rect GetRect(this Listing_Standard listing_Standard, float? height = null)
        {
            return listing_Standard.GetRect(height ?? Text.LineHeight);
        }

        // Token: 0x0600002B RID: 43 RVA: 0x0000392C File Offset: 0x00001B2C
        public static void AddLabeledRadioList(this Listing_Standard listing_Standard, string header, string[] labels,
            ref string val, float? headerHeight = null)
        {
            if (header != string.Empty)
            {
                Widgets.Label(listing_Standard.GetRect(headerHeight), header);
            }

            listing_Standard.AddRadioList(GenerateLabeledRadioValues(labels), ref val);
        }

        // Token: 0x0600002C RID: 44 RVA: 0x0000396C File Offset: 0x00001B6C
        private static void AddRadioList<T>(this Listing_Standard listing_Standard, List<LabeledRadioValue<T>> items,
            ref T val, float? height = null)
        {
            foreach (var labeledRadioValue in items)
            {
                if (Widgets.RadioButtonLabeled(listing_Standard.GetRect(height), labeledRadioValue.Label,
                    EqualityComparer<T>.Default.Equals(labeledRadioValue.Value, val)))
                {
                    val = labeledRadioValue.Value;
                }
            }
        }

        // Token: 0x0600002D RID: 45 RVA: 0x000039EC File Offset: 0x00001BEC
        private static List<LabeledRadioValue<string>> GenerateLabeledRadioValues(string[] labels)
        {
            var list = new List<LabeledRadioValue<string>>();
            foreach (var text in labels)
            {
                list.Add(new LabeledRadioValue<string>(text, text));
            }

            return list;
        }

        // Token: 0x0600002E RID: 46 RVA: 0x00003A24 File Offset: 0x00001C24
        public static void AddLabeledTextField(this Listing_Standard listing_Standard, string label,
            ref string settingsValue, float leftPartPct = 0.5f)
        {
            listing_Standard.LineRectSpilter(out var rect, out var rect2, leftPartPct);
            Widgets.Label(rect, label);
            var text = settingsValue;
            settingsValue = Widgets.TextField(rect2, text);
        }

        // Token: 0x0600002F RID: 47 RVA: 0x00003A60 File Offset: 0x00001C60
        public static void AddLabeledNumericalTextField<T>(this Listing_Standard listing_Standard, string label,
            ref T settingsValue, float leftPartPct = 0.5f, float minValue = 1f, float maxValue = 100000f)
            where T : struct
        {
            listing_Standard.LineRectSpilter(out var rect, out var rect2, leftPartPct);
            Widgets.Label(rect, label);
            var text = settingsValue.ToString();
            Widgets.TextFieldNumeric(rect2, ref settingsValue, ref text, minValue, maxValue);
        }

        // Token: 0x06000030 RID: 48 RVA: 0x00003AA4 File Offset: 0x00001CA4
        public static Rect LineRectSpilter(this Listing_Standard listing_Standard, out Rect leftHalf,
            float leftPartPct = 0.5f, float? height = null)
        {
            var rect = listing_Standard.GetRect(height);
            leftHalf = rect.LeftPart(leftPartPct).Rounded();
            return rect;
        }

        // Token: 0x06000031 RID: 49 RVA: 0x00003ACC File Offset: 0x00001CCC
        public static void LineRectSpilter(this Listing_Standard listing_Standard, out Rect leftHalf,
            out Rect rightHalf, float leftPartPct = 0.5f, float? height = null)
        {
            var rect = listing_Standard.LineRectSpilter(out leftHalf, leftPartPct, height);
            rightHalf = rect.RightPart(1f - leftPartPct).Rounded();
        }

        // Token: 0x02000016 RID: 22
        public class LabeledRadioValue<T>
        {
            // Token: 0x0600008C RID: 140 RVA: 0x000052A7 File Offset: 0x000034A7
            public LabeledRadioValue(string label, T val)
            {
                Label = label;
                Value = val;
            }

            // Token: 0x17000011 RID: 17
            // (get) Token: 0x0600008D RID: 141 RVA: 0x000052BD File Offset: 0x000034BD
            // (set) Token: 0x0600008E RID: 142 RVA: 0x000052C5 File Offset: 0x000034C5
            public string Label { get; }

            // Token: 0x17000012 RID: 18
            // (get) Token: 0x0600008F RID: 143 RVA: 0x000052CE File Offset: 0x000034CE
            // (set) Token: 0x06000090 RID: 144 RVA: 0x000052D6 File Offset: 0x000034D6
            public T Value { get; }
        }
    }
}