# How to Run
- .NET 7.0 is a prerequisite.
- Build and run like a minimal ASP.NET Core WebAPI project.

# Assumptions
1. Even though HackerNews API returns Cache-Control: no-cache header, the requirements implicitly allow the API to return stale results from cache, and then update the cache.
2. I was told that this is a 30-minute task. I've spent significantly more. But still, I assume that I shouldn't spend much time on it.
3. Assuming this project won't extend, the reader would appreciate the simplicity of having everything in 1 project.

# Enhancements
1. Fix warnings about non-nullable properties for both classes at DTO folder.
2. Omit some fields at BestStoryDetails class.
3. Read documentation of HackerNews API about caching.
4. Test coverage.
5. Load testing.
6. Cache items. Check if updated using https://github.com/HackerNews/API/tree/master#changed-items-and-profiles .