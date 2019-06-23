# AspectOrientedCaching

This is a dot net standard 2.0 library meant to add aspect oriented caching to your classes via attributes.

It is made to be used like the PostSharp caching, just without having your IL rewritten.
I had written something similar about two years ago for dot net framework. This will be a from scratch re-creation.

The plan is to make this work with Autofac and Ninject dependency injection, and to support in memory and redis cache. I may add support for memcache if time permits.

Limitations:
Methods will only be cached when they are accessed via dependency injection. In other words, calling a method with a cache attribute from a method within the same class, the interception won't happen meaning the cache will not be accessed.

## References

* <https://github.com/vyasabhishek/Aspect-Oriented-Cache/tree/master/Cache>
* <https://doc.postsharp.net/caching>
* <https://github.com/m4bwav/CachingServiceWithAOPSupport>
* <https://github.com/MichaCo/CacheManager>
