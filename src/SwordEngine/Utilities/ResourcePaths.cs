using System;
using System.IO;

namespace SwordEngine.Utilities
{
    /// <summary>
    /// Class to handle resource paths for the SwordEngine project.
    /// It provides methods to resolve resource paths based on the application's base directory, current working directory, and repository root.
    /// </summary>
    public static class ResourcePaths
    {
        /// <summary>
        /// Method to resolve the full path to a resource file or directory based on the provided segments.
        /// </summary>
        /// <param name="segments">The segments of the path to be combined.</param>
        /// <returns>The full path to the resource file or directory.</returns>
        public static string ResolveResourcePath(params string[] segments)
        {
            var candidates = new[]
            {
                AppContext.BaseDirectory,
                Directory.GetCurrentDirectory()
            };

            foreach (var baseDir in candidates)
            {
                var candidate = CombinePath(baseDir, segments);
                if (File.Exists(candidate) || Directory.Exists(candidate))
                {
                    return candidate;
                }
            }

            var repoRoot = FindRepositoryRoot();
            if (repoRoot != null)
            {
                var repoCandidate = CombinePath(repoRoot, segments);
                if (File.Exists(repoCandidate) || Directory.Exists(repoCandidate))
                {
                    return repoCandidate;
                }
            }

            return CombinePath(AppContext.BaseDirectory, segments);
        }

        /// <summary>
        /// Method used to combine a root path with additional segments to form a complete path.
        /// </summary>
        /// <param name="root">The root path to start from, should be retrieved from install location.</param>
        /// <param name="segments">The additional path segments to combine with the root path.</param>
        /// <returns>The combined full path.</returns>
        private static string CombinePath(string root, string[] segments)
        {
            var path = root;
            foreach (var segment in segments)
            {
                path = Path.Combine(path, segment);
            }

            return path;
        }

        /// <summary>
        /// Method to find the repository root by traversing up the directory tree from the application's base directory.
        /// </summary>
        /// <returns>The full path to the repository root if found; otherwise, null.</returns>
        private static string? FindRepositoryRoot()
        {
            var current = new DirectoryInfo(AppContext.BaseDirectory);
            while (current != null)
            {
                if (Directory.Exists(Path.Combine(current.FullName, "res")) &&
                    File.Exists(Path.Combine(current.FullName, "README.md")))
                {
                    return current.FullName;
                }

                current = current.Parent;
            }

            return null;
        }
    }
}
