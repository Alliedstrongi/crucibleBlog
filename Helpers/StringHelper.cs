using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace crucibleBlog.Helpers
{
	public static class StringHelper
	{
		public static string BlogPostSlug(string? title)
		{
			// removes all accents sand lowercases the strings
			string? output = RemoveAccents(title).ToLower();

			// remove any special characters from a string
			output = Regex.Replace(output, @"[^A-Za-z0-9\s-]", "");

			// remove all additional spces in favour of just one
			output = Regex.Replace(output, @"\s+", " ");

			// replace all spaces with a hyphen
			output = Regex.Replace(output, @"\s", "-");

			return output;
		}

		public static string RemoveAccents(string? title)
		{
			if (string.IsNullOrWhiteSpace(title))
			{
				return title;
			}

			// Convert for Unicode
			title = title.Normalize(System.Text.NormalizationForm.FormD);

			// Format unicode/ascii
			char[] chars = title.Where(c=> CharUnicodeInfo.GetUnicodeCategory(c) !=UnicodeCategory.NonSpacingMark).ToArray();

			// Convert and return the new title
			return new string(chars).Normalize(NormalizationForm.FormC);
		}
	}
}
