using System;
using System.IO;

namespace SwordEngine.Utilities
{
    public static class ResourcePaths
    {
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

        private static string CombinePath(string root, string[] segments)
        {
            var path = root;
            foreach (var segment in segments)
            {
                path = Path.Combine(path, segment);
            }

            return path;
        }

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
