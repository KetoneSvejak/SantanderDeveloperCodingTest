# How to Run
Build and run like you would an empty ASP.NET Core WebAPI project in Visual Studio. No extra steps required.

# Assumptions
1. Even though HackerNews API returns Cache-Control: no-cache header, the requirements implicitly allow the API to return stale results from cache, and then update the cache.
2. I was told that this is a 30-minute task. I've spent significantly more. But still, I assume that I shouldn't spend much time on it.

# Enhancements
1. Fix warnings about non-nullable properties for both classes at DTO folder.
2. Omit some fields at BestStoryDetails class.
3. Read documentation of HackerNews API about caching.
4. Test coverage.
5. Load testing.