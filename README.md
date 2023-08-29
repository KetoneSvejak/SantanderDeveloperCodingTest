# How to Run
Open in Visual Studio, build and run like a typical ASP.NET Core WebAPI project.

# Assumptions
1. Even though HackerNews API returns Cache-Control: no-cache header, the requirements implicitly allow the API to return stale results from cache, and then update the cache.
2. I was told that this is a 30-minute task. I've spent significantly more. But still, I assume that I shouldn't spend much time on it.
3. Assuming this project won't extend, the reader would appreciate the simplicity of having everything in 1 project.
4. Using https://github.com/HackerNews/API/tree/master#changed-items-and-profiles to invalidate cached items.

# Enhancements
1. Check what parts of HackerNews API responses can be non-nullable. In current implementation, they are all nullable.
2. Omit some fields at BestStoryDetails class
3. Test coverage, including load testing
4. Find out how https://github.com/HackerNews/API/tree/master#changed-items-and-profiles decides the time frame for updated items.