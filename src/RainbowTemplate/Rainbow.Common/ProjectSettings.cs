﻿using System.IO;

namespace Rainbow.Common
{
    public class ProjectSettings
    {
        public ProjectSettings(string solutionNamespace, string solutionRoot)
        {
            SolutionNamespace = solutionNamespace;
            SolutionRoot = solutionRoot;
            ViewModelRoot = Path.Combine(solutionRoot, $"{solutionNamespace}.ViewModels");
            ServiceInterfaceRoot = Path.Combine(solutionRoot, $"{solutionNamespace}.Services.Abstractions");
            ServiceRoot = Path.Combine(solutionRoot, $"{solutionNamespace}.Services");
            ControllerRoot = Path.Combine(solutionRoot, $"{solutionNamespace}.Platform.Controllers");
            ManageHostingStartupsRoot = Path.Combine(solutionRoot, $"{solutionNamespace}.Platform.WebAPP", "HostingStartups");
            CustomerHostingStartupsRoot = Path.Combine(solutionRoot, $"{solutionNamespace}.MP.WebAPI", "HostingStartups");
            PlatformWebRoot = Path.Combine(solutionRoot, $"{solutionNamespace}.Platform.WebAPP");
        }

        public string ViewModelRoot { get; }
        public string ServiceInterfaceRoot { get; }
        public string ServiceRoot { get; }
        public string ControllerRoot { get; }
        public string SolutionNamespace { get; }
        public string SolutionRoot { get; }
        public string ManageHostingStartupsRoot { get; }

        public string CustomerHostingStartupsRoot { get; }

        public string PlatformWebRoot { get; }
    }
}
