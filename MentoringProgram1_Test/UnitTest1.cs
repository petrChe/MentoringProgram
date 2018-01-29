using System;
using System.Collections.Generic;
using MentoringProgram;
using MentoringProgram.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MentoringProgram1_Test
{
    [TestClass]
    public class UnitTest1
    {
        public const string Path = @"C:\Users\Petr_Chebanenko\Desktop";

        [TestMethod]
        public void Files_Are_Finded()
        {
            var enumerator = new Mock<IFilesEnumerator>();
            enumerator.Setup(e => e.GetAllFilesByPath(Path)).Returns(new List<UsingFile>());


            var visitor = new FileSystemVisitor(enumerator.Object,
                                               (UsingFile file, string fileExtension) => file.Extension == fileExtension,
                                               Path);

            visitor.StartWork("ex", ".txt", Path);

                        
            Assert.IsNotNull(visitor.FindedFiles);
        }

        [TestMethod]
        public void Files_Are_Not_Finded()
        {
            var enumerator = new Mock<IFilesEnumerator>();
            enumerator.Setup(e => e.GetAllFilesByPath(Path)).Returns(new List<UsingFile>());


            var visitor = new FileSystemVisitor(enumerator.Object,
                                               (UsingFile file, string fileExtension) => file.Extension == fileExtension,
                                               Path);

            visitor.StartWork("efdsf", ".txt", Path);

            Assert.IsNull(visitor.FindedFiles);
        }

        [TestMethod]
        public void Files_Are_Not_Finded_Because_Of_Wrong_Extension()
        {
            var enumerator = new Mock<IFilesEnumerator>();
            enumerator.Setup(e => e.GetAllFilesByPath(Path)).Returns(new List<UsingFile>());


            var visitor = new FileSystemVisitor(enumerator.Object,
                                               (UsingFile file, string fileExtension) => file.Extension == fileExtension,
                                               Path);

            visitor.StartWork("exf", ".wwwwwwwe", Path);

            Assert.IsNull(visitor.FindedFiles);
        }        
    }
}
