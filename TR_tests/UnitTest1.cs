using NUnit.Framework;
using TeamRandomizer.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Configuration;

namespace TR_tests;

public class Tests
{
    [TestFixture]
    public class NameShufflerTests
    {
        private NameShuffler _nameShuffler;

        [SetUp]
        public void Setup()
        {
            _nameShuffler = new NameShuffler();
        }

        [Test]
        public void NameStringShouldFailWithInvalidChar()
        {
            // Arrange
            _nameShuffler.NamesString = "Jerma985";
            var context = new ValidationContext(_nameShuffler);
            var results = new System.Collections.Generic.List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateObject(_nameShuffler, context, results, true);

            // Assert
            Assert.IsFalse(isValid, "Validation should fail with invalid characters.");
        }

        [Test]
        public void TeamSizeShouldFailWhenOutOfRange()
        {
            // Arrange
            _nameShuffler.TeamSize = 15;
            var context = new ValidationContext(_nameShuffler);
            var results = new System.Collections.Generic.List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateObject(_nameShuffler, context, results, true);

            // Assert
            Assert.IsFalse(isValid, "Validation should fail with an out-of-range team size.");
        }


        [Test]
        public void NamesStringShouldPassValidationWithValidCharacters()
        {
            // Arrange
            _nameShuffler = new NameShuffler
            {
                NamesString = "Jerma",
                TeamSize = 2
            };
            var context = new ValidationContext(_nameShuffler);
            var results = new System.Collections.Generic.List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateObject(_nameShuffler, context, results, true);

            // Assert
            Assert.IsTrue(isValid, "Validation should pass with valid characters.");
        }

        [Test]
        public void KruthShuffleShouldShuffleNamesArray()
        {
            // Arrange
            string[] names = { "Jerma\n", "Ster_\n", "Vinny Vinesauce\n", "Poke" };
            string[] original = (string[])names.Clone();
            Random rng = new Random();

            // Act
            NameShuffler.KruthShuffle(rng, names);

            // Assert
            Assert.That(names, Is.Not.EqualTo(original), "Names should be shuffled.");
            Assert.That(names.OrderBy(x => x), Is.EqualTo(original.OrderBy(x => x)),
                "Shuffled names should contain the same elements.");
        }

    }
}