# AspectOrientedCaching

This is a dot net standard 2.0 library meant to add aspect oriented caching to your classes via attributes.

Warning: Still in development.

Update:  I left this alone for a few years, and now I'm trying to remember where I was going with this project.

It is made to be used like the PostSharp caching, just without having your IL rewritten.
I had written something similar about two years ago for dot net framework. This will be a from scratch re-creation.

The plan is to make this work with Autofac and Ninject dependency injection, and to support in memory and redis cache. I may add support for memcache if time permits.
Eventually this will all be NuGet packages. For now, It is easier to dev test referencing the projects directly.

Limitations:
Methods will only be cached when they are accessed via dependency injection. In other words, calling a method with a cache attribute from a method within the same class, the interception won't happen meaning the cache will not be accessed.

## References

* <https://github.com/vyasabhishek/Aspect-Oriented-Cache/tree/master/Cache>
* <https://doc.postsharp.net/caching>
* <https://github.com/m4bwav/CachingServiceWithAOPSupport>
* <https://github.com/MichaCo/CacheManager>
