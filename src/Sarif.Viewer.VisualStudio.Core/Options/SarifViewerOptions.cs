﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.Shell;

namespace Microsoft.Sarif.Viewer.Options
{
    internal class SarifViewerOption : ISarifViewerOptions
    {
        private readonly bool shouldMonitorSarifFolderDefaultValue = true;

        private readonly bool isGitHubAdvancedSecurityEnabled = false;

        private readonly bool keyEventAdornmentEnabledDefaultValue = true;

        private readonly AsyncPackage package;

        private readonly SarifViewerOptionPage optionPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="SarifViewerOption"/> class.
        /// Get visual studio option values.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private SarifViewerOption(AsyncPackage package)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            this.optionPage = (SarifViewerOptionPage)this.package.GetDialogPage(typeof(SarifViewerOptionPage));
            this.OptionStates = new Dictionary<string, bool>
            {
                { "MonitorSarifFolder", this.ShouldMonitorSarifFolder },
                { "GitHubAdvancedSecurity", this.IsGitHubAdvancedSecurityEnabled },
                { "KeyEventAdornment", this.IsKeyEventAdornmentEnabled },
            };
        }

        private SarifViewerOption() { }

        public bool ShouldMonitorSarifFolder => this.optionPage?.MonitorSarifFolder ?? this.shouldMonitorSarifFolderDefaultValue;

        public bool IsGitHubAdvancedSecurityEnabled => this.optionPage?.EnableGitHubAdvancedSecurity ?? this.isGitHubAdvancedSecurityEnabled;

        public bool IsKeyEventAdornmentEnabled => this.optionPage?.EnableKeyEventAdornment ?? this.keyEventAdornmentEnabledDefaultValue;

        public readonly Dictionary<string, bool> OptionStates;

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static SarifViewerOption Instance { get; private set; }

        /// <summary>
        /// Initializes the singleton instance of the <see cref="SarifViewerOption"/> class.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> representing the asynchronous operation.</returns>
        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            Instance = new SarifViewerOption(package);
        }

        public static void InitializeForUnitTests()
        {
            Instance = new SarifViewerOption();
        }

        public bool IsOptionEnabled(string optionName)
        {
            if (this.OptionStates.TryGetValue(optionName, out bool state))
            {
                return state;
            }

            return false;
        }
    }
}
