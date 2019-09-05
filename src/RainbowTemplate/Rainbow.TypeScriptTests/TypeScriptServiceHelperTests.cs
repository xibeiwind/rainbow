using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using Rainbow.TypeScript;
using Yunyong.Core;

namespace Rainbow.TypeScriptTests
{
    public class TypeScriptServiceHelperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGenerateVueService()
        {
            var types = new List<Type>
            {
                typeof(AsyncTaskResult),
                typeof(AsyncTaskTResult<>),
                typeof(PagingList<>),
                //typeof(DataType),
            };

            var helper = new TypeScriptServiceHelper();
            helper.GenerateTypeScriptContracts(new ContractSetting()
            {
                Assembly = Assembly.Load("Rainbow.ViewModels"),
                OutputPath = @"TestOutput",
                ExtTypes = types
            });
            helper.GenerateTypeScriptServices(new TypeScriptServiceSetting
            {
                Assembly = Assembly.Load("Rainbow.Platform.Controllers"),
                ServiceType = TypeScriptServiceType.Vue,
                OutputPath = $@"TestOutput\Services"
            });
            Assert.Pass();
        }

        [Test]
        public void TestGenerateAngularServices()
        {
            var types = new List<Type>
            {
                typeof(AsyncTaskResult),
                typeof(AsyncTaskTResult<>),
                typeof(PagingList<>),
            };

            var helper = new TypeScriptServiceHelper();
            helper.GenerateTypeScriptContracts(new ContractSetting()
            {
                Assembly = Assembly.Load("Rainbow.ViewModels"),
                OutputPath = @"Angular\TestOutput",
                ExtTypes = types
            });

            helper.GenerateTypeScriptServices(new TypeScriptServiceSetting
            {
                Assembly = Assembly.Load("Rainbow.Platform.Controllers"),
                ServiceType = TypeScriptServiceType.Angular,
                OutputPath = $@"Angular\TestOutput\Services"
            });
            Assert.Pass();
        }
    }
}