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
    [TestClass]
    public class EnvironmentObjectTests
    {
        [TestMethod]
        public void IsWorldSizeGreaterThanLimits()
        {
            var environmentWorldSizeTest = new EnvironmentObject
            {
                Name = "Test Environment Large",
                MaxHeight = 500,
                MaxLenght = 1200,
            };

            var dossier = new ValidationContext(environmentWorldSizeTest, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(environmentWorldSizeTest, dossier, results, true);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsWorldSizeSmallerThanLimits()
        {
            var environmentWorldSizeTest = new EnvironmentObject
            {
                Name = "Test Environment Small",
                MaxHeight = 5,
                MaxLenght = 15,
            };
            var dossier = new ValidationContext(environmentWorldSizeTest, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(environmentWorldSizeTest, dossier, results, true);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsWorldSizeWithinLimits()
        {
            var environmentWorldSizeTest = new EnvironmentObject
            {
                Name = "Test Environment Valid",
                MaxHeight = 50,
                MaxLenght = 150,
            };
            var dossier = new ValidationContext(environmentWorldSizeTest, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(environmentWorldSizeTest, dossier, results, true);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsNameTooLong()
        {
            var environmentNameTest = new EnvironmentObject
            {
                Name = "ThisIsAVeryLongEnvironmentNameThatExceedsTheMaximumLengthOf25Characters",
                MaxHeight = 50,
                MaxLenght = 150,
            };
            var dossier = new ValidationContext(environmentNameTest, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(environmentNameTest, dossier, results, true);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsNameValid()
        {
            var environmentNameTest = new EnvironmentObject
            {
                Name = "ValidName",
                MaxHeight = 50,
                MaxLenght = 150,
            };
            var dossier = new ValidationContext(environmentNameTest, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(environmentNameTest, dossier, results, true);
            Assert.IsTrue(isValid);
        }
    }

    [TestClass]
    public class ObjectRepoTests
    {
        [TestMethod]
        public void IsObjectSizeGreaterThanLimits()
        {
            var objectSizeTest = new ObjectRepo
            {
                ObjGuid = Guid.NewGuid(),
                PrefabId = 1,
                PositionX = 0,
                PositionY = 0,
                ScaleX = 60,
                ScaleY = 60,
                RotationZ = 0,
                SortingLayer = 1,
                EnvironmentGuid = Guid.NewGuid()
            };
            var dossier = new ValidationContext(objectSizeTest);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(objectSizeTest, dossier, results, true);
            Assert.IsFalse(isValid);
        }
    }
}