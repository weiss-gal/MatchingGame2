using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchingGame2.lib.infra.Logging;
using Microsoft.VisualBasic.FileIO;

namespace MatchingGame2.lib.parsing.CVSParser
{
    enum ParserState
    {
        Created,
        Started,
        Parsing,
        Completed,
        Error //unrecoverable error
    }

    public class ParsingException : Exception
    {
        public ParsingException(string message) : base(message)
        {
        }

    }

    public struct ParsedLine
    {
        public uint index;
        public IEnumerable<KeyValuePair<string, string>> items;

        public ParsedLine(uint index, IEnumerable<KeyValuePair<string, string>> items)
        {
            this.index = index;
            this.items = items;
        }
    }

    class CSVParser
    {
        private Logger logger;


        public CSVParser(Logger logger)
        {
            this.logger = logger;
        }


        //Converts items list to human readable string in the following format:
        //  (key1, value2); (key2, value2); ...
        private string ItemsToString(IEnumerable<KeyValuePair<string, string>> items)
        {
            return string.Join("; ", items.Select(i => $"({i.Key}, {i.Value})"));
        }

        public IEnumerable<ParsedLine> parse(string fileName, IEnumerable<string> mandatoryFields)
        {
            var csvParser = new TextFieldParser(fileName);
            csvParser.Delimiters = new string[] { "," };

            //Read header line
            var headerFields = csvParser.ReadFields().Select(f => f.Trim().ToLower()).ToArray();

            //verify mandatory headers
            var missingHeaders = mandatoryFields.Where(f => !headerFields.Contains(f));
            if (missingHeaders.Count() > 0)
                throw new ParsingException($"Failed parsing file '{fileName}'. The following headers are missing: {string.Join("; ", missingHeaders)}");

            //read line by line
            var parsedLines = new List<ParsedLine>();
            uint lineIndex = 0;
            var rawlineFields = csvParser.ReadFields();

            while (rawlineFields != null)
            {
                var lineFields = rawlineFields.Select(f => f.Trim().ToLower());

                //create a list of key/values pairs
                var items = headerFields.Zip(lineFields, (a, b) => new KeyValuePair<string, string>(a, b));
                parsedLines.Add(new ParsedLine(lineIndex, items));
                logger.Log($"Adding new parsed line [{lineIndex}]: {ItemsToString(items)}");

                rawlineFields = csvParser.ReadFields();

                lineIndex++;
            }

            logger.Log($"Parsing file '{fileName}' completed, {parsedLines.Count()} data lines read.");
            return parsedLines;
        }
    }
}