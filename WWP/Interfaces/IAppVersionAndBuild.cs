using System;
namespace WWP.Interfaces
{
    public interface IAppVersionAndBuild
    {
        string GetVersionNumber();
        string GetBuildNumber();
    }
}