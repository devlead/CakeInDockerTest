var target = Argument("target", "Default");

Setup(
    static context => new DotNetMSBuildSettings()
            .WithProperty("Configuration", "Release")
            .WithProperty("Version", context.Argument("build-version", "1.0.0"))
);

Task("Build")
    .Does<DotNetMSBuildSettings>(
        static (context, mSBuildSettings) => {
            context.DotNetBuild("./src/HelloWorld.sln",
                new DotNetBuildSettings
                {
                    MSBuildSettings = mSBuildSettings
                });
        }
    );

Task("Publish")
    .IsDependentOn("Build")
    .Does<DotNetMSBuildSettings>(
        static (context, mSBuildSettings) => {
            context.DotNetPublish(
                "./src/HelloWorld/HelloWorld.csproj",
                new DotNetPublishSettings
                {
                    MSBuildSettings = mSBuildSettings,
                    OutputDirectory = "/app/publish"
                });
        }
    );


Task("Default")
    .IsDependentOn("Build");

RunTarget(target);