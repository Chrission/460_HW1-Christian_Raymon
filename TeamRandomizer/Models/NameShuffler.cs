using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.ComponentModel.DataAnnotations;

namespace TeamRandomizer.Models;

// This class will hold the information and methods needed to
// take and distribute names randomly across a defined team size
public class NameShuffler
{

    // For these first two, I have to credit ChatGPT for the [Required] and [RegularExpression] specification-things

    // This will hold the string initially submitted from the textarea on the Index page
    [Required]
    [RegularExpression(@"[a-zA-Z\s\,\.\-_\']+", ErrorMessage = "Invalid characters detected. Only letters, spaces and the characters ,.-_' are allowed")]
    public string? NamesString { get; set; }

    // This will hold the team size submitted from the Index page
    [Required]
    [Range(1, 10, ErrorMessage = "Team size must be between 2 and 10.")]
    public int? TeamSize { get; set; }

    // This array hold all of the individual names
    public string[]? NamesArray { get; set; }

    //This may hold the amount of teams needed to hold all names
    public int? TeamAmount { get; set; }

    public static void ProcessData(NameShuffler data)
    {
        data.NamesArray = ParseNames(data.NamesString);

        Random rng = new Random();
        KruthShuffle(rng, data.NamesArray);

        data.TeamAmount = CalculateTeamsAmount(data.NamesArray, data.TeamSize ?? 0);
    }

    // This method will translate the names from a string, to a list of names.
    // names should be seperated by newlines
    public static string[] ParseNames(string? namesIn)
    {
        if (string.IsNullOrWhiteSpace(namesIn))
        {
            return Array.Empty<string>();
        }
        // List<string> NamesList = new List<string>();
        string[]? names = namesIn.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        return names;
    }

    // This method will calculate the amount of teams needed
    public static int CalculateTeamsAmount(string[] namesArray, int teamSize)
    {
        int teamAmount = (int)Math.Ceiling((double)namesArray.Length / teamSize);
        return teamAmount;
    }

    // This method will shuffle the order of names from the NamesArray
    // Based on the implementation of the Fisher-Yates algorithm found here: https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net/110570#110570
    public static void KruthShuffle(Random rng, string[] namesArray)
    {
        int n = namesArray.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            (namesArray[n], namesArray[k]) = (namesArray[k], namesArray[n]);
        }
    }

}