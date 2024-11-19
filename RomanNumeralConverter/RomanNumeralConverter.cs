using System.Text;

namespace RomanNumeralConverter
{
    public static class RomanNumConverter
    { 
        public static string IntToRoman(int roman)
        {
            if (roman < 1 || roman > 1000)
            {
                throw new ArgumentException("Please enter a number from 1 to 1000");
            }

            var romans =   new Dictionary<int, string>
            {
                { 1000, "M" },
                { 900, "CM" },
                { 500, "D" },
                { 400, "CD" },
                { 100, "C" },
                { 90, "XC" },
                { 50, "L" },
                { 40, "XL" },
                { 10, "X" },
                { 9, "IX" },
                { 5, "V" },
                { 4, "IV" },
                { 1, "I" }
            };


           var result = new StringBuilder();

            // Iterate through the mapping and build the Roman numeral
            // I'd prefer a linq-based solution if possible, as looping may not be performant with large datasets.
        
            // I went with a loop due to time constraints
            // Given the number 276
            // It iterates through the dictionary until the input >= key value
            // The first pass yields C.  Then it deducts 100. Since 176 is > 100, adds a second C and deducts 100 before
            // proceeding to the next value
            // At this point 76 remains, so the next pair is L, leaving 26.  Then X leaves 16.  A second X leaves 6 and so on
            // result is CCLXXVI
            foreach (var pair in romans)
            {
                while (roman >= pair.Key)
                {
                    result.Append(pair.Value);
                    roman -= pair.Key;
                }
            }

            return result.ToString();

        }

        public static int RomanToInt(string roman)
        {
            roman = roman.ToUpper().Trim();
            var romans = new Dictionary<char, int>()
            {
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M', 1000}
            };

            if (string.IsNullOrWhiteSpace(roman))
            {
                throw new ArgumentException("A roman numeral is required");
            }

            // check to see if the string passed in contains an invalid character
            if  (roman.Any(c => !romans.ContainsKey(c)))
            {
                throw new ArgumentException("Only I, V, X, L, C, D, and M are valid");
            }


            // Check for invalid subtractive combinations using LINQ
            var invalidSubtractivePatterns = new[] { "IL", "IC", "ID", "IM", "VL", "IC", "ID", "IM", "XD", "XM", "LC", "LD", "LM", "DM", "VV", "LL", "DD" };
            if (invalidSubtractivePatterns.Any(pattern => roman.Contains(pattern)))
            {
                throw new ArgumentException("Invalid Roman numeral subtractive combination");
            }

            //Zip pairs elements from two sequences
            //So given DDDDCIX
            //The first Zip takes the initial letter and pairs it with next one (D,D) and repeats through the string (D,D) to (IX)
            //subsequent zips take the pairs created by the first (D,D) then adds the next character to that grouping (making DDD) and later (DDDD)
            //
            //Once all combinations have been identified, look for any where all four Match
            //
            //This is useful to enforce something like a password restriction against 3 identical characters in a row
            if (roman.Zip(roman.Skip(1), (a, b) => new { a, b })
               .Zip(roman.Skip(2), (ab, c) => new { ab.a, ab.b, c })
               .Zip(roman.Skip(3), (abc, d) => new { abc.a, abc.b, abc.c, d })
               .Any(x => x.a == x.b && x.b == x.c && x.c == x.d))
            {
                throw new ArgumentException("Roman numerals cannot have more than three of the same character in succession.");
            }

            //this selects a list of all the values from the dictionary and puts them in a list
            //Thus given CD, it creates a list containing 100 and 500
            var values = roman.Select(c => romans[c]).ToList();

            // Use LINQ to calculate the result by summing the values
            // it takes the first element in the list and looks to see if it is less than the next
            // if its less, we can to subtract it from following one. Otherwise, the results are added and summed.
            // It proceeds to the next one in the list until it reaches the last.
            // The last number is added to accumulated sum
            int result = values.Zip(values.Skip(1), (current, next) => current < next ? -current : current).Sum() + values.Last();

            return result;
        }
    
       
    }
}


