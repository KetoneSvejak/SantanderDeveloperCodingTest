# How to Run
Open in Visual Studio, build and run like a typical ASP.NET Core WebAPI project.

# Assumptions
1. Even though HackerNews API returns Cache-Control: no-cache header, the requirements implicitly allow the API to return stale results from cache, and then update the cache.
2. I was told that this is a 30-minute task. I've spent significantly more. But still, I assume that I shouldn't spend much time on it.
3. Using https://github.com/HackerNews/API/tree/master#changed-items-and-profiles to invalidate cached items.
4. Cache configuration is rather arbitrary.

# Enhancements
1. Check what parts of HackerNews API responses can be non-nullable. In current implementation, they are all nullable.
2. Omit some fields at BestStoryDetails class
3. Test coverage, including load testing
4. Find out how https://github.com/HackerNews/API/tree/master#changed-items-and-profiles decides the time frame for updated items. Perhaps use another event to check and invalidate cache instead of on each request to GetBestStories.
5. Perhaps use Firebase API to subscribe to changes.