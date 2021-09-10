﻿// Copyright Scott Whitney. All Rights Reserved.
// Reproduction or transmission in whole or in part, any form or by any
// means, electronic, mechanical or otherwise, is prohibited without the
// prior written consent of the copyright owner.

using System.Collections.Generic;
using _3DS_CivilSurveySuite.Model;

namespace _3DS_CivilSurveySuite.UI.Services
{
    public interface ISurfaceSelectService
    {
        IEnumerable<CivilSurface> GetSurfaces();

        CivilSurface SelectSurface();
    }
}