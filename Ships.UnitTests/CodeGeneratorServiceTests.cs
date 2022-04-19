using FluentAssertions;
using Ships.Application.Interfaces;
using Ships.Application.Services;
using Xunit;

namespace Ships.UnitTests
{
    public class CodeGeneratorServiceTests
    {
        private readonly ICodeGeneratorService _codeGeneratorService = new CodeGeneratorService();

        [Fact]
        public void GenerateShipCode_Should_Generate_Properly_Formatted_Code()
        {
            var i = 10000;
            while (i > 0)
            {
                var shipCode = _codeGeneratorService.GenerateShipCode();
                shipCode.Length.Should().Be(12);

                var sections = shipCode.Split('-');
                sections.Length.Should().Be(3);

                sections[0].Length.Should().Be(4);
                var isFirstSectionAlphabet = sections[0].Any(x => char.IsLetter(x));
                var isFirstSectionAlphabetsUpperCase = sections[0].Any(x => char.IsUpper(x));
                isFirstSectionAlphabet.Should().BeTrue();
                isFirstSectionAlphabetsUpperCase.Should().BeTrue();

                sections[1].Length.Should().Be(4);
                var isSecondSectionNumeric = sections[1].Any(x => char.IsDigit(x));
                isSecondSectionNumeric.Should().BeTrue();

                sections[2].Length.Should().Be(2);
                var isFirstCharAlphabet = char.IsLetter(sections[2].First());
                var isFirstCharAlphabetUppercase = char.IsUpper(sections[2].First());
                var isSecondCharNumeric = char.IsDigit(sections[2].Last());

                isFirstCharAlphabet.Should().BeTrue();
                isFirstCharAlphabetUppercase.Should().BeTrue();
                isSecondCharNumeric.Should().BeTrue();

                i--;
            }
        }
    }
}
