using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySecureBackend.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MySecureBackend.WebApi.Controllers;
using MySecureBackend.WebApi.Repositories;
using MySecureBackend.WebApi.Services;

namespace MySecureBackend.Tests
{

    namespace SecureBackendTests
    {
        [TestClass]
        public class EnvironmentObjectTests
        {
            [TestMethod]
            public void IsWorldNameValid()
            {
                var envNaamTest = new EnvironmentObject
                {
                    EnvName = "ThisWorldHasALongNameBecauseINeedToTestWhatHappensWhenItIsThisLong",
                    MaxLenght = 200, MaxHeight = 100
                };

                var dossier = new ValidationContext(envNaamTest);
                var results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(envNaamTest, dossier, results, true);

                Assert.IsFalse(isValid);

            }
        }
    }
}
