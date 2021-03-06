using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Acme.Orders.Api.AcceptanceTests.Infrastructure;
using Acme.Orders.Api.Config;

namespace Acme.Orders.Api.AcceptanceTests
{
    [TestClass]
    public class InitializeAcceptanceTestRun
    {
        public const string DEFAULT_TEST_EXECUTION_CONTEXT = "Development-InProcess";
        public const string TEST_EXECUTION_CONTEXT_KEY_NAME = "TestExecutionContext";
        
        private static IServiceProvider _instance;

        [AssemblyInitialize]
        public static void SetupTestRun(TestContext testContext)
        {
            // The Text Execution Context (environment, variables) are chosen in this order:
            //
            // 1. {Acme.Orders.Api.ConfigurationSingleton.ENVIRONMENT_VARIABLE_PREFIX}_TEST_EXECUTION_CONTEXT
            // 2. DEFAULT_TEST_EXECUTION_CONTEXT
            // 3. .runsettings is the fallback

            var testExecutionContext = $"{Environment.GetEnvironmentVariable($"{ConfigurationSingleton.EnvironmentVariablePrefix}_TEST_EXECUTION_CONTEXT") ?? DEFAULT_TEST_EXECUTION_CONTEXT}";
            if (testContext.Properties.Contains(TEST_EXECUTION_CONTEXT_KEY_NAME))
            {
                testExecutionContext = Convert.ToString(testContext.Properties[TEST_EXECUTION_CONTEXT_KEY_NAME]);
            }

            var candidateTestExecutionContextFilename = $"testSettings.{testExecutionContext}.json";

            _instance = ContainerSingleton.InitializeContainer(candidateTestExecutionContextFilename);

            testContext.Properties.Add("ServiceProvider", _instance);
        }

        [AssemblyCleanup]
        public static void CleanupTestRun()
        {
        }
    }
}
