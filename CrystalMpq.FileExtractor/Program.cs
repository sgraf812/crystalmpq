#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CrystalMpq;
using CrystalMpq.WoW;

namespace CrystalMpq.FileExtractor
{
	internal sealed class ErrorMessage
	{
		public string MessageName;
		public object[] Parameters;
	}

	internal static class Program
	{
		private enum SourceType
		{
			SingleArchive = 0,
			FileSystem = 1,
			WoWFileSystem = 2
		}

		private sealed class Options
		{
			public bool ShowLogo = true;
			public bool Verbose = false;
			public bool ShowHelp = false;
			public bool Extract = true;
			public bool? FileListParsing = null;
			public bool AutoPickSingleLanguagePack = true;
			public string ExternalFileList = null;
			public SourceType SourceType = SourceType.SingleArchive;
			public string SourceFile = null;
			public string[] ArchiveFiles = null;
			public string Destination = null;
			public ErrorMessage[] Errors = null;
		}

		[STAThread]
		private static int Main(string[] args)
		{
			var options = ParseCommandLine(args);

			if (options.Errors != null)
			{
				if (options.ShowLogo) PrintLogo();

				PrintUsage();

				return -1;
			}
			if (options.ShowLogo) PrintLogo();
			if (options.ShowHelp) PrintUsage();
			else
			{
				IMpqFileSystem fileSystem;

				switch (options.SourceType)
				{
					case SourceType.SingleArchive:
						fileSystem = new MpqFileSystem();
						fileSystem.Archives.Add(new MpqArchive(options.SourceFile));
						break;
					case SourceType.FileSystem:
						fileSystem = new MpqFileSystem();
						break;
					case SourceType.WoWFileSystem:
						break;
				}
			}

			return 0;
		}

		private static MpqFileSystem OpenFileSystem(string[] filenames)
		{
			var fileSystem = new MpqFileSystem(false);

			foreach (var filename in filenames)
				fileSystem.Archives.Add(new MpqArchive(filename, false));

			return fileSystem;
		}

		private static MpqFileSystem OpenFileSystem(string filename)
		{
			return OpenFileSystem(ReadListFile(filename));
		}

		private static string[] ReadListFile(string filename) { return File.ReadAllLines(filename); }

		private static string[] ReadListFile<TStream>(TStream stream) where TStream : Stream
		{
			var reader = new StreamReader(stream, true);
			var lines = new List<string>();
			string line;

			while ((line = reader.ReadLine()) != null) if (line.Length > 0) lines.Add(line);

			return lines.ToArray();
		}

		private static void PrintLogo()
		{
			Console.ResetColor();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("CrystalMpq Command-line MPQ File Extractor 0.1");
			Console.ResetColor();
			Console.WriteLine("Copyright © GoldenCrystal 2011");
		}

		private static void PrintUsage()
		{
		}

		private static Options ParseCommandLine(string[] args)
		{
			if (args.Length == 0) return null;

			int optionCount = args.Length;

			var options = new Options();

			for (int i = 0; i < args.Length; i++)
			{
				var arg = args[i];

				if (arg[0] != '/' && arg[0] != '-')
				{
					optionCount = i;
					break;
				}

				int parameterSeparatorIndex = arg.IndexOf(':');

				var switchName = arg.Substring(1, (parameterSeparatorIndex >= 0 ? parameterSeparatorIndex : arg.Length) - 1).ToUpperInvariant();
				var parameterValue = parameterSeparatorIndex >= 0 ? arg.Substring(parameterSeparatorIndex + 1) : null;

				switch (switchName)
				{
					case "WOW":
						options.SourceType = SourceType.WoWFileSystem;
						options.SourceFile = parameterValue;
						break;
					case "FS":
						options.SourceType = SourceType.FileSystem;
						options.SourceFile = parameterValue;
						break;
					case "MPQ":
						options.SourceType = SourceType.SingleArchive;
						options.SourceFile = parameterValue;
						break;
					case "L":
						break;
					case "E":
					case "EXTRACT":
					case "E+":
					case "EXTRACT+":
						options.Extract = true;
						break;
					case "ALL":
						break;
					case "V":
					case "VERBOSE":
					case "V+":
					case "VERBOSE+":
						options.Verbose = true;
						break;
					case "V-":
					case "VERBOSE-":
						options.Verbose = false;
						break;
					default:

						break;
				}
			}

			if (optionCount < args.Length)
			{
			}

			return options;
		}
	}
}
