﻿// Copyright Scott Whitney. All Rights Reserved.
// Reproduction or transmission in whole or in part, any form or by any
// means, electronic, mechanical or otherwise, is prohibited without the
// prior written consent of the copyright owner.

using System.Collections.Generic;
using _3DS_CivilSurveySuite.ACAD2017;
using _3DS_CivilSurveySuite.Model;
using _3DS_CivilSurveySuite.UI.Services;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Civil.DatabaseServices;

namespace _3DS_CivilSurveySuite.C3D2017.Services
{
    public class PointGroupSelectService : IPointGroupSelectService
    {
        public IEnumerable<CivilPointGroup> GetPointGroups()
        {
            var list = new List<CivilPointGroup>();
            using (var tr = AcadApp.StartLockedTransaction())
            {
                foreach (ObjectId pointGroupId in C3DApp.ActiveDocument.PointGroups)
                {
                    var pointGroup = tr.GetObject(pointGroupId, OpenMode.ForRead) as PointGroup;
                    list.Add(pointGroup.ToCivilPointGroup());
                }

                tr.Commit();
            }
            return list;
        }
    }
}