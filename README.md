## WHAT IS MANGOCACHE?

MangoCache is a kind of simple analogue of Redis for data caching, enriched with sufficient functionality, which is wrapped in a convenient Nuget package.

## ADVANTAGES

The caching system itself is very lightweight, does not require excessive configuration, and provides high-speed caching in the simplest way using binary serialization.
In addition to the comfortable client, there is also a Core part, which gives you the opportunity to let your imagination run wild, write a client for yourself, expand functionality and have full control over the caching logic.

## HOW TO USE?

First, install the packages needed for work:

![client](https://github.com/yokohama-spirit/MangoCache/raw/main/src/picts/install_client.png)
![core](https://github.com/yokohama-spirit/MangoCache/raw/main/src/picts/install_client.png)

Then we register services in Program.cs:

![programcs](https://github.com/yokohama-spirit/MangoCache/raw/main/src/picts/programcs.png)

After which we can safely use it in the code!

![example](https://github.com/yokohama-spirit/MangoCache/raw/main/src/picts/example.png)
