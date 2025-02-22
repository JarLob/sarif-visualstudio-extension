﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Sarif.Viewer.Options;

namespace Microsoft.Sarif.Viewer.VisualStudio.UnitTests
{
    public class SarifViewerPackageUnitTests
    {
        public SarifViewerPackageUnitTests()
        {
            SarifViewerPackage.IsUnitTesting = true;
            SarifViewerOption.InitializeForUnitTests();
        }
    }
}
