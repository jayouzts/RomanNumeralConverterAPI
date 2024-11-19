using RomanNumeralConverter;

namespace TestProject1
{
    public class Tests
    {

        [TestCase(1, "I")]
        [TestCase(4, "IV")]
        [TestCase(9, "IX")]
        [TestCase(10, "X")]
        [TestCase(40, "XL")]
        [TestCase(50, "L")]
        [TestCase(90, "XC")]
        [TestCase(99, "XCIX")]
        [TestCase(100, "C")]
        [TestCase(400, "CD")]
        [TestCase(500, "D")]
        [TestCase(900, "CM")]
        [TestCase(1000, "M")]
        public void IntToRoman_ValidInput_ReturnsCorrectRomanNumeral(int input, string expectedOutput)
        {
            var result = RomanNumConverter.IntToRoman(input);
            Assert.AreEqual(expectedOutput, result);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void IntToRoman_InvalidInput_ThrowsArgumentException(int input)
        {
            Assert.Throws<ArgumentException>(() => RomanNumConverter.IntToRoman(input));
        }

        [TestCase("I", 1)]
        [TestCase("IV", 4)]
        [TestCase("IX", 9)]
        [TestCase("X", 10)]
        [TestCase("XL", 40)]
        [TestCase("L", 50)]
        [TestCase("XC", 90)]
        [TestCase("C", 100)]
        [TestCase("CD", 400)]
        [TestCase("D", 500)]
        [TestCase("CM", 900)]
        [TestCase("M", 1000)]
        [TestCase("MMMCMXCIX", 3999)]
        public void RomanToInt_ValidInput_ReturnsCorrectInteger(string roman, int expectedOutput)
        {
            var result = RomanNumConverter.RomanToInt(roman);
            Assert.AreEqual(expectedOutput, result);
        }

        [TestCase("A")] // Invalid character
        [TestCase("IIII")] // Invalid Roman numeral
        [TestCase("VV")] // Invalid Roman numeral
        [TestCase("IC")] // Invalid Roman numeral
        public void RomanToInt_InvalidInput_ThrowsArgumentException(string roman)
        {
            Assert.Throws<ArgumentException>(() => RomanNumConverter.RomanToInt(roman));
        }

   
    }
}