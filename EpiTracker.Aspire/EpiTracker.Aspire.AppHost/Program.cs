var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.EpiTracker_Aspire_ApiService>("apiservice");

builder.AddProject<Projects.EpiTracker_Aspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
