﻿// Copyright Scott Whitney. All Rights Reserved.
// Reproduction or transmission in whole or in part, any form or by any
// means, electronic, mechanical or otherwise, is prohibited without the
// prior written consent of the copyright owner.

using _3DS_CivilSurveySuite.ACAD2017.Extensions;
using _3DS_CivilSurveySuite.Core;
using _3DS_CivilSurveySuite.Model;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.Runtime;

[assembly: CommandClass(typeof(_3DS_CivilSurveySuite.ACAD2017.Traverse))]
namespace _3DS_CivilSurveySuite.ACAD2017
{
    public class Traverse
    {
        private const int GraphicsSize = 6;

        // Bearing
        // Distance
        // Show transient line
        // Ask to confirm or flip or cancel
        // Cancel removes the transient line and starts a bearing

        // If more than 2 in list show zoom to closure option.

        [CommandMethod("3DS", "_3DSTraverse", CommandFlags.Modal)]
        public void Traverse_CommandLine()
        {
            AcadApp.Editor.WriteMessage("\n3DS> === Traverse Utility ===");

            var graphics = new TransientGraphics(TransientDrawingMode.Main);
            try
            {
                if (!EditorUtils.GetBasePoint3d(out Point3d basePoint, "\n3DS> Select base point: "))
                    return;

                graphics.DrawPlus(basePoint, GraphicsSize);

                if (!EditorUtils.GetAngle(out Angle angle, "\n3DS> Bearing: ", basePoint))
                    return;

                AcadApp.Editor.WriteMessage($"\n3DS> {angle}");

                //TODO: Add feet/units conversion
                if (!EditorUtils.GetDistance(out double distance, "\n3DS> Distance: ", basePoint))
                    return;

                var pko = new PromptKeywordOptions("\n3DS> Continue? ") { AppendKeywordsToMessage = true };
                pko.Keywords.Add(Keywords.Accept);
                pko.Keywords.Add(Keywords.Cancel);
                pko.Keywords.Add(Keywords.Change);
                pko.Keywords.Add(Keywords.Flip);

                Point newPoint = MathHelpers.AngleAndDistanceToPoint(angle, distance, basePoint.ToPoint());
                graphics.DrawLine(basePoint.ToPoint2d(), newPoint.ToPoint2d());
                graphics.DrawPlus(newPoint.ToPoint3d(), GraphicsSize);
                graphics.DrawArrow(MathHelpers.MidpointBetweenPoints(basePoint.ToPoint(), newPoint).ToPoint3d(), angle, GraphicsSize);

                PromptResult prResult = AcadApp.Editor.GetKeywords(pko);

                switch (prResult.StringResult)
                {
                    case Keywords.Accept:
                        using (var tr = AcadApp.StartTransaction())
                        {
                            LineUtils.DrawLine(tr, basePoint, newPoint.ToPoint3d());
                            tr.Commit();
                        }
                        break;
                    case Keywords.Cancel:
                        break;
                    case Keywords.Change:
                        break;
                    case Keywords.Flip:
                        break;
                }
            }
            catch (Exception e)
            {
                AcadApp.Editor.WriteMessage($"Exception: {e.Message}");
            }
            finally
            {
                graphics.Dispose();
            }
        }
    }
}