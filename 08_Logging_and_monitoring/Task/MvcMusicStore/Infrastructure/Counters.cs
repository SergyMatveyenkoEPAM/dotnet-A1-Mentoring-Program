using PerformanceCounterHelper;
using System.Diagnostics;

namespace MvcMusicStore.Infrastructure
{
    [PerformanceCounterCategory("MvcMusicStor", System.Diagnostics.PerformanceCounterCategoryType.MultiInstance, "MvcMusicStor")]
    public enum Counters
    {
        [PerformanceCounter("Go to Home count", "Go to home", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
        GoToHome,
        [PerformanceCounter("Successful log in count", "Successful log in", PerformanceCounterType.NumberOfItems32)]
        SuccessfulLogIn,
        [PerformanceCounter("Successful log off count", "Successful log off", PerformanceCounterType.NumberOfItems32)]
        SuccessfulLogOff
    }
}