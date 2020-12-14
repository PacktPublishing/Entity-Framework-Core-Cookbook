## $5 Tech Unlocked 2021!
[Buy and download this Book for only $5 on PacktPub.com](https://www.packtpub.com/product/entity-framework-core-cookbook-second-edition/9781785883309)
-----
*If you have read this book, please leave a review on [Amazon.com](https://www.amazon.com/gp/product/1785883305).     Potential readers can then use your unbiased opinion to help them make purchase decisions. Thank you. The $5 campaign         runs from __December 15th 2020__ to __January 13th 2021.__*

# Entity Framework Core Cookbook

This is the code repository for [Entity Framework Core Cookbook](https://www.packtpub.com/application-development/entity-framework-core-10-cookbook-second-edition?utm_source=github&utm_medium=repository&utm_content=9781785883309), published by Packt. It contains all the supporting project files necessary to work through the book from start to finish.

##Instructions and Navigation
All of the code is organized into folders. Each folder starts with number followed by the application name. For example, Chapter02.

You will see code something similar to the following:

```
protected void OnModelCreating(ModelBuilder modelBuilder)
{
  modelBuilder
   .HasSequence(name: "SeqName", schema: "shared")
   .StartsAt(1)
   .IncrementsBy(1);
  base.OnModelCreating (modelBuilder);
}


```

Software and Hardware List

| Chapter  | Software required       | OS required           | Free/Proprietary | Download links to the software           |
| -------- | ------------------------| ----------------------|------------------|------------------------------------------|
| 1        |Visual Studio 2015       | Windows               |  Proprietary     |  https://www.visualstudio.com            |
| 2        |SQL Server 2012/2016     | Windows/              |  Proprietary     |https://www.microsoft.com/en-us/cloud-platform/sql-server-editions-express|
| 3        |.NET Core                | Windows/Mac OSX/ Linux|  Proprietary     | https://www.microsoft.com/net/core       |

##Related IBM topics:

* [Entity Framework 4.1: Expert’s Cookbook](https://www.packtpub.com/application-development/entity-framework-41-expert%E2%80%99s-cookbook?utm_source=github&utm_medium=repository&utm_content=9781849684460)

* [Mastering Entity Framework](https://www.packtpub.com/application-development/mastering-entity-framework?utm_source=github&utm_medium=repository&utm_content=9781784391003)

* [Entity Framework Tutorial - Second Edition](https://www.packtpub.com/application-development/entity-framework-tutorial-second-edition?utm_source=github&utm_medium=repository&utm_content=9781783550012)

### Suggestions and Feedback
[Click here] (https://docs.google.com/forms/d/e/1FAIpQLSe5qwunkGf6PUvzPirPDtuy1Du5Rlzew23UBp2S-P3wB-GcwQ/viewform) if you have any feedback or suggestions.
