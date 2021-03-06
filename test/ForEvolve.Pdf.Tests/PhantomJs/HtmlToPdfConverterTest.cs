﻿using ForEvolve.Pdf.Abstractions;
using ForEvolve.Pdf.PhantomJs;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs
{
    public class HtmlToPdfConverterTest
    {
        private readonly HtmlToPdfConverter sut;
        private readonly HtmlToPdfConverterOptions _options;
        private readonly Mock<IExecutableNameFinder> _executableNameFinderMock;
        private readonly Mock<IHtmlToPdfConverterOptionsSerializer> _optionsSerializerMock;

        private readonly string _currentDirectory;
        private readonly string _targetDirectory;
        private const string SerializedOptions = "{\\\"prop\\\":\\\"serialized!\\\"}";
        private const string ExpectedSerializedOptions = "{\"prop\":\"serialized!\"}";

        public HtmlToPdfConverterTest()
        {
            _options = new HtmlToPdfConverterOptions();

            _executableNameFinderMock = new Mock<IExecutableNameFinder>();
            _executableNameFinderMock
                .Setup(x => x.Find())
                .Returns("ArgsLoggerConsole.exe");

            _optionsSerializerMock = new Mock<IHtmlToPdfConverterOptionsSerializer>();
            _optionsSerializerMock
                .Setup(x => x.Serialize(It.IsAny<HtmlToPdfConverterOptions>()))
                .Returns(SerializedOptions);

            sut = new HtmlToPdfConverter(
                _options, 
                _executableNameFinderMock.Object, 
                _optionsSerializerMock.Object
            );

            _currentDirectory = Directory.GetCurrentDirectory();
            _targetDirectory = Path.Combine(_currentDirectory, "PhantomJs", "Target");
        }

        public class Ctor : HtmlToPdfConverterTest
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                var nullOptions = default(HtmlToPdfConverterOptions);
                var nullExecutableNameFinder = default(IExecutableNameFinder);
                var nullHtmlToPdfConverterOptionsSerializer = default(IHtmlToPdfConverterOptionsSerializer);

                // Act & Assert
                Assert.Throws<ArgumentNullException>("options", () => new HtmlToPdfConverter(
                    nullOptions,
                    _executableNameFinderMock.Object,
                    _optionsSerializerMock.Object
                ));
                Assert.Throws<ArgumentNullException>("executableNameFinder", () => new HtmlToPdfConverter(
                    _options,
                    nullExecutableNameFinder,
                    _optionsSerializerMock.Object
                ));
                Assert.Throws<ArgumentNullException>("optionsSerializer", () => new HtmlToPdfConverter(
                    _options,
                    _executableNameFinderMock.Object,
                    nullHtmlToPdfConverterOptionsSerializer
                ));
            }
        }

        public class Convert : HtmlToPdfConverterTest
        {
            [Fact]
            public void Should_pass_expected_arguments_to_the_exe()
            {
                // Arrange
                var html = GenerateHtml();

                // Act
                var result = sut.Convert(html, _targetDirectory);

                // Assert
                var expectedFileName = Path.GetFileNameWithoutExtension(result);
                var expectedOutputFilePath = result;
                var logFile = Path.Combine(_options.PhantomRootDirectory, "ArgsLoggerConsole.txt");
                var fileExists = File.Exists(logFile);
                Assert.True(fileExists, "The log file should exist to verify its content.");
                var lines = File.ReadAllLines(logFile);
                Assert.Collection(lines,
                    line => Assert.Equal("rasterize.js", line),
                    line => Assert.Equal(expectedFileName, line),
                    line => Assert.Equal(expectedOutputFilePath, line),
                    line => Assert.Equal(ExpectedSerializedOptions, line)
                );
            }

            [Fact]
            public void Should_throw_a_ArgumentException_when_the_specified_outputDirectory_does_not_exist()
            {
                // Arrange
                var html = GenerateHtml();

                // Act & Assert
                var ex = Assert.Throws<ArgumentException>("outputDirectory", () => sut.Convert(html, "z:\\some-unexisting-directory\\"));
                Assert.StartsWith(PhantomJsConstants.OutputDirectoryDoesNotExist, ex.Message);
            }

            [Fact]
            public void Should_throw_a_EX_when_the_executable_does_not_exist()
            {
                // Arrange
                var html = GenerateHtml();
                var expectedErrorMessage = PhantomJsConstants.PhantomJSExeNotFound;
                _executableNameFinderMock
                    .Setup(x => x.Find())
                    .Returns("Unexisting.exe");

                // Act & Assert
                var ex = Assert.Throws<FileNotFoundException>(() => sut.Convert(html, _targetDirectory));
                Assert.Equal(expectedErrorMessage, ex.Message);
                Assert.Equal("Unexisting.exe", ex.FileName);
            }

        }

        private string GenerateHtml()
        {
            return @"<!DOCTYPE html>
<html>
<head>
    <title>Generated HTML</title>
</head>
<body>
    <h1>Hello World!</h1>
    <p>This PDF has been generated by PhantomJs ;)</p>

    <h2>This is a subtitle</h2>
    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla dignissim sit amet erat vehicula semper. In lobortis, velit mattis sagittis accumsan, erat nisl tincidunt dui, varius imperdiet elit lacus non metus. Ut iaculis maximus semper. Interdum et malesuada fames ac ante ipsum primis in faucibus. Ut ac pharetra augue. Fusce vitae felis auctor, rutrum urna vitae, sagittis nibh. Curabitur in aliquet odio, non varius orci. Aenean sit amet rutrum nibh. Aenean erat urna, efficitur id maximus a, dapibus ut sem.</p>
</body>
</html>";
        }
    }
}
