using DocArcSharedLibrary.Interfaces;
using DocArcSharedLibrary.MockedProvider;
using DocArcSharedLibrary.Provider;

namespace DocArcService.Provider
{
    public static class ProviderFactory
    {
        public static bool IsMocked { get; set; } = false;

        public static IDatabaseProvider CreateDatabaseProvider()
        {
            return IsMocked
                ? new MockedDatabaseProvider() as IDatabaseProvider
                : new AzureDatabaseProvider();
        }
    }
}