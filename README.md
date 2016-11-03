# Entity-Framework-Core-Cookbook

This is the code repository for [Entity Framework Core Cookbook](https://www.packtpub.com/virtualization-and-cloud/learning-ibm-bluemix?utm_source=github&utm_medium=repository&utm_campaign=9781785887741), published by Packt. It contains all the supporting project files necessary to work through the book from start to finish.

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

[Learning IBM Watson Analytics](https://www.packtpub.com/big-data-and-business-intelligence/learning-ibm-watson-analytics?utm_source=github&utm_campaign=9781785880773&utm_medium=repository)

[IBM® SmartCloud® Essentials](https://www.packtpub.com/virtualization-and-cloud/ibm%C2%AE-smartcloud%C2%AE-essentials?utm_source=Github&utm_medium=Repository&utm_campaign=9781782170648)


### Suggestions and Feedback
[Click here] (https://docs.google.com/forms/d/e/1FAIpQLSe5qwunkGf6PUvzPirPDtuy1Du5Rlzew23UBp2S-P3wB-GcwQ/viewform) if you have any feedback or suggestions.
